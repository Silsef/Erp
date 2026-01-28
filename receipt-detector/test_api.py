"""
Script de test pour l'API Receipt Detector
Usage: python test_api.py <chemin_vers_image>
"""

import requests
import sys
import json
from pathlib import Path


def test_health():
    """Test le health check"""
    print("🔍 Test du health check...")
    response = requests.get("http://localhost:8000/health")
    
    if response.status_code == 200:
        print("✅ Health check OK")
        print(f"   Réponse: {response.json()}")
    else:
        print(f"❌ Health check échoué: {response.status_code}")


def test_analyze_receipt(image_path):
    """Test l'analyse d'un ticket"""
    print(f"\n🔍 Test de l'analyse de l'image: {image_path}")
    
    if not Path(image_path).exists():
        print(f"❌ Fichier introuvable: {image_path}")
        return
    
    with open(image_path, 'rb') as f:
        files = {'file': f}
        response = requests.post(
            "http://localhost:8000/api/v1/analyze",
            files=files
        )
    
    if response.status_code == 200:
        data = response.json()
        print("✅ Analyse réussie!")
        print(f"\n📊 Résultats:")
        print(f"   Success: {data['success']}")
        print(f"   Message: {data['message']}")
        print(f"   Tickets trouvés: {data['total_receipts_found']}")
        
        for idx, receipt in enumerate(data['receipts'], 1):
            info = receipt['receipt_info']
            print(f"\n   Ticket #{idx}:")
            print(f"      📅 Date: {info['date']}")
            print(f"      💰 Montant: {info['amount']}")
            print(f"      💱 Devise: {info['currency']}")
            print(f"      📈 Confiance: {info['confidence']:.2%}")
            
            if receipt.get('bounding_box'):
                bbox = receipt['bounding_box']
                print(f"      📍 Position: x={bbox['x']}, y={bbox['y']}, "
                      f"w={bbox['width']}, h={bbox['height']}")
    else:
        print(f"❌ Analyse échouée: {response.status_code}")
        print(f"   Erreur: {response.text}")


def test_ocr_only(image_path):
    """Test l'OCR uniquement"""
    print(f"\n🔍 Test de l'OCR seul sur: {image_path}")
    
    if not Path(image_path).exists():
        print(f"❌ Fichier introuvable: {image_path}")
        return
    
    with open(image_path, 'rb') as f:
        files = {'file': f}
        response = requests.post(
            "http://localhost:8000/api/v1/ocr-only",
            files=files
        )
    
    if response.status_code == 200:
        data = response.json()
        print("✅ OCR réussi!")
        print(f"\n📝 Texte extrait (confiance: {data['confidence']:.2%}):")
        print("-" * 50)
        print(data['text'])
        print("-" * 50)
    else:
        print(f"❌ OCR échoué: {response.status_code}")
        print(f"   Erreur: {response.text}")


if __name__ == "__main__":
    print("=" * 60)
    print("🧾 TEST DE L'API RECEIPT DETECTOR")
    print("=" * 60)
    
    # Test health
    test_health()
    
    # Test avec une image si fournie
    if len(sys.argv) > 1:
        image_path = sys.argv[1]
        test_analyze_receipt(image_path)
        
        # Demander si on veut tester l'OCR seul
        test_ocr = input("\n❓ Voulez-vous tester l'OCR seul ? (o/n): ")
        if test_ocr.lower() == 'o':
            test_ocr_only(image_path)
    else:
        print("\n💡 Usage: python test_api.py <chemin_vers_image>")
        print("   Exemple: python test_api.py ticket.jpg")
    
    print("\n" + "=" * 60)
    print("✅ Tests terminés!")
    print("=" * 60)
