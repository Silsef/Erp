# 🚀 Guide de Démarrage Rapide - Version Corrigée

## ⚡ Installation Ultra-Rapide

### Option 1 : Script automatique (Linux/Mac)
```bash
chmod +x install.sh
./install.sh
```

### Option 2 : Installation manuelle

```bash
# 1. Créer un environnement virtuel
python3 -m venv venv

# 2. Activer l'environnement
# Sur Linux/Mac:
source venv/bin/activate
# Sur Windows:
venv\Scripts\activate

# 3. Installer les dépendances
pip install -r requirements.txt
```

**⏱️ Temps estimé**: 5-10 minutes

## 🏃 Lancer l'API

```bash
uvicorn app.main:app --reload
```

✅ API accessible sur: `http://localhost:8000`

## 🧪 Tester (3 options)

### Option A - Script Python Simple 🐍
```bash
python test_simple.py chemin/vers/ticket.jpg
```

### Option B - curl 💻
```bash
curl -X POST "http://localhost:8000/api/v1/analyze" \
  -F "file=@ticket.jpg"
```

### Option C - Documentation interactive 📚
Ouvrir `http://localhost:8000/docs` dans votre navigateur

## 📊 Exemple de réponse (NOUVEAU: avec TVA!)

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

## 🆕 Nouveautés de cette version

### ✅ Bug PaddleOCR corrigé
Le paramètre `cls` a été retiré des appels OCR.

### ✅ Support de la TVA ajouté
L'API extrait maintenant automatiquement le montant de la TVA.

**Formats supportés** :
- `TVA 20%: 10.50`
- `TVA: 10.50 EUR`
- `T.V.A. 10,50`
- `TVA 20.00% 10.50`

## 🎯 Informations extraites

1. 📅 **Date** du ticket
2. 💰 **Montant** total
3. 🧾 **TVA** (nouveau!)
4. 💱 **Devise** (EUR, USD, GBP, CHF)
5. 📈 **Score de confiance** OCR

## 🐛 Résolution de problèmes

### L'API ne démarre pas
```bash
# Vérifier que l'environnement est activé
which python  # Doit pointer vers venv/bin/python

# Réinstaller les dépendances
pip install -r requirements.txt --force-reinstall
```

### Erreur PaddleOCR
```bash
# Vérifier l'installation
python -c "from paddleocr import PaddleOCR; print('OK')"

# Si erreur, réinstaller
pip install paddlepaddle paddleocr --force-reinstall
```

### Aucun texte détecté
- Utilisez des images de bonne qualité (min 1000px)
- Assurez un bon contraste et éclairage
- Testez avec `/api/v1/ocr-only` pour voir le texte brut

### TVA non détectée
- Vérifiez que le ticket contient bien une ligne TVA
- Le format doit contenir "TVA" suivi d'un montant
- Testez avec `/api/v1/ocr-only` pour voir le texte brut

## 📞 Endpoints disponibles

| Endpoint | Description |
|----------|-------------|
| `GET /` | Page d'accueil |
| `GET /health` | Health check |
| `POST /api/v1/analyze` | **Analyse complète** (date, montant, TVA, devise) |
| `POST /api/v1/ocr-only` | OCR seul (debug) |
| `GET /docs` | Documentation Swagger |

## 💡 Astuces pour de meilleurs résultats

✅ **DO** :
- Images haute résolution (>1000px)
- Bon éclairage sans reflets
- Photos bien droites
- Tickets à plat

❌ **DON'T** :
- Images floues ou sombres
- Tickets froissés
- Reflets importants
- Résolution trop faible

## 🔄 Workflow

```
1. Upload image
   ↓
2. Détection des zones de tickets
   ↓
3. OCR sur chaque zone
   ↓
4. Extraction avec regex
   ↓
5. Retour JSON structuré
```

## 🎓 Prochaines étapes

1. ✅ Tester avec vos tickets
2. ✅ Adapter les regex si nécessaire
3. ✅ Déployer avec Docker (si besoin)

## 📦 Structure des fichiers corrigés

```
receipt-detector/
├── app/
│   ├── main.py                 ✅ Mis à jour (support TVA)
│   ├── models/schemas.py       ✅ Mis à jour (champ TVA)
│   └── services/
│       ├── ocr_service.py      ✅ CORRIGÉ (bug cls)
│       └── extraction_service.py ✅ Mis à jour (TVA)
├── requirements.txt
├── install.sh                  🆕 Script d'installation
├── test_simple.py              🆕 Script de test
├── QUICKSTART.md               📄 Ce fichier
└── README_CORRECTIONS.md       📄 Détails des corrections
```

## 🚀 C'est parti !

```bash
# 1. Installation
./install.sh  # ou installation manuelle

# 2. Lancer l'API
uvicorn app.main:app --reload

# 3. Tester
python test_simple.py mon_ticket.jpg
```

## 📚 Documentation complète

- **Corrections détaillées** : `README_CORRECTIONS.md`
- **API interactive** : `http://localhost:8000/docs`

Bon développement ! 🎉
