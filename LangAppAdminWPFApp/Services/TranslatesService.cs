using LangApp.Admin.WPF.Models;
using LangApp.Admin.WPF.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace LangApp.Admin.WPF.Services
{
    public class TranslatesService : ITranslatesService
    {
        private readonly HttpClient _client;
        private readonly ITokenStorage _tokenStorage;

        public TranslatesService(HttpClient client, ITokenStorage tokenStorage)
        {
            _client = client;
            _tokenStorage = tokenStorage;
        }

        public async Task<List<Translate>> GetTranslatesAsync(CancellationToken cancellationToken)
        {
            return await _client.GetFromJsonAsync<List<Translate>>("api/Translates/get-all-translations", 
                cancellationToken) ?? [];
        }
        public async Task<string> CreateTranslatesAsync(CancellationToken cancellationToken)
        {
            try
            {
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue
                    ("Bearer", _tokenStorage.AccessToken);

                var response = await _client.PostAsync("api/Translates/create-list-translates", 
                    content: null, cancellationToken);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync(cancellationToken: cancellationToken);

                    return result ?? throw new Exception("Translates didn't create");
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync(cancellationToken);
                    throw new Exception($"Create translates failed: {response.StatusCode} - {errorMessage}");
                }

            }
            catch (Exception ex)
            {
                throw new Exception($"Create translates failed: {ex.Message}");

            }
        }
    }
}
