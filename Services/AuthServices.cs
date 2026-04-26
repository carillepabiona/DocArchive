using System.Net.Http.Json;
using Microsoft.Maui.Storage;
using DocArchive.Models;

namespace DocArchive.Services
{
    public class AuthService
    {
        private readonly HttpClient _http;

        private const string TOKEN_KEY = "auth_token";

        public AuthService(HttpClient http)
        {
            _http = http;
        }

        // =========================
        // LOGIN
        // =========================
        public async Task<string?> Login(string username, string password)
        {
            var request = new
            {
                Username = username,
                Password = password
            };

            var response = await _http.PostAsJsonAsync("api/login", request);

            if (!response.IsSuccessStatusCode)
                return null;

            var result = await response.Content.ReadFromJsonAsync<LoginResponse>();

            if (!string.IsNullOrEmpty(result?.Token))
            {
                // ✅ SAVE TOKEN
                await SecureStorage.SetAsync(TOKEN_KEY, result.Token);
            }

            return result?.Token;
        }

        // =========================
        // GET TOKEN
        // =========================
        public async Task<string?> GetToken()
        {
            return await SecureStorage.GetAsync(TOKEN_KEY);
        }

        // =========================
        // LOGOUT
        // =========================
        public void Logout()
        {
            SecureStorage.Remove(TOKEN_KEY);
        }
    }
}