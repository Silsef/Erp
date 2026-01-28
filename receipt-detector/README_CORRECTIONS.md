# Receipt Detector API - Version Corrigée 🧾

## 🔧 Corrections apportées

### 1. **Correction de l'erreur PaddleOCR** ✅
**Problème** : `PaddleOCR.predict() got an unexpected keyword argument 'cls'`

**Solution** : 
- Retrait du paramètre `cls=True` dans les appels à `self.ocr.ocr()`
- Le paramètre `use_angle_cls=True` dans l'initialisation suffit

**Fichiers modifiés** :
- `app/services/ocr_service.py` : lignes 64 et 102

### 2. **Ajout du support de la TVA** 🆕
**Nouveau** : Extraction automatique du montant de la TVA

**Ajouts** :
- Patterns regex pour détecter la TVA dans `extraction_service.py`
- Champ `tva` dans le modèle `ReceiptInfo`
- Affichage de la TVA dans les résultats

**Formats de TVA supportés** :
- `TVA 20%: 10.50`
- `TVA: 10.50 EUR`
- `TVA 20.00% 10.50`
- `T.V.A. 10,50`

## 🚀 Installation et démarrage

### 1. Installer les dépendances
```bash
cd receipt-detector
python -m venv venv
source venv/bin/activate  # Sur Windows: venv\Scripts\activate
pip install -r requirements.txt
```

### 2. Lancer l'API
```bash
uvicorn app.main:app --reload
```

L'API sera accessible sur `http://localhost:8000`

## 📊 Exemple de réponse (avec TVA)

```json
{
  "success": true,
  "message": "1 ticket(s) analysé(s) avec succès",
  "receipts": [
    {
      "receipt_info": {
        "date": "2026-01-28",
        "amount": 45.50,
        "tva": 7.58,
        "currency": "EUR",
        "confidence": 0.95
      },
      "ticket_number": 1
    }
  ],
  "total_receipts_found": 1
}
```

## 🧪 Tester l'API

### Option 1 - Interface Web
Ouvrir `test_interface.html` dans un navigateur (si vous l'avez créé)

### Option 2 - curl
```bash
curl -X POST "http://localhost:8000/api/v1/analyze" \
  -F "file=@ticket.jpg"
```

### Option 3 - Python
```python
import requests

with open('ticket.jpg', 'rb') as f:
    response = requests.post(
        'http://localhost:8000/api/v1/analyze',
        files={'file': f}
    )
    print(response.json())
```

### Option 4 - Documentation interactive
Ouvrir `http://localhost:8000/docs` dans un navigateur

## 📝 Ce qui a changé dans le code

### ocr_service.py
```python
# ❌ AVANT (causait l'erreur)
result = self.ocr.ocr(processed_image, cls=True)

# ✅ APRÈS (corrigé)
result = self.ocr.ocr(processed_image)
```

### extraction_service.py
```python
# 🆕 NOUVEAU : Patterns pour la TVA
TVA_PATTERNS = [
    r'(?:tva|t\.v\.a\.|taxe)[\s:]*(?:20%|10%|5,5%|2,1%)?[\s:]*([0-9]+[,\.][0-9]{2})',
    r'tva\s+[0-9]+[,\.][0-9]{1,2}%\s+([0-9]+[,\.][0-9]{2})',
    r'tva.*?([0-9]+[,\.][0-9]{2})',
]

# 🆕 NOUVEAU : Méthode d'extraction de TVA
def extract_tva(self, text: str) -> Optional[float]:
    # ... extraction de la TVA
```

### schemas.py
```python
# 🆕 NOUVEAU : Champ TVA ajouté
class ReceiptInfo(BaseModel):
    date: Optional[str] = None
    amount: Optional[float] = None
    tva: Optional[float] = None  # ← NOUVEAU
    currency: Optional[str] = None
    raw_text: Optional[str] = None
    confidence: Optional[float] = None
```

## 🎯 Informations extraites

L'API extrait maintenant **5 informations** :
1. ✅ **Date** (formats français : DD/MM/YYYY, etc.)
2. ✅ **Montant total** (avec virgule ou point)
3. ✅ **TVA** (nouveau !)
4. ✅ **Devise** (EUR, USD, GBP, CHF)
5. ✅ **Confiance OCR** (score de 0 à 1)

## 🐛 Si ça ne marche toujours pas

1. **Vérifier que PaddleOCR est bien installé** :
```bash
python -c "from paddleocr import PaddleOCR; print('OK')"
```

2. **Vérifier les logs** :
Les logs affichent les étapes de traitement et peuvent aider à identifier les problèmes.

3. **Tester l'OCR seul** :
```bash
curl -X POST "http://localhost:8000/api/v1/ocr-only" \
  -F "file=@ticket.jpg"
```

Cela permet de voir si le problème vient de l'OCR ou de l'extraction.

## 📞 Endpoints disponibles

| Endpoint | Méthode | Description |
|----------|---------|-------------|
| `/` | GET | Page d'accueil |
| `/health` | GET | Health check |
| `/api/v1/analyze` | POST | Analyser un ticket (complet) |
| `/api/v1/ocr-only` | POST | OCR uniquement (debug) |
| `/docs` | GET | Documentation Swagger |

## 💡 Conseils pour de meilleurs résultats

- Utilisez des images haute résolution (min 1000px)
- Assurez un bon éclairage sans reflets
- Prenez les photos bien droites
- Évitez les tickets froissés ou trop sombres

## 🔄 Structure du projet corrigé

```
receipt-detector/
├── app/
│   ├── __init__.py
│   ├── main.py                    # ✅ Mis à jour (support TVA)
│   ├── models/
│   │   ├── __init__.py
│   │   └── schemas.py             # ✅ Mis à jour (champ TVA)
│   └── services/
│       ├── __init__.py
│       ├── ocr_service.py         # ✅ CORRIGÉ (retrait cls)
│       ├── extraction_service.py  # ✅ Mis à jour (extraction TVA)
│       └── detection_service.py   # ✅ Inchangé
├── requirements.txt
└── README_CORRECTIONS.md          # ← Ce fichier
```

## 🎉 Version

**v1.0.1** - Corrections du bug PaddleOCR + Support TVA

---

**Développé avec ❤️ et FastAPI**
