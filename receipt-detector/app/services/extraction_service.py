import re
from typing import Optional, Dict, Any
from datetime import datetime
import logging

logger = logging.getLogger(__name__)

class ExtractionService:
    """Service avancé pour extraire les informations structurées du texte OCR"""

    # --- PATTERNS REGEX AMÉLIORÉS ---
    
    # Dates : gère les espaces (28 / 01 / 2026), les tirets, points et mois textuels
    DATE_PATTERNS = [
        # JJ/MM/AAAA ou JJ-MM-AAAA avec espaces optionnels
        r'\b(\d{1,2})\s*[\/\-\.]\s*(\d{1,2})\s*[\/\-\.]\s*(\d{2,4})\b',
        # YYYY-MM-DD
        r'\b(\d{4})\s*[\/\-\.]\s*(\d{1,2})\s*[\/\-\.]\s*(\d{1,2})\b',
        # 28 janv. 2026
        r'\b(\d{1,2})\s+(janvier|février|mars|avril|mai|juin|juillet|août|septembre|octobre|novembre|décembre|janv|févr|mars|avr|mai|juin|juil|août|sept|oct|nov|déc)[^\d]*(\d{2,4})\b'
    ]

    # Montants : cherche les mots clés TOTAL, mais aussi les montants isolés
    # Gère les virgules, points, et espaces parasites (ex: 45 . 00)
    AMOUNT_PATTERNS = [
        # Mots clés explicites suivis d'un montant
        r'(?:total|montant|somme|payer|ttc)[\D]{0,15}?(\d+[\s\.,]*\d{0,2})(?:[\s]*(?:€|EUR|euro))?',
        # Montant avec symbole devise à la fin (ex: 45.00 €)
        r'\b(\d+[\s\.,]*\d{0,2})\s*(?:€|EUR|euro)\b',
        # Montant isolé en fin de ligne (souvent le total sur un ticket)
        r'^\s*(\d+[\s\.,]*\d{2})\s*$'
    ]

    # TVA : cherche les taux communs (20, 10, 5.5) et les mots clés
    TVA_PATTERNS = [
        r'(?:tva|taxe|vat)[\D]{0,10}?(\d+[\s\.,]*\d{2})',
        r'(\d+[\s\.,]*\d{2})\s*(?:%)',  # Montant suivi d'un %
        r'\b(20[\.,]00|10[\.,]00|5[\.,]50)\b' # Taux explicites
    ]

    # Code postal (France) pour deviner l'adresse
    ZIP_CODE_PATTERN = r'\b(\d{5})\b'

    # Mapping mois textuels -> chiffres
    MONTH_MAPPING = {
        'janvier': '01', 'janv': '01', 'février': '02', 'févr': '02', 'mars': '03',
        'avril': '04', 'avr': '04', 'mai': '05', 'juin': '06',
        'juillet': '07', 'juil': '07', 'août': '08', 'septembre': '09', 'sept': '09',
        'octobre': '10', 'oct': '10', 'novembre': '11', 'nov': '11', 'décembre': '12', 'déc': '12'
    }

    CURRENCY_MAPPING = {
        '€': 'EUR', 'euro': 'EUR', 'euros': 'EUR', 'eur': 'EUR',
        '$': 'USD', 'dollar': 'USD', 'usd': 'USD',
        '£': 'GBP', 'livre': 'GBP', 'gbp': 'GBP',
        'chf': 'CHF', 'franc': 'CHF'
    }

    def _clean_number_string(self, num_str: str) -> str:
        """Nettoie une chaîne numérique OCR (enlève espaces, remplace ',' par '.')"""
        if not num_str:
            return ""
        # Remplacer les confusions courantes OCR (O -> 0, l -> 1) si nécessaire
        # num_str = num_str.replace('O', '0').replace('o', '0')
        
        # Garder uniquement chiffres, point, virgule
        clean = re.sub(r'[^\d,\.]', '', num_str)
        # Remplacer virgule par point
        clean = clean.replace(',', '.')
        # Gérer les cas "1 000.00" où l'espace a sauté mais il reste un point bizarre
        try:
            # S'il y a plusieurs points, on garde le dernier comme décimale
            if clean.count('.') > 1:
                parts = clean.split('.')
                clean = "".join(parts[:-1]) + '.' + parts[-1]
            return clean
        except Exception:
            return clean

    def extract_date(self, text: str) -> Optional[str]:
        """Extrait la date la plus probable"""
        text_lower = text.lower()
        
        for pattern in self.DATE_PATTERNS:
            matches = re.finditer(pattern, text_lower, re.IGNORECASE)
            for match in matches:
                try:
                    groups = match.groups()
                    year, month, day = "", "", ""

                    # Cas JJ MM AAAA
                    if len(groups) >= 3:
                        # Détection basique basée sur la taille
                        g1, g2, g3 = groups[0], groups[1], groups[2]
                        
                        # Si le mois est textuel
                        if g2 in self.MONTH_MAPPING:
                            day, month_txt, year = g1, g2, g3
                            month = self.MONTH_MAPPING[month_txt]
                        # Si le premier groupe est l'année (YYYY-MM-DD)
                        elif len(g1) == 4:
                            year, month, day = g1, g2, g3
                        # Sinon standard (DD-MM-YYYY)
                        else:
                            day, month, year = g1, g2, g3

                        # Nettoyage
                        year = year.replace(' ', '')
                        if len(year) == 2:
                            year = '20' + year
                            
                        # Validation
                        try:
                            d = datetime(int(year), int(month), int(day))
                            # On ne prend pas les dates dans le futur ou trop vieilles (ex: numéros de tel)
                            if 2000 < d.year <= datetime.now().year + 1:
                                return d.strftime('%Y-%m-%d')
                        except ValueError:
                            continue
                except Exception as e:
                    logger.debug(f"Erreur parsing date: {e}")
                    continue
        return None

    def extract_amount(self, text: str) -> Optional[float]:
        """Extrait le montant total (le plus grand montant trouvé proche d'un mot clé 'Total')"""
        text_lower = text.lower()
        candidates = []

        for pattern in self.AMOUNT_PATTERNS:
            matches = re.finditer(pattern, text_lower, re.MULTILINE | re.IGNORECASE)
            for match in matches:
                try:
                    # On prend le groupe qui contient les chiffres
                    val_str = match.group(1)
                    val_clean = self._clean_number_string(val_str)
                    if val_clean:
                        val = float(val_clean)
                        # Filtre les montants aberrants (ex: codes barres, téléphones)
                        if 0 < val < 10000: 
                            candidates.append(val)
                except Exception:
                    continue
        
        if candidates:
            # Heuristique : le montant total est souvent le maximum trouvé
            return max(candidates)
        return None

    def extract_tva(self, text: str) -> Optional[float]:
        """Extrait la TVA"""
        text_lower = text.lower()
        candidates = []
        
        for pattern in self.TVA_PATTERNS:
            matches = re.finditer(pattern, text_lower, re.MULTILINE | re.IGNORECASE)
            for match in matches:
                try:
                    val_str = match.group(1)
                    val_clean = self._clean_number_string(val_str)
                    if val_clean:
                        val = float(val_clean)
                        if 0 < val < 1000: # TVA rarement > 1000 sur un ticket standard
                            candidates.append(val)
                except Exception:
                    continue
        
        if candidates:
            return max(candidates)
        return None

    def extract_currency(self, text: str) -> str:
        """Extrait la devise, défaut EUR"""
        text_lower = text.lower()
        for symbol, code in self.CURRENCY_MAPPING.items():
            if symbol in text_lower:
                return code
        return "EUR"

    def extract_merchant(self, text: str) -> Optional[str]:
        """Tente d'extraire le nom du magasin (souvent la 1ère ligne non vide)"""
        lines = [line.strip() for line in text.split('\n') if line.strip()]
        if lines:
            # On prend la première ligne, en ignorant les lignes trop courtes (bruit)
            for line in lines[:3]:
                if len(line) > 3 and not any(char.isdigit() for char in line):
                    return line
        return None

    def extract_all(self, text: str) -> Dict[str, Any]:
        """Extrait toutes les données"""
        return {
            'date': self.extract_date(text),
            'amount': self.extract_amount(text),
            'tva': self.extract_tva(text),
            'currency': self.extract_currency(text),
            'merchant': self.extract_merchant(text),
            'raw_text': text
        }