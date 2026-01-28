"""
Script de test simple pour Receipt Detector API
Usage: python test_simple.py chemin/vers/ticket.jpg
"""

import requests
import sys
import json
from pathlib import Path


def test_api(image_path):
    """Test l'API avec une image"""
    
    if not Path(image_path).exists():
        print(f"❌ Fichier introuvable: {image_path}")
        return
    
    print(f"🔍 Test de l'image: {image_path}")
    print("-" * 60)
    
    try:
        # Health check
        print("1️⃣ Vérification de l'API...")
        response = requests.get("http://localhost:8000/health", timeout=5)
        if response.status_code == 200:
            print("   ✅ API accessible")
        else:
            print("   ❌ API non accessible")
            return
        
        # Analyse du ticket
        print("\n2️⃣ Analyse du ticket...")
        with open(image_path, 'rb') as f:
            files = {'file': f}
            response = requests.post(
                "http://localhost:8000/api/v1/analyze",
                files=files,
                timeout=60
            )
        
        if response.status_code == 200:
            data = response.json()
            
            if data['success']:
                print(f"   ✅ Analyse réussie!")
                print(f"\n📊 Résultats:")
                print(f"   Tickets trouvés: {data['total_receipts_found']}")
                
                for receipt in data['receipts']:
                    info = receipt['receipt_info']
                    print(f"\n   📄 Ticket #{receipt['ticket_number']}:")
                    print(f"      📅 Date: {info['date'] or 'Non détectée'}")
                    print(f"      💰 Montant: {info['amount'] or 'Non détecté'} {info['currency'] or ''}")
                    print(f"      🧾 TVA: {info['tva'] or 'Non détectée'} {info['currency'] or ''}")
                    print(f"      💱 Devise: {info['currency'] or 'Non détectée'}")
                    print(f"      📈 Confiance: {info['confidence']:.1%}")
                    
                    if info.get('raw_text'):
                        print(f"\n      📝 Texte OCR (extrait):")
                        lines = info['raw_text'].split('\n')[:5]
                        for line in lines:
                            print(f"         {line}")
                        if len(info['raw_text'].split('\n')) > 5:
                            print(f"         ... ({len(info['raw_text'].split('\n')) - 5} lignes supplémentaires)")
            else:
                print(f"   ⚠️  {data['message']}")
        else:
            print(f"   ❌ Erreur {response.status_code}")
            print(f"   {response.text}")
    
    except requests.exceptions.ConnectionError:
        print("\n❌ Impossible de se connecter à l'API")
        print("   Assurez-vous que l'API est lancée avec:")
        print("   uvicorn app.main:app --reload")
    except requests.exceptions.Timeout:
        print("\n❌ Timeout - L'analyse a pris trop de temps")
    except Exception as e:
        print(f"\n❌ Erreur: {e}")
    
    print("\n" + "-" * 60)


if __name__ == "__main__":
    print("=" * 60)
    print("🧾 TEST RECEIPT DETECTOR API")
    print("=" * 60)
    print()
    
    if len(sys.argv) < 2:
        print("💡 Usage: python test_simple.py chemin/vers/ticket.jpg")
        print()
        print("Exemple:")
        print("   python test_simple.py ticket.jpg")
        print()
    else:
        image_path = sys.argv[1]
        test_api(image_path)
    
    print("=" * 60)
