@echo off
echo ========================================
echo Installation Receipt Detector API
echo ========================================
echo.

REM Vérifier si venv existe
if not exist "venv\" (
    echo Creation de l'environnement virtuel...
    python -m venv venv
)

REM Activer l'environnement
echo Activation de l'environnement virtuel...
call venv\Scripts\activate

REM Upgrade pip
echo.
echo Mise a jour de pip...
python -m pip install --upgrade pip

REM Installer numpy seul d'abord (solution au problème de compilation)
echo.
echo ========================================
echo Installation de numpy (precompile)...
echo ========================================
pip install numpy

if errorlevel 1 (
    echo.
    echo ERREUR lors de l'installation de numpy
    echo.
    echo Solutions possibles :
    echo 1. Installer Python 3.11 au lieu de 3.13
    echo 2. Installer Visual Studio Build Tools
    echo.
    pause
    exit /b 1
)

REM Installer le reste des dépendances
echo.
echo ========================================
echo Installation des autres dependances...
echo ========================================
echo.

echo - fastapi...
pip install fastapi==0.109.0

echo - uvicorn...
pip install uvicorn[standard]==0.27.0

echo - python-multipart...
pip install python-multipart==0.0.6

echo - Pillow...
pip install Pillow

echo - paddleocr (peut prendre quelques minutes)...
pip install paddleocr

echo - paddlepaddle...
pip install paddlepaddle

echo - opencv-python-headless...
pip install opencv-python-headless

echo - pydantic...
pip install pydantic==2.5.3

echo - pydantic-settings...
pip install pydantic-settings==2.1.0

echo.
echo ========================================
echo Installation terminee avec succes !
echo ========================================
echo.
echo Pour lancer l'API :
echo   1. venv\Scripts\activate
echo   2. uvicorn app.main:app --reload
echo.
echo Pour tester :
echo   python test_simple.py chemin\vers\ticket.jpg
echo.
echo Documentation interactive :
echo   http://localhost:8000/docs
echo.
pause
