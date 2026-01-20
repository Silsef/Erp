using Erp_Blazor.Service.Interfaces;
using System.Net.Http.Json;

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
        return await _client.GetFromJsonAsync<List<TRead>>(_endpoint);
    }

    public async Task<TRead> GetByID(int id)
    {
        return await _client.GetFromJsonAsync<TRead>($"{_endpoint}/{id}");
    }

    // --- ECRITURE (Avec DTO spécifique) ---
    public async Task<TRead> Post(TCreate item)
    {
        var response = await _client.PostAsJsonAsync(_endpoint, item);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<TRead>();
    }

    // --- MODIFICATION (Avec DTO spécifique) ---
    public async Task<TRead> Put(int id, TUpdate item)
    {
        var response = await _client.PutAsJsonAsync($"{_endpoint}/{id}", item);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<TRead>();
    }

    // --- SUPPRESSION ---
    public async Task Delete(int id)
    {
        var response = await _client.DeleteAsync($"{_endpoint}/{id}");
        response.EnsureSuccessStatusCode();
    }
}