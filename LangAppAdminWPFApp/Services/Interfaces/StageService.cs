using LangApp.Admin.WPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace LangApp.Admin.WPF.Services.Interfaces
{
   public class StageService : IStageService
    {
        private readonly HttpClient _client;

        public StageService(HttpClient client)
        {
            _client = client;
        }

        public async Task<List<Stage>> GetStagesAsync(CancellationToken cancellationToken)
        {
            return await _client.GetFromJsonAsync<List<Stage>>("", cancellationToken) ?? [];
        }
    }
}
