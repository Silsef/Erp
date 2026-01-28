from fastapi import FastAPI, File, UploadFile, HTTPException, status
from fastapi.middleware.cors import CORSMiddleware
from fastapi.responses import JSONResponse
import logging
from datetime import datetime
import tempfile
import os
from typing import List

from app.models.schemas import ReceiptResponse, HealthResponse, DetectedReceipt, ReceiptInfo, BoundingBox
from app.services import OCRService, ExtractionService, DetectionService

# Configuration du logging
logging.basicConfig(
    level=logging.INFO,
    format='%(asctime)s - %(name)s - %(levelname)s - %(message)s'
)
logger = logging.getLogger(__name__)

# Initialisation de l'application FastAPI
app = FastAPI(
    title="Receipt Detector API",
    description="API pour détecter et extraire les informations des tickets de caisse",
    version="1.0.0"
)

# Configuration CORS
app.add_middleware(
    CORSMiddleware,
    allow_origins=["*"],  # À restreindre en production
    allow_credentials=True,
    allow_methods=["*"],
    allow_headers=["*"],
)

# Initialisation des services (chargement au démarrage)
ocr_service = None
extraction_service = None
detection_service = None


@app.on_event("startup")
async def startup_event():
    """Initialise les services au démarrage de l'application"""
    global ocr_service, extraction_service, detection_service
    
    logger.info("Initialisation des services...")
    
    try:
        ocr_service = OCRService()
        extraction_service = ExtractionService()
        detection_service = DetectionService()
        logger.info("Services initialisés avec succès")
    except Exception as e:
        logger.error(f"Erreur lors de l'initialisation des services: {e}")
        raise


@app.get("/", response_model=HealthResponse)
async def root():
    """Endpoint racine pour vérifier que l'API fonctionne"""
    return HealthResponse(
        status="ok",
        timestamp=datetime.now()
    )


@app.get("/health", response_model=HealthResponse)
async def health_check():
    """Health check endpoint"""
    return HealthResponse(
        status="healthy",
        timestamp=datetime.now()
    )


@app.post("/api/v1/analyze", response_model=ReceiptResponse)
async def analyze_receipt(
    file: UploadFile = File(..., description="Image du/des ticket(s) de caisse")
):
    """
    Analyse une image contenant un ou plusieurs tickets de caisse
    et extrait les informations (date, montant, devise)
    
    Args:
        file: Fichier image uploadé (PNG, JPG, JPEG)
        
    Returns:
        ReceiptResponse avec les informations extraites
    """
    # Vérifier le type de fichier
    if not file.content_type.startswith('image/'):
        raise HTTPException(
            status_code=status.HTTP_400_BAD_REQUEST,
            detail="Le fichier doit être une image (PNG, JPG, JPEG)"
        )
    
    temp_file_path = None
    
    try:
        # Lire les bytes de l'image
        image_bytes = await file.read()
        
        # Créer un fichier temporaire
        with tempfile.NamedTemporaryFile(delete=False, suffix='.jpg') as temp_file:
            temp_file.write(image_bytes)
            temp_file_path = temp_file.name
        
        logger.info(f"Traitement de l'image: {file.filename}")
        
        # Étape 1: Détecter les tickets dans l'image
        detected_regions = detection_service.detect_from_bytes(image_bytes)
        logger.info(f"{len(detected_regions)} région(s) détectée(s)")
        
        detected_receipts = []
        
        # Étape 2: Traiter chaque ticket détecté
        for idx, (region_image, bbox) in enumerate(detected_regions, 1):
            logger.info(f"Traitement du ticket #{idx}")
            
            # Sauvegarder temporairement la région
            region_temp_path = None
            try:
                import cv2
                with tempfile.NamedTemporaryFile(delete=False, suffix='.jpg') as region_temp:
                    cv2.imwrite(region_temp.name, region_image)
                    region_temp_path = region_temp.name
                
                # OCR sur la région
                text, confidence = ocr_service.extract_text(region_temp_path, preprocess=True)
                
                if not text:
                    logger.warning(f"Aucun texte extrait pour le ticket #{idx}")
                    continue
                
                # Extraction des informations
                extracted_info = extraction_service.extract_all(text)
                
                # Créer l'objet de réponse
                receipt_info = ReceiptInfo(
                    date=extracted_info['date'],
                    amount=extracted_info['amount'],
                    currency=extracted_info['currency'],
                    raw_text=text,
                    confidence=confidence
                )
                
                bounding_box = BoundingBox(
                    x=bbox[0],
                    y=bbox[1],
                    width=bbox[2],
                    height=bbox[3]
                )
                
                detected_receipt = DetectedReceipt(
                    receipt_info=receipt_info,
                    bounding_box=bounding_box,
                    ticket_number=idx
                )
                
                detected_receipts.append(detected_receipt)
                
                logger.info(f"Ticket #{idx} traité - Date: {extracted_info['date']}, "
                          f"Montant: {extracted_info['amount']}, Devise: {extracted_info['currency']}")
                
            finally:
                # Nettoyer le fichier temporaire de la région
                if region_temp_path and os.path.exists(region_temp_path):
                    os.unlink(region_temp_path)
        
        # Construire la réponse
        if detected_receipts:
            response = ReceiptResponse(
                success=True,
                message=f"{len(detected_receipts)} ticket(s) analysé(s) avec succès",
                receipts=detected_receipts,
                total_receipts_found=len(detected_receipts)
            )
        else:
            response = ReceiptResponse(
                success=False,
                message="Aucun ticket détecté ou aucune information extraite",
                receipts=[],
                total_receipts_found=0
            )
        
        return response
        
    except Exception as e:
        logger.error(f"Erreur lors de l'analyse: {e}", exc_info=True)
        raise HTTPException(
            status_code=status.HTTP_500_INTERNAL_SERVER_ERROR,
            detail=f"Erreur lors de l'analyse de l'image: {str(e)}"
        )
    
    finally:
        # Nettoyer le fichier temporaire principal
        if temp_file_path and os.path.exists(temp_file_path):
            os.unlink(temp_file_path)


@app.post("/api/v1/ocr-only")
async def ocr_only(
    file: UploadFile = File(..., description="Image du ticket de caisse")
):
    """
    Endpoint simple pour tester uniquement l'OCR sans extraction
    
    Args:
        file: Fichier image uploadé
        
    Returns:
        Texte brut extrait par OCR
    """
    if not file.content_type.startswith('image/'):
        raise HTTPException(
            status_code=status.HTTP_400_BAD_REQUEST,
            detail="Le fichier doit être une image"
        )
    
    try:
        image_bytes = await file.read()
        text, confidence = ocr_service.extract_text_from_bytes(image_bytes, preprocess=True)
        
        return {
            "success": True,
            "text": text,
            "confidence": confidence,
            "message": "OCR effectué avec succès"
        }
        
    except Exception as e:
        logger.error(f"Erreur OCR: {e}")
        raise HTTPException(
            status_code=status.HTTP_500_INTERNAL_SERVER_ERROR,
            detail=f"Erreur lors de l'OCR: {str(e)}"
        )


if __name__ == "__main__":
    import uvicorn
    uvicorn.run(app, host="0.0.0.0", port=8000)
