# 🪟 Guide d'Installation Windows

## Problème rencontré

Erreur lors de l'installation de numpy sur Python 3.13 :
```
ERROR: Unknown compiler(s): [['cl'], ['cc'], ['gcc']...]
```

## ✅ Solution : Installation en 2 étapes

### Étape 1 : Installer numpy seul d'abord

```cmd
pip install numpy
```

Cette commande va télécharger une version **précompilée** de numpy compatible avec Python 3.13.

### Étape 2 : Installer le reste des dépendances

```cmd
pip install -r requirements.txt
```

## 🚀 Installation complète

Copie et colle ces commandes dans ton terminal (dans le dossier receipt-detector) :

```cmd
REM Créer l'environnement virtuel
python -m venv venv

REM Activer l'environnement
venv\Scripts\activate

REM Mettre à jour pip
python -m pip install --upgrade pip

REM Installer numpy d'abord (version précompilée)
pip install numpy

REM Installer le reste
pip install fastapi==0.109.0
pip install uvicorn[standard]==0.27.0
pip install python-multipart==0.0.6
pip install Pillow
pip install paddleocr
pip install paddlepaddle
pip install opencv-python-headless
pip install pydantic==2.5.3
pip install pydantic-settings==2.1.0

REM Lancer l'API
uvicorn app.main:app --reload
```

## 🎯 Méthode alternative : Script batch

Créer un fichier `install.bat` avec ce contenu :

```batch
@echo off
echo Installation de Receipt Detector API pour Windows
echo ================================================

REM Activer l'environnement
call venv\Scripts\activate

REM Upgrade pip
python -m pip install --upgrade pip

REM Installer numpy seul d'abord
echo.
echo Installation de numpy...
pip install numpy

REM Installer le reste
echo.
echo Installation des autres dépendances...
pip install fastapi==0.109.0
pip install uvicorn[standard]==0.27.0
pip install python-multipart==0.0.6
pip install Pillow
pip install paddleocr
pip install paddlepaddle
pip install opencv-python-headless
pip install pydantic==2.5.3
pip install pydantic-settings==2.1.0

echo.
echo ================================================
echo Installation terminee !
echo.
echo Pour lancer l'API :
echo   venv\Scripts\activate
echo   uvicorn app.main:app --reload
echo.
pause
```

Puis double-cliquer sur `install.bat`.

## 🐛 Si ça ne marche toujours pas

### Option 1 : Utiliser Python 3.11 au lieu de 3.13

Python 3.13 est très récent et peut causer des problèmes de compatibilité.

1. Désinstaller Python 3.13
2. Installer Python 3.11.x depuis [python.org](https://www.python.org/downloads/)
3. Recommencer l'installation

### Option 2 : Installer les Build Tools (si tu veux garder Python 3.13)

1. Télécharger [Visual Studio Build Tools](https://visualstudio.microsoft.com/fr/downloads/)
2. Installer avec "Desktop development with C++"
3. Redémarrer le terminal
4. Réessayer `pip install -r requirements.txt`

## ✅ Vérification de l'installation

Après installation, teste :

```cmd
python -c "import numpy; print('numpy:', numpy.__version__)"
python -c "import paddleocr; print('paddleocr: OK')"
python -c "import fastapi; print('fastapi: OK')"
```

Si tout s'affiche sans erreur, c'est bon ! 🎉

## 🚀 Lancer l'API

```cmd
venv\Scripts\activate
uvicorn app.main:app --reload
```

Tu devrais voir :
```
INFO:     Uvicorn running on http://127.0.0.1:8000
INFO:     PaddleOCR initialisé avec succès
INFO:     Services initialisés avec succès
```

## 🧪 Tester

```cmd
python test_simple.py chemin\vers\ticket.jpg
```

Ou ouvre http://127.0.0.1:8000/docs dans ton navigateur.

---

**Résumé de la solution** : Installer numpy seul d'abord avec `pip install numpy`, puis installer le reste. Python 3.13 est trop récent et essaie de compiler numpy, alors qu'installer numpy seul télécharge la version précompilée.
