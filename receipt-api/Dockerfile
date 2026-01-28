FROM python:3.10-slim-bookworm

# Métadonnées
LABEL maintainer="Receipt OCR API"
LABEL description="API d'analyse de tickets avec Ollama"

WORKDIR /app

# Installer curl pour le debugging
RUN apt-get update && apt-get install -y curl && rm -rf /var/lib/apt/lists/*

# Copier les dépendances
COPY requirements.txt requirements.txt

# Installer les dépendances Python
RUN pip install --no-cache-dir -r requirements.txt

# Copier le code
COPY main.py main.py

# Exposer le port
EXPOSE 8000

# Lancer l'application
CMD ["python", "main.py"]
