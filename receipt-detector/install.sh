#!/bin/bash

# Script d'installation rapide pour Receipt Detector API

echo "🧾 Installation de Receipt Detector API"
echo "========================================"
echo ""

# Vérifier Python
if ! command -v python3 &> /dev/null; then
    echo "❌ Python 3 n'est pas installé"
    exit 1
fi

echo "✅ Python $(python3 --version) détecté"
echo ""

# Créer environnement virtuel
echo "📦 Création de l'environnement virtuel..."
python3 -m venv venv

# Activer environnement virtuel
if [[ "$OSTYPE" == "msys" || "$OSTYPE" == "win32" ]]; then
    echo "🪟 Windows détecté"
    source venv/Scripts/activate
else
    echo "🐧 Linux/Mac détecté"
    source venv/bin/activate
fi

# Installer dépendances
echo ""
echo "📥 Installation des dépendances..."
pip install --upgrade pip
pip install -r requirements.txt

echo ""
echo "✅ Installation terminée !"
echo ""
echo "🚀 Pour lancer l'API :"
echo ""
if [[ "$OSTYPE" == "msys" || "$OSTYPE" == "win32" ]]; then
    echo "   venv\\Scripts\\activate"
else
    echo "   source venv/bin/activate"
fi
echo "   uvicorn app.main:app --reload"
echo ""
echo "📚 Documentation : http://localhost:8000/docs"
echo ""
