using LangApp.Admin.WPF.DTOs.Requests;
using LangApp.Admin.WPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LangApp.Admin.WPF.Services
{
    public class LanguageService : ILanguageService
    {
        private readonly HttpClient _client;
        private readonly ITokenStorage _tokenStorage;

        public LanguageService (HttpClient client, ITokenStorage tokenStorage)
        {
            _client = client;
            _tokenStorage = tokenStorage;
        }
        public async Task<List<Language>> GetLanguagesAsync(CancellationToken cancellationToken)
        {
            return await _client.GetFromJsonAsync<List<Language>>("api/AdminLangCodes/get-languages", 
                cancellationToken)
                ?? [];
        }

        public async Task<string> AddNewLanguageAsync
            (CreateLanguageDTO createLanguageDTO, CancellationToken cancellationToken)
        {
            try
            {
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue
                    ("Bearer", _tokenStorage.AccessToken);

                var response = await _client.PostAsJsonAsync("api/AdminLangCodes/add-langCode", createLanguageDTO, cancellationToken);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync(cancellationToken: cancellationToken);

                    return result ?? throw new Exception("Language didn`t add");
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync(cancellationToken);
                    throw new Exception($"Add language failed: {response.StatusCode} - {errorMessage}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Add language failed: {ex.Message}");
            }
        }
    }
}
