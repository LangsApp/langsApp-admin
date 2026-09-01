using LangApp.Admin.WPF.Models;
using LangApp.Admin.WPF.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace LangApp.Admin.WPF.Services
{
    public class WordsService : IWordsService
    {
        private readonly HttpClient _client;

        public WordsService(HttpClient client)
        {
            _client = client;
        }

        public async Task<List<BaseWord>> GetBaseWordsAsync(CancellationToken cancellationToken)
        {
            return await _client.GetFromJsonAsync<List<BaseWord>>("api/Words/get-list-words", cancellationToken) ?? [];
        }
    }
}
