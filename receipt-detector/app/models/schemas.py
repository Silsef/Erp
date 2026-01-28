from pydantic import BaseModel, Field
from typing import Optional, List
from datetime import datetime


class ReceiptInfo(BaseModel):
    """Informations extraites d'un ticket de caisse"""
    date: Optional[str] = Field(None, description="Date du ticket (format détecté)")
    amount: Optional[float] = Field(None, description="Montant total")
    currency: Optional[str] = Field(None, description="Devise (EUR, USD, etc.)")
    raw_text: Optional[str] = Field(None, description="Texte brut extrait par OCR")
    confidence: Optional[float] = Field(None, description="Confiance globale de l'extraction")


class BoundingBox(BaseModel):
    """Coordonnées d'une zone détectée"""
    x: int
    y: int
    width: int
    height: int


class DetectedReceipt(BaseModel):
    """Un ticket détecté avec ses informations"""
    receipt_info: ReceiptInfo
    bounding_box: Optional[BoundingBox] = Field(None, description="Zone du ticket dans l'image")
    ticket_number: int = Field(description="Numéro du ticket dans l'image (si plusieurs)")


class ReceiptResponse(BaseModel):
    """Réponse de l'API pour l'analyse de tickets"""
    success: bool
    message: str
    receipts: List[DetectedReceipt] = Field(default_factory=list)
    total_receipts_found: int = 0


class HealthResponse(BaseModel):
    """Réponse pour le health check"""
    status: str
    timestamp: datetime
