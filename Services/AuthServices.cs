using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocArchive.Models;

namespace DocArchive.Services
{
    using System.Net.Http.Json;

    public class AuthService
    {
        private readonly HttpClient _http;

        public AuthService()
        {
            _http = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:5250/")
            };
        }

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

            return result?.Token;
        }
    }
}
