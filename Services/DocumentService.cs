using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace DocArchive.Services
{
    public class DocumentService
    {
        private readonly HttpClient _http;

        public DocumentService(HttpClient http)
        {
            _http = http;
        }

        public async Task<HttpResponseMessage> UploadDocument(MultipartFormDataContent content)
        {
            return await _http.PostAsync("api/documents/upload", content);
        }

        public async Task<List<string>> GetDocuments()
        {
            var token = await SecureStorage.GetAsync("jwt_token");

            _http.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            return await _http.GetFromJsonAsync<List<string>>("api/documents");
        }
    }
}
