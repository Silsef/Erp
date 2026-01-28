from paddleocr import PaddleOCR
import cv2
import numpy as np
from typing import Tuple, List, Optional
import logging
import os

logger = logging.getLogger(__name__)

class OCRService:
    """Service optimisé pour l'extraction de texte (Stabilité CPU + Gestion basse résolution)"""
    
    def __init__(self):
        """Initialise PaddleOCR avec les paramètres de détection agressifs"""
        try:
            # DÉSACTIVATION TOTALE DE MKLDNN (Pour éviter le crash 'ConvertPirAttribute')
            os.environ['FLAGS_use_mkldnn'] = 'False' 
            os.environ['FLAGS_use_ngraph'] = 'False'
            os.environ['CUDA_VISIBLE_DEVICES'] = '' # Force CPU
            
            import paddle
            paddle.set_device('cpu')
            
            logger.info("Configuration OCR : Mode CPU Standard (Stable)")
            
            # --- CONFIGURATION PRINCIPALE ---
            self.ocr = PaddleOCR(
                use_angle_cls=False,         # DÉSACTIVÉ pour la vitesse (gain majeur)
                lang='fr',
                enable_mkldnn=False,         # DÉSACTIVÉ pour éviter le crash
                use_tensorrt=False,
                
                # Paramètres de sensibilité (CRUCIAL pour votre ticket pâle/petit)
                det_db_thresh=0.1,           # Détecte le texte très faible
                det_db_box_thresh=0.3,       # Accepte les boîtes incertaines
                det_db_unclip_ratio=2.0,     # Élargit les zones (capture les lettres floues)
                use_dilation=True            # Connecte les caractères brisés
            )
            logger.info("Moteur PaddleOCR prêt (Mode Sensible)")
            
        except Exception as e:
            logger.error(f"Erreur init OCR: {e}")
            # Fallback absolu
            self.ocr = PaddleOCR(use_angle_cls=False, lang='fr', enable_mkldnn=False)
    
    def preprocess_image(self, image: np.ndarray) -> np.ndarray:
        """
        Agrandit les petites images et applique une netteté.
        """
        if image is None:
            return None

        # 1. Conversion BGR -> RGB (Vital pour Paddle)
        if len(image.shape) == 3:
            image = cv2.cvtColor(image, cv2.COLOR_BGR2RGB)
            
        h, w = image.shape[:2]
        
        # 2. Agrandissement si nécessaire (target ~1000px)
        min_dim = 1000
        if min(h, w) < min_dim:
            scale = min_dim / min(h, w)
            scale = min(scale, 5.0) # Limite x5
            
            if scale > 1.0:
                new_w = int(w * scale)
                new_h = int(h * scale)
                # L'interpolation CUBIC garde les textes plus nets
                image = cv2.resize(image, (new_w, new_h), interpolation=cv2.INTER_CUBIC)
                logger.info(f"Upscale: {w}x{h} -> {new_w}x{new_h} (x{scale:.2f})")
                
                # 3. Filtre de netteté (Sharpen)
                kernel = np.array([[0, -1, 0], 
                                   [-1, 5,-1], 
                                   [0, -1, 0]])
                image = cv2.filter2D(image, -1, kernel)

        return image
    
    def _parse_ocr_result(self, result) -> Tuple[str, float]:
        if not result or not result[0]:
            logger.warning("OCR : Aucun texte détecté.")
            return "", 0.0
        
        lines = []
        scores = []
        
        for line in result[0]:
            try:
                # Format: [[[x,y],..], ("texte", score)]
                text_info = line[1]
                text = str(text_info[0])
                score = float(text_info[1])
                
                if text.strip():
                    lines.append(text)
                    scores.append(score)
            except Exception:
                continue
        
        full_text = "\n".join(lines)
        avg_conf = sum(scores) / len(scores) if scores else 0.0
        
        logger.info(f"Succès OCR : {len(lines)} lignes (Conf: {avg_conf:.2f})")
        return full_text, avg_conf
    
    def extract_text(self, image_path: str, preprocess: bool = True) -> Tuple[str, float]:
        try:
            image = cv2.imread(image_path)
            if image is None: return "", 0.0
            return self.process_ocr(image)
        except Exception as e:
            logger.error(f"Erreur extract_text: {e}")
            raise
    
    def extract_text_from_bytes(self, image_bytes: bytes, preprocess: bool = True) -> Tuple[str, float]:
        try:
            nparr = np.frombuffer(image_bytes, np.uint8)
            image = cv2.imdecode(nparr, cv2.IMREAD_COLOR)
            if image is None: return "", 0.0
            return self.process_ocr(image)
        except Exception as e:
            logger.error(f"Erreur extract_text_from_bytes: {e}")
            raise

    def process_ocr(self, image):
        # Pré-traitement (Agrandissement + RGB + Netteté)
        proc_image = self.preprocess_image(image)
        
        # Appel simple sans arguments conflictuels
        result = self.ocr.ocr(proc_image)
        
        return self._parse_ocr_result(result)