using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

public class ApiService
{
    private readonly HttpClient _httpClient;

    public ApiService()
    {
        _httpClient = new HttpClient();
    }

    public async Task<string> LoginAsync()
    {
        var login = new LoginRequest
        {
            Username = "user@aemenersol.com",
            Password = "Test@123"
        };

        var content =
            new StringContent(
                JsonSerializer.Serialize(login),
                Encoding.UTF8,
                "application/json");

        var response =
            await _httpClient.PostAsync(
                "YOUR_LOGIN_ENDPOINT",
                content);

        response.EnsureSuccessStatusCode();

        var json =
            await response.Content.ReadAsStringAsync();

        var loginResult =
            JsonSerializer.Deserialize<LoginResponse>(
                json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

        return loginResult.Token;
    }

    public async Task<System.Text.Json.JsonDocument> GetPlatformWellActual(
        string token)
    {
        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue(
                "Bearer",
                token);

        var response =
            await _httpClient.GetAsync(
                "YOUR_GETPLATFORMWELLACTUAL_ENDPOINT");

        response.EnsureSuccessStatusCode();

        var json =
            await response.Content.ReadAsStringAsync();

        return JsonDocument.Parse(json);
    }
}