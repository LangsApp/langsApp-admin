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
    public class TranslatesService : ITranslatesService
    {
        private readonly HttpClient _client;

        public TranslatesService(HttpClient client)
        {
            _client = client;
        }

        public async Task<List<Translate>> GetTranslatesAsync(CancellationToken cancellationToken)
        {
            return await _client.GetFromJsonAsync<List<Translate>>("", cancellationToken) ?? [];
        }
    }
}
