using Newtonsoft.Json;
using SecretWords.WebApp.Models;
using System.Net.Http;
using System.Text;

/// <summary>
/// Service that makes use of the API 
/// </summary>
public class ApiService
{
    private readonly HttpClient _client;

    public ApiService(HttpClient client)
    {
        _client = client;
    }

    /// <summary>
    /// Get a list of all the words
    /// </summary>
    /// <returns></returns>
    public async Task<List<SensitiveWordViewModel>> GetAllWordsAsync()
    {
        var response = await _client.GetAsync("/api/sensitivewords");
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<List<SensitiveWordViewModel>>(json)
               ?? new List<SensitiveWordViewModel>();
    }

    /// <summary>
    /// Call the API to create a new word
    /// </summary>
    /// <param name="word"></param>
    /// <returns></returns>
    public async Task<SensitiveWordViewModel?> CreateWordAsync(string word)
    {
        var content = new StringContent(
            JsonConvert.SerializeObject(new { Word = word }),
            Encoding.UTF8,
            "application/json"
        );

        var response = await _client.PostAsync("/api/sensitivewords", content);
        if (!response.IsSuccessStatusCode) return null;

        var json = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<SensitiveWordViewModel>(json);
    }

    /// <summary>
    /// Update a word
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task UpdateWordAsync(SensitiveWordViewModel model)
    {
        var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
        await _client.PutAsync($"/api/sensitivewords/{model.Id}", content);
    }

    /// <summary>
    /// Delete a word
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task DeleteWordAsync(int id)
    {
        await _client.DeleteAsync($"/api/sensitivewords/{id}");
    }

    /// <summary>
    /// Bloop a message
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public async Task<string> BloopMessageAsync(string message)
    {
        var content = new StringContent(
            JsonConvert.SerializeObject(new { Message = message }),
            Encoding.UTF8,
            "application/json"
        );

        var response = await _client.PostAsync("/api/bloop", content);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        var bloop = JsonConvert.DeserializeObject<BloopResponseViewModel>(json);
        return bloop?.Message ?? message;
    }
}
