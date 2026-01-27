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

    public BaseWebService(HttpClient client, string endpoint)
    {
        _client = client;
        _endpoint = endpoint;
    }

    // --- LECTURE ---
    public async Task<List<TRead>> GetAll()
    {
        try
        {
            var result = await _client.GetFromJsonAsync<List<TRead>>(_endpoint + "/GetAll");
            return result ?? new List<TRead>();
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Erreur HTTP lors du GetAll: {ex.Message}");
            throw new Exception("Erreur lors de la récupération des données", ex);
        }
    }

    public async Task<TRead> GetByID(int id)
    {
        try
        {
            var result = await _client.GetFromJsonAsync<TRead>($"{_endpoint}/GetById/{id}");
            if (result == null)
                throw new Exception($"Élément avec l'ID {id} non trouvé");
            return result;
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Erreur HTTP lors du GetByID: {ex.Message}");
            throw new Exception($"Erreur lors de la récupération de l'élément {id}", ex);
        }
    }

    // --- CREATION (Avec validation) ---
    public async Task<TRead> Post(TCreate item)
    {
        try
        {
            var response = await _client.PostAsJsonAsync(_endpoint + "/Create", item);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Erreur lors de la création: {response.StatusCode} - {errorContent}");

                // Tenter de parser les erreurs de validation
                try
                {
                    var validationErrors = JsonSerializer.Deserialize<Dictionary<string, string[]>>(
                        errorContent,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                    );

                    if (validationErrors != null && validationErrors.ContainsKey("errors"))
                    {
                        var errors = string.Join(", ", validationErrors["errors"]);
                        throw new Exception($"Erreurs de validation: {errors}");
                    }
                }
                catch (JsonException)
                {
                    // Si ce n'est pas un format de validation connu
                }

                throw new Exception($"Erreur lors de la création: {response.StatusCode}");
            }

            var result = await response.Content.ReadFromJsonAsync<TRead>();
            if (result == null)
                throw new Exception("La création a réussi mais aucune donnée n'a été retournée");

            return result;
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Erreur HTTP lors du Post: {ex.Message}");
            throw new Exception("Erreur lors de la création de l'élément", ex);
        }
    }

    // --- MODIFICATION (Avec validation) ---
    public async Task<TRead> Put(int id, TUpdate item)
    {
        try
        {
            var response = await _client.PutAsJsonAsync($"{_endpoint}/Update/{id}", item);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Erreur lors de la modification: {response.StatusCode} - {errorContent}");
                throw new Exception($"Erreur lors de la modification: {response.StatusCode}");
            }

            var result = await response.Content.ReadFromJsonAsync<TRead>();
            if (result == null)
                throw new Exception("La modification a réussi mais aucune donnée n'a été retournée");

            return result;
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Erreur HTTP lors du Put: {ex.Message}");
            throw new Exception($"Erreur lors de la modification de l'élément {id}", ex);
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
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Erreur lors de la suppression: {response.StatusCode} - {errorContent}");
                throw new Exception($"Erreur lors de la suppression: {response.StatusCode}");
            }
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Erreur HTTP lors du Delete: {ex.Message}");
            throw new Exception($"Erreur lors de la suppression de l'élément {id}", ex);
        }
    }
}