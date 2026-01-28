from paddleocr import PaddleOCR
import cv2
import numpy as np
from PIL import Image
from typing import Tuple, List, Optional
import logging

logger = logging.getLogger(__name__)


class OCRService:
    """Service pour l'extraction de texte des images de tickets"""
    
    def __init__(self):
        """Initialise PaddleOCR"""
        try:
            # Initialiser PaddleOCR (français + anglais)
            # Version 3.3.3 - paramètres simplifiés
            self.ocr = PaddleOCR(
                use_angle_cls=True,
                lang='fr'
            )
            logger.info("PaddleOCR initialisé avec succès")
        except Exception as e:
            logger.error(f"Erreur lors de l'initialisation de PaddleOCR: {e}")
            raise
    
    def preprocess_image(self, image: np.ndarray) -> np.ndarray:
        """
        Prétraite l'image pour améliorer l'OCR
        
        Args:
            image: Image en format numpy array
            
        Returns:
            Image prétraitée
        """
        # Conversion en niveaux de gris
        if len(image.shape) == 3:
            gray = cv2.cvtColor(image, cv2.COLOR_BGR2GRAY)
        else:
            gray = image
        
        # Augmentation du contraste avec CLAHE
        clahe = cv2.createCLAHE(clipLimit=2.0, tileGridSize=(8, 8))
        enhanced = clahe.apply(gray)
        
        # Débruitage
        denoised = cv2.fastNlMeansDenoising(enhanced)
        
        # Binarisation adaptative
        binary = cv2.adaptiveThreshold(
            denoised,
            255,
            cv2.ADAPTIVE_THRESH_GAUSSIAN_C,
            cv2.THRESH_BINARY,
            11,
            2
        )
        
        return binary
    
    def extract_text(self, image_path: str, preprocess: bool = True) -> Tuple[str, float]:
        """
        Extrait le texte d'une image de ticket
        
        Args:
            image_path: Chemin vers l'image
            preprocess: Appliquer le prétraitement ou non
            
        Returns:
            Tuple (texte extrait, score de confiance moyen)
        """
        try:
            # Charger l'image
            image = cv2.imread(image_path)
            if image is None:
                raise ValueError(f"Impossible de charger l'image: {image_path}")
            
            # Prétraitement optionnel
            if preprocess:
                processed_image = self.preprocess_image(image)
            else:
                processed_image = image
            
            # OCR
            result = self.ocr.ocr(processed_image, cls=True)
            
            if not result or not result[0]:
                logger.warning("Aucun texte détecté dans l'image")
                return "", 0.0
            
            # Extraire le texte et calculer la confiance moyenne
            lines = []
            confidences = []
            
            for line in result[0]:
                text = line[1][0]  # Texte détecté
                confidence = line[1][1]  # Score de confiance
                lines.append(text)
                confidences.append(confidence)
            
            full_text = "\n".join(lines)
            avg_confidence = sum(confidences) / len(confidences) if confidences else 0.0
            
            logger.info(f"Texte extrait avec confiance moyenne: {avg_confidence:.2f}")
            return full_text, avg_confidence
            
        except Exception as e:
            logger.error(f"Erreur lors de l'extraction de texte: {e}")
            raise
    
    def extract_text_from_bytes(self, image_bytes: bytes, preprocess: bool = True) -> Tuple[str, float]:
        """
        Extrait le texte d'une image en bytes
        
        Args:
            image_bytes: Image en bytes
            preprocess: Appliquer le prétraitement ou non
            
        Returns:
            Tuple (texte extrait, score de confiance moyen)
        """
        try:
            # Convertir bytes en numpy array
            nparr = np.frombuffer(image_bytes, np.uint8)
            image = cv2.imdecode(nparr, cv2.IMREAD_COLOR)
            
            if image is None:
                raise ValueError("Impossible de décoder l'image")
            
            # Prétraitement optionnel
            if preprocess:
                processed_image = self.preprocess_image(image)
            else:
                processed_image = image
            
            # OCR
            result = self.ocr.ocr(processed_image, cls=True)
            
            if not result or not result[0]:
                logger.warning("Aucun texte détecté dans l'image")
                return "", 0.0
            
            # Extraire le texte et calculer la confiance moyenne
            lines = []
            confidences = []
            
            for line in result[0]:
                text = line[1][0]
                confidence = line[1][1]
                lines.append(text)
                confidences.append(confidence)
            
            full_text = "\n".join(lines)
            avg_confidence = sum(confidences) / len(confidences) if confidences else 0.0
            
            return full_text, avg_confidence
            
        except Exception as e:
            logger.error(f"Erreur lors de l'extraction de texte depuis bytes: {e}")
            raise