import cv2
import numpy as np
from typing import List, Tuple
import logging

logger = logging.getLogger(__name__)


class DetectionService:
    """Service pour détecter plusieurs tickets dans une même image"""
    
    def __init__(self):
        self.min_contour_area = 10000  # Surface minimale pour considérer un ticket
    
    def detect_multiple_receipts(self, image_path: str) -> List[Tuple[np.ndarray, Tuple[int, int, int, int]]]:
        """
        Détecte et extrait plusieurs tickets d'une même image
        
        Args:
            image_path: Chemin vers l'image
            
        Returns:
            Liste de tuples (sous-image, bounding_box)
            bounding_box = (x, y, width, height)
        """
        try:
            # Charger l'image
            image = cv2.imread(image_path)
            if image is None:
                raise ValueError(f"Impossible de charger l'image: {image_path}")
            
            # Conversion en niveaux de gris
            gray = cv2.cvtColor(image, cv2.COLOR_BGR2GRAY)
            
            # Binarisation
            _, binary = cv2.threshold(gray, 0, 255, cv2.THRESH_BINARY_INV + cv2.THRESH_OTSU)
            
            # Trouver les contours
            contours, _ = cv2.findContours(binary, cv2.RETR_EXTERNAL, cv2.CHAIN_APPROX_SIMPLE)
            
            receipts = []
            
            # Si aucun contour significatif, retourner l'image entière
            if not contours or len(contours) == 0:
                h, w = image.shape[:2]
                return [(image, (0, 0, w, h))]
            
            # Filtrer et trier les contours par taille
            valid_contours = []
            for contour in contours:
                area = cv2.contourArea(contour)
                if area > self.min_contour_area:
                    valid_contours.append(contour)
            
            # Si aucun contour valide, retourner l'image entière
            if not valid_contours:
                h, w = image.shape[:2]
                return [(image, (0, 0, w, h))]
            
            # Extraire les zones rectangulaires
            for contour in valid_contours:
                x, y, w, h = cv2.boundingRect(contour)
                
                # Ajouter une petite marge
                margin = 10
                x = max(0, x - margin)
                y = max(0, y - margin)
                w = min(image.shape[1] - x, w + 2 * margin)
                h = min(image.shape[0] - y, h + 2 * margin)
                
                # Extraire la sous-image
                roi = image[y:y+h, x:x+w]
                receipts.append((roi, (x, y, w, h)))
            
            # Si aucun ticket détecté, retourner l'image entière
            if not receipts:
                h, w = image.shape[:2]
                return [(image, (0, 0, w, h))]
            
            logger.info(f"{len(receipts)} ticket(s) détecté(s)")
            return receipts
            
        except Exception as e:
            logger.error(f"Erreur lors de la détection de tickets: {e}")
            # En cas d'erreur, retourner l'image entière
            image = cv2.imread(image_path)
            h, w = image.shape[:2]
            return [(image, (0, 0, w, h))]
    
    def detect_from_bytes(self, image_bytes: bytes) -> List[Tuple[np.ndarray, Tuple[int, int, int, int]]]:
        """
        Détecte plusieurs tickets depuis une image en bytes
        
        Args:
            image_bytes: Image en bytes
            
        Returns:
            Liste de tuples (sous-image, bounding_box)
        """
        try:
            # Convertir bytes en numpy array
            nparr = np.frombuffer(image_bytes, np.uint8)
            image = cv2.imdecode(nparr, cv2.IMREAD_COLOR)
            
            if image is None:
                raise ValueError("Impossible de décoder l'image")
            
            # Conversion en niveaux de gris
            gray = cv2.cvtColor(image, cv2.COLOR_BGR2GRAY)
            
            # Binarisation
            _, binary = cv2.threshold(gray, 0, 255, cv2.THRESH_BINARY_INV + cv2.THRESH_OTSU)
            
            # Trouver les contours
            contours, _ = cv2.findContours(binary, cv2.RETR_EXTERNAL, cv2.CHAIN_APPROX_SIMPLE)
            
            receipts = []
            
            # Si aucun contour, retourner l'image entière
            if not contours:
                h, w = image.shape[:2]
                return [(image, (0, 0, w, h))]
            
            # Filtrer les contours
            valid_contours = []
            for contour in contours:
                area = cv2.contourArea(contour)
                if area > self.min_contour_area:
                    valid_contours.append(contour)
            
            # Si aucun contour valide, retourner l'image entière
            if not valid_contours:
                h, w = image.shape[:2]
                return [(image, (0, 0, w, h))]
            
            # Extraire les zones
            for contour in valid_contours:
                x, y, w, h = cv2.boundingRect(contour)
                
                margin = 10
                x = max(0, x - margin)
                y = max(0, y - margin)
                w = min(image.shape[1] - x, w + 2 * margin)
                h = min(image.shape[0] - y, h + 2 * margin)
                
                roi = image[y:y+h, x:x+w]
                receipts.append((roi, (x, y, w, h)))
            
            if not receipts:
                h, w = image.shape[:2]
                return [(image, (0, 0, w, h))]
            
            return receipts
            
        except Exception as e:
            logger.error(f"Erreur lors de la détection depuis bytes: {e}")
            # Retourner l'image entière en cas d'erreur
            nparr = np.frombuffer(image_bytes, np.uint8)
            image = cv2.imdecode(nparr, cv2.IMREAD_COLOR)
            h, w = image.shape[:2]
            return [(image, (0, 0, w, h))]
