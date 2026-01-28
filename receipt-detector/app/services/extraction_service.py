import re
from typing import Optional, Dict, Any
from datetime import datetime
import logging

logger = logging.getLogger(__name__)


class ExtractionService:
    """Service pour extraire les informations structurées du texte OCR"""
    
    # Patterns regex pour les dates (formats français courants)
    DATE_PATTERNS = [
        # Format: 28/01/2026, 28-01-2026, 28.01.2026
        r'\b(\d{1,2})[\/\-\.](\d{1,2})[\/\-\.](\d{2,4})\b',
        # Format: 28 janvier 2026, 28 janv 2026
        r'\b(\d{1,2})\s+(janvier|février|mars|avril|mai|juin|juillet|août|septembre|octobre|novembre|décembre|janv|févr|mars|avr|mai|juin|juil|août|sept|oct|nov|déc)\.?\s+(\d{4})\b',
        # Format: 2026-01-28
        r'\b(\d{4})[\/\-\.](\d{1,2})[\/\-\.](\d{1,2})\b',
    ]
    
    # Patterns pour les montants
    AMOUNT_PATTERNS = [
        # Format: TOTAL: 45.50, Total : 45,50 EUR
        r'(?:total|montant|somme|à\s+payer)[\s:]*([0-9]+[,\.][0-9]{2})\s*(?:€|EUR|euro)?',
        # Format: 45.50€, 45,50 €, 45.50 EUR
        r'\b([0-9]+[,\.][0-9]{2})\s*(?:€|EUR|euro)\b',
        # Format juste avant la fin (dernière ligne avec montant)
        r'\b([0-9]+[,\.][0-9]{2})\s*$',
    ]
    
    # Patterns pour les devises
    CURRENCY_PATTERNS = [
        r'\b(EUR|€|euro|euros)\b',
        r'\b(USD|\$|dollar|dollars)\b',
        r'\b(GBP|£|pound|livre)\b',
        r'\b(CHF|franc)\b',
    ]
    
    # Mapping devise -> code ISO
    CURRENCY_MAPPING = {
        '€': 'EUR',
        'euro': 'EUR',
        'euros': 'EUR',
        'EUR': 'EUR',
        '$': 'USD',
        'dollar': 'USD',
        'dollars': 'USD',
        'USD': 'USD',
        '£': 'GBP',
        'pound': 'GBP',
        'livre': 'GBP',
        'GBP': 'GBP',
        'franc': 'CHF',
        'CHF': 'CHF',
    }
    
    # Mapping mois français -> numéro
    MONTH_MAPPING = {
        'janvier': '01', 'janv': '01',
        'février': '02', 'févr': '02',
        'mars': '03',
        'avril': '04', 'avr': '04',
        'mai': '05',
        'juin': '06',
        'juillet': '07', 'juil': '07',
        'août': '08',
        'septembre': '09', 'sept': '09',
        'octobre': '10', 'oct': '10',
        'novembre': '11', 'nov': '11',
        'décembre': '12', 'déc': '12',
    }
    
    def extract_date(self, text: str) -> Optional[str]:
        """
        Extrait la date du texte OCR
        
        Args:
            text: Texte extrait par OCR
            
        Returns:
            Date au format ISO (YYYY-MM-DD) ou None
        """
        text_lower = text.lower()
        
        for pattern in self.DATE_PATTERNS:
            matches = re.finditer(pattern, text_lower, re.IGNORECASE)
            for match in matches:
                try:
                    groups = match.groups()
                    
                    # Format numérique (DD/MM/YYYY ou YYYY/MM/DD)
                    if len(groups) == 3 and all(g.isdigit() or g in ['/', '-', '.'] for g in groups):
                        if len(groups[0]) == 4:  # Format YYYY-MM-DD
                            year, month, day = groups
                        else:  # Format DD-MM-YYYY
                            day, month, year = groups
                        
                        # Gérer année sur 2 chiffres
                        if len(year) == 2:
                            year = '20' + year if int(year) < 50 else '19' + year
                        
                        # Valider la date
                        date_obj = datetime(int(year), int(month), int(day))
                        return date_obj.strftime('%Y-%m-%d')
                    
                    # Format avec mois en lettres
                    elif len(groups) == 3 and groups[1] in self.MONTH_MAPPING:
                        day, month_name, year = groups
                        month = self.MONTH_MAPPING[month_name]
                        date_obj = datetime(int(year), int(month), int(day))
                        return date_obj.strftime('%Y-%m-%d')
                        
                except (ValueError, IndexError) as e:
                    logger.debug(f"Date invalide ignorée: {match.group()} - {e}")
                    continue
        
        logger.warning("Aucune date valide trouvée")
        return None
    
    def extract_amount(self, text: str) -> Optional[float]:
        """
        Extrait le montant total du texte OCR
        
        Args:
            text: Texte extrait par OCR
            
        Returns:
            Montant en float ou None
        """
        text_lower = text.lower()
        amounts = []
        
        for pattern in self.AMOUNT_PATTERNS:
            matches = re.finditer(pattern, text_lower, re.IGNORECASE | re.MULTILINE)
            for match in matches:
                try:
                    amount_str = match.group(1)
                    # Remplacer virgule par point
                    amount_str = amount_str.replace(',', '.')
                    amount = float(amount_str)
                    amounts.append(amount)
                except (ValueError, IndexError) as e:
                    logger.debug(f"Montant invalide ignoré: {match.group()} - {e}")
                    continue
        
        if amounts:
            # Retourner le montant le plus élevé (probablement le total)
            max_amount = max(amounts)
            logger.info(f"Montant extrait: {max_amount}")
            return max_amount
        
        logger.warning("Aucun montant trouvé")
        return None
    
    def extract_currency(self, text: str) -> Optional[str]:
        """
        Extrait la devise du texte OCR
        
        Args:
            text: Texte extrait par OCR
            
        Returns:
            Code devise ISO (EUR, USD, etc.) ou None
        """
        text_lower = text.lower()
        
        for pattern in self.CURRENCY_PATTERNS:
            match = re.search(pattern, text_lower, re.IGNORECASE)
            if match:
                currency_raw = match.group(1).lower()
                currency_code = self.CURRENCY_MAPPING.get(currency_raw)
                if currency_code:
                    logger.info(f"Devise extraite: {currency_code}")
                    return currency_code
        
        # Par défaut, supposer EUR pour la France
        logger.info("Aucune devise trouvée, défaut: EUR")
        return "EUR"
    
    def extract_all(self, text: str) -> Dict[str, Any]:
        """
        Extrait toutes les informations du texte OCR
        
        Args:
            text: Texte extrait par OCR
            
        Returns:
            Dictionnaire avec date, montant, devise
        """
        return {
            'date': self.extract_date(text),
            'amount': self.extract_amount(text),
            'currency': self.extract_currency(text),
            'raw_text': text
        }
