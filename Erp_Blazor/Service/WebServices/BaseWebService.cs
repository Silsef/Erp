using Blazored.Toast.Services; // Ajouter l'using
using Erp_Blazor.Service.Interfaces;
using System.Net.Http.Json;
using System.Text.Json;

public class BaseWebService<TRead, TCreate, TUpdate> : ICrudService<TRead, TCreate, TUpdate>
    where TRead : class
    where TCreate : class
    where TUpdate : class
{
    protected readonly HttpClient _client;
    protected readonly string _endpoint;
    protected readonly IToastService _toastService; // Ajouter le service

    // Modifier le constructeur pour accepter le ToastService
    public BaseWebService(HttpClient client, string endpoint, IToastService toastService)
    {
        _client = client;
        _endpoint = endpoint;
        _toastService = toastService;
    }

    // --- LECTURE ---
    public async Task<List<TRead>> GetAll()
    {
        try
        {
            var result = await _client.GetFromJsonAsync<List<TRead>>(_endpoint + "/GetAll");
            return result ?? new List<TRead>();
        }
        catch (Exception ex) // On attrape tout pour notifier
        {
            Console.WriteLine($"Erreur HTTP lors du GetAll: {ex.Message}");
            _toastService.ShowError("Impossible de charger les données."); // Notification Erreur
            throw; // On relance l'erreur pour ne pas casser la logique existante
        }
    }

    public async Task<TRead> GetByID(int id)
    {
        try
        {
            var result = await _client.GetFromJsonAsync<TRead>($"{_endpoint}/GetById/{id}");
            if (result == null) throw new Exception("Non trouvé");
            return result;
        }
        catch (Exception ex)
        {
            _toastService.ShowError($"Erreur de chargement: {ex.Message}");
            throw;
        }
    }

    // --- CREATION ---
    public async Task<TRead> Post(TCreate item)
    {
        try
        {
            var response = await _client.PostAsJsonAsync(_endpoint + "/Create", item);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();

                _toastService.ShowError("Erreur lors de la création. Vérifiez les données.");
                throw new Exception($"Erreur création: {response.StatusCode}");
            }

            var result = await response.Content.ReadFromJsonAsync<TRead>();

            // SUCCES !
            _toastService.ShowSuccess("Création effectuée avec succès !");

            return result!;
        }
        catch (Exception ex)
        {
            _toastService.ShowError(ex.Message); 
            throw;
        }
    }

    // --- MODIFICATION ---
    public async Task<TRead> Put(int id, TUpdate item)
    {
        try
        {
            var response = await _client.PutAsJsonAsync($"{_endpoint}/Update/{id}", item);

            if (!response.IsSuccessStatusCode)
            {
                _toastService.ShowError("Erreur lors de la mise à jour.");
                throw new Exception($"Erreur modification: {response.StatusCode}");
            }

            var result = await response.Content.ReadFromJsonAsync<TRead>();

            // SUCCES !
            _toastService.ShowSuccess("Mise à jour effectuée !");

            return result!;
        }
        catch (Exception ex)
        {
            _toastService.ShowError($"Echec de la modification : {ex.Message}");
            throw;
        }
    }

    // --- SUPPRESSION ---
    public async Task Delete(int id)
    {
        try
        {
            var response = await _client.DeleteAsync($"{_endpoint}/{id}");

            if (!response.IsSuccessStatusCode)
            {
                _toastService.ShowError("Impossible de supprimer cet élément.");
                throw new Exception($"Erreur suppression: {response.StatusCode}");
            }

            // SUCCES !
            _toastService.ShowSuccess("Élément supprimé.");
        }
        catch (Exception ex)
        {
            _toastService.ShowError($"Erreur : {ex.Message}");
            throw;
        }
    }
}