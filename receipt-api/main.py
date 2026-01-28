"""
API REST pour l'analyse de tickets de caisse avec Ollama (LLM Vision)
Optimisé pour llama3.2-vision, llava, bakllava
"""
from fastapi import FastAPI, File, UploadFile, HTTPException
from pydantic import BaseModel
from typing import Optional
import base64
import requests
import json
import logging
from PIL import Image
import io
import os

# Configuration du logging
logging.basicConfig(level=logging.INFO)
logger = logging.getLogger("ReceiptOCR")

app = FastAPI(title="Receipt OCR with Ollama", version="3.1.0")

# Configuration Ollama - Utilise la variable d'environnement ou localhost par défaut
OLLAMA_BASE_URL = os.getenv("OLLAMA_BASE_URL", "http://localhost:11434")
OLLAMA_URL = f"{OLLAMA_BASE_URL}/api/generate"
# Modèles recommandés : llama3.2-vision:11b, llava, bakllava
OLLAMA_MODEL = os.getenv("OLLAMA_MODEL", "llama3.2-vision:11b")

class ReceiptData(BaseModel):
    success: bool
    montant_ttc: Optional[float] = None
    tva: Optional[float] = None
    date: Optional[str] = None
    devise: Optional[str] = "EUR"
    merchant: Optional[str] = None
    confidence: float
    raw_response: Optional[str] = None
    message: Optional[str] = None

def image_to_base64(image_bytes: bytes) -> str:
    """Convertit une image en base64"""
    return base64.b64encode(image_bytes).decode('utf-8')

def extract_json_from_text(text: str) -> dict:
    """Extrait un objet JSON d'une réponse texte"""
    try:
        # Cherche un bloc JSON dans le texte
        start = text.find('{')
        end = text.rfind('}') + 1
        if start != -1 and end > start:
            json_str = text[start:end]
            return json.loads(json_str)
    except Exception as e:
        logger.debug(f"Erreur extraction JSON: {e}")
    return {}

def parse_llm_response(response_text: str) -> dict:
    """Parse la réponse du LLM pour extraire les données structurées"""
    result = {
        "montant_ttc": None,
        "tva": None,
        "date": None,
        "devise": "EUR",
        "merchant": None
    }
    
    # D'abord essayer d'extraire du JSON
    json_data = extract_json_from_text(response_text)
    if json_data:
        result.update({
            "montant_ttc": json_data.get("montant_ttc") or json_data.get("total") or json_data.get("montant"),
            "tva": json_data.get("tva") or json_data.get("tax"),
            "date": json_data.get("date"),
            "devise": json_data.get("devise") or json_data.get("currency", "EUR"),
            "merchant": json_data.get("merchant") or json_data.get("commercant") or json_data.get("magasin")
        })
        return result
    
    # Sinon, extraction par regex (fallback)
    import re
    
    # Recherche montant total
    patterns_montant = [
        r'montant[:\s]*([0-9]+[.,][0-9]{2})',
        r'total[:\s]*([0-9]+[.,][0-9]{2})',
        r'€[:\s]*([0-9]+[.,][0-9]{2})',
        r'EUR[:\s]*([0-9]+[.,][0-9]{2})',
    ]
    for pattern in patterns_montant:
        match = re.search(pattern, response_text, re.IGNORECASE)
        if match:
            try:
                result["montant_ttc"] = float(match.group(1).replace(',', '.'))
                break
            except:
                pass
    
    # Recherche TVA
    patterns_tva = [
        r'tva[:\s]*([0-9]+[.,][0-9]{2})',
        r'tax[:\s]*([0-9]+[.,][0-9]{2})',
    ]
    for pattern in patterns_tva:
        match = re.search(pattern, response_text, re.IGNORECASE)
        if match:
            try:
                result["tva"] = float(match.group(1).replace(',', '.'))
                break
            except:
                pass
    
    # Recherche date
    date_pattern = r'(\d{1,2}[/-]\d{1,2}[/-]\d{2,4})'
    match = re.search(date_pattern, response_text)
    if match:
        result["date"] = match.group(1)
    
    return result

def analyze_with_ollama(image_base64: str) -> tuple[str, bool]:
    """Envoie l'image à Ollama pour analyse"""
    
    # Prompt adapté selon le modèle
    if "llama3.2-vision" in OLLAMA_MODEL.lower():
        # Prompt optimisé pour llama3.2-vision (préfère l'anglais)
        prompt = """You are a receipt analyzer. Extract the following information from this receipt image and return ONLY valid JSON:

{
  "montant_ttc": <total amount as number>,
  "tva": <VAT/tax amount as number or null>,
  "date": "YYYY-MM-DD",
  "devise": "EUR",
  "merchant": "<store name>"
}

Rules:
- montant_ttc: Find the TOTAL amount to pay (look for: TOTAL, TOTAL TTC, À PAYER, MONTANT, etc.)
- Return ONLY the JSON object, no other text
- Use null if information is not found
- Numbers must be float values (e.g., 45.50 not "45.50")
- Date format must be YYYY-MM-DD
"""
    else:
        # Prompt pour llava et autres modèles (français OK)
        prompt = """Analyse ce ticket de caisse et extrais les informations suivantes au format JSON strict :
{
  "montant_ttc": <montant total avec décimales>,
  "tva": <montant de la TVA si visible>,
  "date": "YYYY-MM-DD",
  "devise": "EUR",
  "merchant": "<nom du magasin>"
}

Règles importantes :
- montant_ttc : le montant TOTAL à payer (cherche TOTAL, SOUSTOTAL, À PAYER, etc.)
- Retourne uniquement le JSON, sans texte avant ou après
- Si une valeur n'est pas trouvée, utilise null
- Les montants doivent être des nombres (ex: 45.50, pas "45.50")
"""

    payload = {
        "model": OLLAMA_MODEL,
        "prompt": prompt,
        "images": [image_base64],
        "stream": False,
        "options": {
            "temperature": 0.1,  # Moins de créativité pour plus de précision
            "num_predict": 200   # Limiter la longueur de réponse
        }
    }
    
    try:
        logger.info(f"Envoi de la requête à Ollama (modèle: {OLLAMA_MODEL})...")
        logger.info(f"URL: {OLLAMA_URL}")
        response = requests.post(OLLAMA_URL, json=payload, timeout=120)
        response.raise_for_status()
        
        result = response.json()
        llm_response = result.get("response", "")
        
        logger.info(f"Réponse d'Ollama : {llm_response[:200]}...")
        return llm_response, True
        
    except requests.exceptions.ConnectionError:
        logger.error(f"Impossible de se connecter à Ollama à {OLLAMA_URL}")
        return "", False
    except requests.exceptions.Timeout:
        logger.error("Timeout lors de la connexion à Ollama")
        return "", False
    except Exception as e:
        logger.error(f"Erreur Ollama: {e}")
        return "", False

