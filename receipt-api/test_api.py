"""
Script de test pour l'API Receipt OCR
"""
import requests
import sys

API_URL = "http://localhost:8000"

def test_health():
    """Test du endpoint health"""
    print("🔍 Test du endpoint /health...")
    try:
        response = requests.get(f"{API_URL}/health")
        if response.status_code == 200:
            print("✅ API opérationnelle")
            print(f"   {response.json()}")
            return True
        else:
            print(f"❌ Erreur: {response.status_code}")
            return False
    except requests.exceptions.ConnectionError:
        print("❌ Impossible de se connecter à l'API")
        print("   Assurez-vous que l'API est démarrée: python main.py")
        return False

def test_analyze(image_path):
    """Test du endpoint analyze avec une image"""
    print(f"\n🔍 Test d'analyse d'image: {image_path}...")
    try:
        with open(image_path, 'rb') as f:
            files = {'file': (image_path, f, 'image/jpeg')}
            response = requests.post(f"{API_URL}/analyze", files=files)
        
        if response.status_code == 200:
            result = response.json()
            print("✅ Analyse réussie")
            print(f"   💶 Montant TTC: {result.get('montant_ttc', 'Non trouvé')} {result.get('devise', '')}")
            print(f"   📊 TVA: {result.get('tva', 'Non trouvé')}")
            print(f"   📅 Date: {result.get('date', 'Non trouvée')}")
            print(f"   🎯 Confiance: {result.get('confidence', 'N/A')}")
            return True
        else:
            print(f"❌ Erreur: {response.status_code}")
            print(f"   {response.json()}")
            return False
    except FileNotFoundError:
        print(f"❌ Fichier non trouvé: {image_path}")
        return False
    except Exception as e:
        print(f"❌ Erreur: {e}")
        return False

def main():
    print("=" * 50)
    print("   TEST DE L'API RECEIPT OCR")
    print("=" * 50)
    
    # Test de santé
    if not test_health():
        sys.exit(1)
    
    # Test d'analyse si une image est fournie
    if len(sys.argv) > 1:
        image_path = sys.argv[1]
        test_analyze(image_path)
    else:
        print("\n💡 Pour tester l'analyse d'image:")
        print("   python test_api.py chemin/vers/votre/ticket.jpg")

if __name__ == "__main__":
    main()
