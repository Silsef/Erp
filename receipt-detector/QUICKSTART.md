# 🚀 Guide de Démarrage Rapide

## Installation en 3 étapes

### 1️⃣ Préparer l'environnement

```bash
# Créer un environnement virtuel
python -m venv venv

# Activer l'environnement
# Sur Linux/Mac:
source venv/bin/activate
# Sur Windows:
venv\Scripts\activate

# Installer les dépendances
pip install -r requirements.txt
```

**⏱️ Temps estimé**: 5-10 minutes (téléchargement des modèles PaddleOCR)

### 2️⃣ Lancer l'API

```bash
uvicorn app.main:app --reload
```

L'API sera accessible sur: `http://localhost:8000`

### 3️⃣ Tester l'API

**Option A - Interface Web** 🌐

Ouvrir `test_interface.html` dans votre navigateur et uploader une image de ticket.

**Option B - Script Python** 🐍

```bash
python test_api.py chemin/vers/ticket.jpg
```

**Option C - curl** 💻

```bash
curl -X POST "http://localhost:8000/api/v1/analyze" \
  -F "file=@ticket.jpg"
```

**Option D - Documentation interactive** 📚

Ouvrir `http://localhost:8000/docs` dans votre navigateur.

## 📊 Exemple de réponse

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
        "confidence": 0.95
      },
      "ticket_number": 1
    }
  ],
  "total_receipts_found": 1
}
```

## 🎯 Cas d'usage

### 1. Un seul ticket
Uploadez simplement l'image, l'API détectera automatiquement le ticket.

### 2. Plusieurs tickets sur une image
L'API détectera et analysera chaque ticket séparément.

### 3. Ticket flou ou de mauvaise qualité
Le prétraitement automatique améliore la qualité avant l'OCR.

## ⚙️ Configuration

### Changer la langue OCR

Dans `app/services/ocr_service.py`:

```python
self.ocr = PaddleOCR(
    use_angle_cls=True,
    lang='en',  # 'fr', 'en', 'ch', 'spanish', etc.
    show_log=False
)
```

### Activer le GPU

Dans `app/services/ocr_service.py`:

```python
self.ocr = PaddleOCR(
    use_angle_cls=True,
    lang='fr',
    use_gpu=True  # ← Active le GPU
)
```

## 🐛 Problèmes courants

### L'API ne démarre pas
- Vérifier que l'environnement virtuel est activé
- Vérifier que toutes les dépendances sont installées

### Aucun texte détecté
- Vérifier la qualité de l'image
- Tester avec l'endpoint `/api/v1/ocr-only` pour voir le texte brut
- Essayer d'améliorer la résolution de l'image

### Montant ou date non extraits
- Vérifier le format dans le texte brut (endpoint OCR)
- Adapter les regex dans `app/services/extraction_service.py`

## 📞 Endpoints disponibles

| Endpoint | Méthode | Description |
|----------|---------|-------------|
| `/` | GET | Page d'accueil |
| `/health` | GET | Health check |
| `/api/v1/analyze` | POST | Analyser un ticket |
| `/api/v1/ocr-only` | POST | OCR uniquement |
| `/docs` | GET | Documentation Swagger |

## 🎓 Prochaines étapes

1. **Tester avec vos propres tickets** pour valider la détection
2. **Adapter les regex** si les formats ne correspondent pas
3. **Fine-tuner les patterns** selon vos besoins spécifiques
4. **Dockeriser** pour un déploiement facile

## 💡 Astuces

- Pour de meilleurs résultats, utilisez des images haute résolution (min 1000px de largeur)
- Les tickets doivent être bien éclairés et sans reflets
- Pour plusieurs tickets, espacez-les bien dans l'image
- Utilisez le format JPEG ou PNG

## 🔄 Workflow recommandé

```
1. Upload image → 
2. Détection des zones de tickets → 
3. OCR sur chaque zone → 
4. Extraction avec regex → 
5. Retour JSON structuré
```

## 🚀 Déploiement avec Docker

```bash
# Build
docker-compose build

# Lancer
docker-compose up -d

# Logs
docker-compose logs -f
```

Bon développement ! 🎉
