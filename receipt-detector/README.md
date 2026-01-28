# Receipt Detector API 🧾

API FastAPI pour détecter et extraire les informations des tickets de caisse (date, montant, devise).

## 🚀 Fonctionnalités

- ✅ **OCR puissant** avec PaddleOCR
- ✅ **Extraction automatique** de date, montant et devise
- ✅ **Support multi-tickets** sur une même image
- ✅ **Prétraitement d'image** pour améliorer la qualité OCR
- ✅ **API REST** facile à utiliser

## 📋 Prérequis

- Python 3.8+
- pip

## 🔧 Installation

1. **Cloner ou créer le projet**
```bash
cd receipt-detector
```

2. **Créer un environnement virtuel** (recommandé)
```bash
python -m venv venv
source venv/bin/activate  # Sur Windows: venv\Scripts\activate
```

3. **Installer les dépendances**
```bash
pip install -r requirements.txt
```

⚠️ **Note**: L'installation de PaddleOCR peut prendre quelques minutes car elle télécharge les modèles pré-entraînés.

## 🏃 Lancement

### Développement

```bash
# Depuis le dossier receipt-detector
uvicorn app.main:app --reload --host 0.0.0.0 --port 8000
```

### Production

```bash
uvicorn app.main:app --host 0.0.0.0 --port 8000 --workers 4
```

L'API sera accessible sur `http://localhost:8000`

## 📚 Documentation API

Une fois l'API lancée, accédez à la documentation interactive :

- **Swagger UI**: http://localhost:8000/docs
- **ReDoc**: http://localhost:8000/redoc

## 🔍 Endpoints

### 1. Health Check

```bash
GET /health
```

**Réponse**:
```json
{
  "status": "healthy",
  "timestamp": "2026-01-28T10:30:00"
}
```

### 2. Analyser un ticket (endpoint principal)

```bash
POST /api/v1/analyze
```

**Paramètres**:
- `file`: Image du ticket (PNG, JPG, JPEG)

**Exemple avec curl**:
```bash
curl -X POST "http://localhost:8000/api/v1/analyze" \
  -H "accept: application/json" \
  -H "Content-Type: multipart/form-data" \
  -F "file=@ticket.jpg"
```

**Réponse**:
```json
{
  "success": true,
  "message": "1 ticket(s) analysé(s) avec succès",
  "receipts": [
    {
      "receipt_info": {
        "date": "2026-01-28",
        "amount": 45.50,
        "currency": "EUR",
        "raw_text": "CARREFOUR\nDate: 28/01/2026\n...\nTOTAL: 45,50€",
        "confidence": 0.95
      },
      "bounding_box": {
        "x": 10,
        "y": 10,
        "width": 300,
        "height": 500
      },
      "ticket_number": 1
    }
  ],
  "total_receipts_found": 1
}
```

### 3. OCR uniquement (pour tests)

```bash
POST /api/v1/ocr-only
```

Extrait uniquement le texte brut sans analyse.

## 🧪 Test rapide avec Python

```python
import requests

# Analyser un ticket
with open('ticket.jpg', 'rb') as f:
    response = requests.post(
        'http://localhost:8000/api/v1/analyze',
        files={'file': f}
    )
    
print(response.json())
```

## 📁 Structure du projet

```
receipt-detector/
├── app/
│   ├── __init__.py
│   ├── main.py                    # FastAPI app
│   ├── models/
│   │   ├── __init__.py
│   │   └── schemas.py             # Modèles Pydantic
│   └── services/
│       ├── __init__.py
│       ├── ocr_service.py         # Service OCR
│       ├── extraction_service.py  # Extraction regex
│       └── detection_service.py   # Détection multi-tickets
├── requirements.txt
└── README.md
```

## 🎯 Formats reconnus

### Dates
- `28/01/2026`, `28-01-2026`, `28.01.2026`
- `28 janvier 2026`, `28 janv 2026`
- `2026-01-28`

### Montants
- `TOTAL: 45.50`, `Total : 45,50 EUR`
- `45.50€`, `45,50 €`
- Dernière ligne avec montant

### Devises
- EUR (€, euro, euros)
- USD ($, dollar, dollars)
- GBP (£, pound, livre)
- CHF (franc)

## 🔧 Configuration avancée

### Activer le GPU (optionnel)

Dans `app/services/ocr_service.py`, modifier :
```python
self.ocr = PaddleOCR(
    use_angle_cls=True,
    lang='fr',
    show_log=False,
    use_gpu=True  # ← Activer le GPU
)
```

### Changer la langue OCR

Langues supportées : 'fr', 'en', 'ch', 'spanish', etc.

```python
self.ocr = PaddleOCR(
    use_angle_cls=True,
    lang='en',  # ← Changer la langue
    show_log=False
)
```

## 🐛 Dépannage

### Erreur "No module named 'paddle'"
```bash
pip install paddlepaddle --break-system-packages
```

### OCR ne détecte rien
- Vérifier la qualité de l'image (résolution, contraste)
- Tester avec l'endpoint `/api/v1/ocr-only` pour voir le texte brut
- Essayer avec `preprocess=False` dans le code

### Performances lentes
- Activer le GPU si disponible
- Réduire la résolution des images
- Utiliser plusieurs workers uvicorn

## 📝 Prochaines améliorations

- [ ] Support de plus de formats de dates
- [ ] Extraction d'autres informations (TVA, articles, magasin)
- [ ] Fine-tuning d'un modèle NER personnalisé
- [ ] API de batch processing
- [ ] Dockerisation
- [ ] Tests unitaires

## 📄 Licence

MIT

## 👨‍💻 Auteur

Développé avec ❤️ et FastAPI
