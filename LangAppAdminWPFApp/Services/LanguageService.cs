using LangApp.Admin.WPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LangApp.Admin.WPF.Services
{
    public class LanguageService : ILanguageService
    {
        private readonly HttpClient _client;

        public LanguageService (HttpClient client)
        {
            _client = client;
        }
        public async Task<List<Language>> GetLanguagesAsync(CancellationToken cancellationToken)
        {
            return await _client.GetFromJsonAsync<List<Language>>("api/AdminLangCodes/get-languages", 
                cancellationToken)
                ?? [];
        }
    }
}