@app.get("/")
async def root():
    return {
        "message": "Receipt OCR API with Ollama - Version 3.1.0",
        "ollama_url": OLLAMA_URL,
        "ollama_model": OLLAMA_MODEL,
        "endpoints": {
            "/analyze": "POST - Analyser un ticket de caisse",
            "/health": "GET - Vérifier l'état de l'API",
            "/test-ollama": "GET - Tester la connexion à Ollama",
            "/docs": "GET - Documentation Swagger"
        }
    }

@app.get("/health")
async def health():
    return {
        "status": "healthy",
        "model": "Ollama + " + OLLAMA_MODEL,
        "ollama_url": OLLAMA_URL,
        "version": "3.1.0"
    }

@app.get("/test-ollama")
async def test_ollama():
    """Test la connexion à Ollama"""
    try:
        tags_url = f"{OLLAMA_BASE_URL}/api/tags"
        logger.info(f"Test de connexion à: {tags_url}")
        response = requests.get(tags_url, timeout=5)
        if response.status_code == 200:
            models = response.json().get("models", [])
            model_names = [m.get("name") for m in models]
            return {
                "status": "connected",
                "available_models": model_names,
                "current_model": OLLAMA_MODEL,
                "model_exists": OLLAMA_MODEL in model_names,
                "ollama_url": OLLAMA_BASE_URL
            }
        else:
            return {
                "status": "error",
                "message": "Ollama répond mais avec une erreur",
                "status_code": response.status_code
            }
    except requests.exceptions.ConnectionError:
        return {
            "status": "disconnected",
            "message": f"Ollama n'est pas accessible à {OLLAMA_BASE_URL}",
            "suggestion": "Vérifiez que le conteneur Ollama est démarré"
        }
    except Exception as e:
        return {
            "status": "error",
            "message": str(e)
        }

@app.post("/analyze", response_model=ReceiptData)
async def analyze_receipt(file: UploadFile = File(...)):
    """Analyse un ticket de caisse avec Ollama"""
    
    try:
        # 1. Lecture de l'image
        contents = await file.read()
        
        # 2. Validation de l'image
        try:
            image = Image.open(io.BytesIO(contents))
            logger.info(f"Image reçue: {image.format}, taille: {image.size}")
            
            # Redimensionner si trop grande pour économiser des tokens
            max_size = 1024
            if max(image.size) > max_size:
                logger.info(f"Redimensionnement de l'image de {image.size} à max {max_size}px")
                image.thumbnail((max_size, max_size), Image.Resampling.LANCZOS)
                # Reconvertir en bytes
                buffer = io.BytesIO()
                image.save(buffer, format=image.format or 'JPEG')
                contents = buffer.getvalue()
        except Exception as e:
            raise HTTPException(status_code=400, detail=f"Image invalide: {e}")
        
        # 3. Conversion en base64
        image_b64 = image_to_base64(contents)
        
        # 4. Analyse avec Ollama
        llm_response, success = analyze_with_ollama(image_b64)
        
        if not success:
            return ReceiptData(
                success=False,
                confidence=0.0,
                message="Erreur de connexion à Ollama. Vérifiez qu'Ollama est démarré."
            )
        
        if not llm_response:
            return ReceiptData(
                success=False,
                confidence=0.0,
                message="Aucune réponse d'Ollama"
            )
        
        # 5. Parse la réponse
        extracted_data = parse_llm_response(llm_response)
        
        # 6. Calcul de la confiance
        confidence = 0
        if extracted_data["montant_ttc"]:
            confidence += 50
        if extracted_data["date"]:
            confidence += 20
        if extracted_data["merchant"]:
            confidence += 15
        if extracted_data["tva"]:
            confidence += 15
        
        return ReceiptData(
            success=True,
            montant_ttc=extracted_data["montant_ttc"],
            tva=extracted_data["tva"],
            date=extracted_data["date"],
            devise=extracted_data["devise"],
            merchant=extracted_data["merchant"],
            confidence=confidence / 100.0,
            raw_response=llm_response
        )
        
    except Exception as e:
        logger.error(f"Erreur lors de l'analyse: {str(e)}", exc_info=True)
        raise HTTPException(status_code=500, detail=f"Erreur d'analyse: {str(e)}")

if __name__ == "__main__":
    import uvicorn
    uvicorn.run(app, host="0.0.0.0", port=8000)