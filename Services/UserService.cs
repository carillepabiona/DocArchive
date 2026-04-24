using DocArchive.DTOs;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DocArchive.Services
{
    public class UserService
    {
        private readonly HttpClient _http;

        public UserService(HttpClient http)
        {
            _http = http;
        }

      
        public async Task<bool> UpdateProfile(string contactNumber, string address)
        {
            try
            {
                var token = await SecureStorage.GetAsync("jwt_token");

                if (string.IsNullOrWhiteSpace(token))
                    throw new Exception("JWT token not found.");

                // IMPORTANT: avoid duplicate headers bug
                _http.DefaultRequestHeaders.Authorization = null;
                _http.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);

                var data = new
                {
                    ContactNumber = contactNumber,
                    Address = address
                };

                var json = JsonSerializer.Serialize(data);

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _http.PutAsync("api/users/update-profile", content);

                Console.WriteLine($"STATUS: {response.StatusCode}");

                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"API ERROR: {error}");
                }

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"SERVICE ERROR: {ex.Message}");
                return false;
            }
        }
    }
}