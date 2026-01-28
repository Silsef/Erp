# API d'Analyse de Tickets de Caisse (Receipt OCR)

API REST performante pour extraire automatiquement les données structurées de tickets de caisse (JPG/PNG).

## 🚀 Fonctionnalités

- ✅ **Extraction robuste** : Montant TTC, TVA, Date, Devise
- ✅ **Prétraitement d'image** : Redimensionnement automatique, amélioration du contraste, réduction du bruit
- ✅ **Regex flexibles** : Gère différents formats de tickets
- ✅ **Gestion d'erreurs** : Messages d'erreur clairs
- ✅ **Versions fixes** : Aucun conflit de dépendances
- ✅ **Documentation automatique** : Interface Swagger intégrée

## 📋 Prérequis

### Option 1 : Installation locale

- Python 3.9+
- Tesseract OCR

**Installation de Tesseract :**

```bash
# Ubuntu/Debian
sudo apt-get install tesseract-ocr tesseract-ocr-fra

# macOS
brew install tesseract tesseract-lang

# Windows
# Télécharger depuis: https://github.com/UB-Mannheim/tesseract/wiki
```

### Option 2 : Docker (recommandé)

- Docker
- Docker Compose

## 🔧 Installation

### Méthode 1 : Installation locale

```bash
# 1. Créer un environnement virtuel
python -m venv venv

# 2. Activer l'environnement
# Linux/macOS :
source venv/bin/activate
# Windows :
venv\Scripts\activate

# 3. Installer les dépendances
pip install -r requirements.txt

# 4. Lancer l'API
python main.py
```

L'API sera accessible sur : http://localhost:8000

### Méthode 2 : Docker (recommandé)

```bash
# Construire et lancer avec Docker Compose
docker-compose up -d

# Voir les logs
docker-compose logs -f

# Arrêter l'API
docker-compose down
```

## 📖 Utilisation

### Interface Swagger (Documentation interactive)

Accédez à http://localhost:8000/docs pour une interface web interactive.

### Exemples avec curl

**1. Vérifier l'état de l'API :**

```bash
curl http://localhost:8000/health
```

**2. Analyser un ticket :**

```bash
curl -X POST "http://localhost:8000/analyze" \
  -H "accept: application/json" \
  -H "Content-Type: multipart/form-data" \
  -F "file=@ticket.jpg"
```

### Exemple avec Python

```python
import requests

# Analyser un ticket
with open('ticket.jpg', 'rb') as f:
    files = {'file': f}
    response = requests.post('http://localhost:8000/analyze', files=files)
    
result = response.json()
print(f"Montant TTC: {result['montant_ttc']} {result['devise']}")
print(f"TVA: {result['tva']}")
print(f"Date: {result['date']}")
print(f"Confiance: {result['confidence']}")
```

### Script de test inclus

```bash
# Test basique
python test_api.py

# Test avec une image
python test_api.py chemin/vers/ticket.jpg
```

## 📊 Format de réponse

```json
{
  "montant_ttc": 45.50,
  "tva": 4.14,
  "date": "2024-01-15",
  "devise": "EUR",
  "raw_text": "Texte complet extrait...",
  "confidence": "high"
}
```

**Niveaux de confiance :**
- `high` : 3-4 champs extraits
- `medium` : 2 champs extraits
- `low` : 0-1 champ extrait

## 🔍 Endpoints de l'API

| Endpoint | Méthode | Description |
|----------|---------|-------------|
| `/` | GET | Page d'accueil |
| `/health` | GET | Vérification de l'état |
| `/analyze` | POST | Analyser un ticket |
| `/docs` | GET | Documentation Swagger |

## 🛠️ Fonctionnalités avancées

### Prétraitement d'image

L'API gère automatiquement :
- ✅ **Redimensionnement** : Les images trop petites sont agrandies (min 800px)
- ✅ **Amélioration du contraste** : CLAHE pour améliorer la lisibilité
- ✅ **Réduction du bruit** : Filtre de débruitage
- ✅ **Binarisation adaptative** : Optimisation pour l'OCR

### Extraction robuste

Les regex sont flexibles et gèrent :
- Différents formats de nombres : `12.50`, `12,50`
- Plusieurs mots-clés : `TOTAL`, `TTC`, `MONTANT`, `À PAYER`
- Formats de dates variés : `DD/MM/YYYY`, `DD-MM-YYYY`, etc.
- Devises multiples : EUR, USD, GBP, CHF, CAD, JPY

## ⚠️ Résolution de problèmes

### Tesseract non trouvé

```bash
# Vérifier l'installation
tesseract --version

# Si non installé, installer Tesseract (voir Prérequis)
```

### Faible qualité d'extraction

- Assurez-vous que l'image est nette et bien éclairée
- Utilisez une résolution d'au moins 800x800 pixels
- Évitez les images avec beaucoup de bruit ou floutées

### Conflit de dépendances

Les versions sont fixées dans `requirements.txt` pour éviter les conflits.
En cas de problème, utilisez Docker.

## 🐳 Commandes Docker utiles

```bash
# Reconstruire l'image
docker-compose build

# Voir les logs en temps réel
docker-compose logs -f receipt-api

# Redémarrer le service
docker-compose restart

# Supprimer tout (conteneurs + volumes)
docker-compose down -v
```

## 📝 Notes importantes

1. **OCR multilingue** : L'API supporte le français et l'anglais par défaut
2. **Formats supportés** : JPG, JPEG, PNG uniquement
3. **Taille max** : Limitée par FastAPI (default 16MB)
4. **Performance** : ~2-5 secondes par ticket selon la taille

## 🚀 Améliorations futures possibles

- Support d'autres langues OCR
- API de batch pour traiter plusieurs tickets
- Cache pour améliorer les performances
- Support de PDF
- Détection automatique de la zone du ticket

## 📄 Licence

Ce projet est fourni "tel quel" pour usage personnel ou commercial.

## 🤝 Support

Pour tout problème ou suggestion, créez une issue ou contactez le développeur.

---

**Version** : 1.0.0  
**Dernière mise à jour** : Janvier 2025
