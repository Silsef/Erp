# Exemples d'utilisation de l'API Receipt OCR

## Exemple 1 : Python avec requests

```python
import requests
import json

API_URL = "http://localhost:8000"

def analyze_receipt(image_path):
    """Analyse un ticket de caisse"""
    with open(image_path, 'rb') as f:
        files = {'file': (image_path, f, 'image/jpeg')}
        response = requests.post(f"{API_URL}/analyze", files=files)
    
    if response.status_code == 200:
        data = response.json()
        print(json.dumps(data, indent=2))
        return data
    else:
        print(f"Erreur: {response.status_code}")
        print(response.text)
        return None

# Utilisation
result = analyze_receipt("mon_ticket.jpg")
if result:
    print(f"Montant: {result['montant_ttc']} {result['devise']}")
```

## Exemple 2 : Node.js avec axios

```javascript
const axios = require('axios');
const FormData = require('form-data');
const fs = require('fs');

async function analyzeReceipt(imagePath) {
    const form = new FormData();
    form.append('file', fs.createReadStream(imagePath));
    
    try {
        const response = await axios.post('http://localhost:8000/analyze', form, {
            headers: form.getHeaders()
        });
        
        console.log('Résultat:', response.data);
        return response.data;
    } catch (error) {
        console.error('Erreur:', error.response?.data || error.message);
        return null;
    }
}

// Utilisation
analyzeReceipt('mon_ticket.jpg');
```

## Exemple 3 : cURL

```bash
# Analyser un ticket
curl -X POST "http://localhost:8000/analyze" \
  -H "Content-Type: multipart/form-data" \
  -F "file=@ticket.jpg" \
  | jq '.'

# Avec jq pour formater la sortie
curl -X POST "http://localhost:8000/analyze" \
  -F "file=@ticket.jpg" \
  | jq '{montant: .montant_ttc, devise: .devise, date: .date}'
```

## Exemple 4 : PHP

```php
<?php
$apiUrl = 'http://localhost:8000/analyze';
$imagePath = 'ticket.jpg';

$cfile = new CURLFile($imagePath, 'image/jpeg', 'file');
$data = array('file' => $cfile);

$ch = curl_init();
curl_setopt($ch, CURLOPT_URL, $apiUrl);
curl_setopt($ch, CURLOPT_POST, 1);
curl_setopt($ch, CURLOPT_POSTFIELDS, $data);
curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);

$response = curl_exec($ch);
curl_close($ch);

$result = json_decode($response, true);
print_r($result);
?>
```

## Exemple 5 : Traitement par lot (Python)

```python
import requests
import os
from pathlib import Path

def batch_analyze(directory):
    """Analyse tous les tickets dans un répertoire"""
    results = []
    
    for file_path in Path(directory).glob('*.jpg'):
        print(f"Traitement de {file_path.name}...")
        
        with open(file_path, 'rb') as f:
            files = {'file': f}
            response = requests.post('http://localhost:8000/analyze', files=files)
        
        if response.status_code == 200:
            data = response.json()
            data['filename'] = file_path.name
            results.append(data)
            print(f"  ✓ Montant: {data['montant_ttc']} {data['devise']}")
        else:
            print(f"  ✗ Erreur: {response.status_code}")
    
    return results

# Utilisation
results = batch_analyze('./tickets')
print(f"\n{len(results)} tickets analysés avec succès")
```

## Exemple 6 : Validation des résultats

```python
import requests

def validate_result(result):
    """Valide les résultats de l'analyse"""
    errors = []
    warnings = []
    
    if result['montant_ttc'] is None:
        errors.append("Montant TTC non trouvé")
    elif result['montant_ttc'] <= 0:
        errors.append("Montant TTC invalide")
    
    if result['date'] is None:
        warnings.append("Date non trouvée")
    
    if result['devise'] is None:
        warnings.append("Devise non trouvée")
    
    if result['confidence'] == 'low':
        warnings.append("Faible confiance dans l'extraction")
    
    return {
        'valid': len(errors) == 0,
        'errors': errors,
        'warnings': warnings
    }

# Utilisation
with open('ticket.jpg', 'rb') as f:
    response = requests.post('http://localhost:8000/analyze', files={'file': f})
    result = response.json()

validation = validate_result(result)
if validation['valid']:
    print("✓ Ticket valide")
else:
    print("✗ Erreurs:", validation['errors'])

if validation['warnings']:
    print("⚠ Avertissements:", validation['warnings'])
```
