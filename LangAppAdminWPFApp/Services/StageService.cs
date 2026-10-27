using LangApp.Admin.WPF.DTOs.Requests;
using LangApp.Admin.WPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace LangApp.Admin.WPF.Services.Interfaces
{
   public class StageService : IStageService
    {
        private readonly HttpClient _client;
        private readonly ITokenStorage _tokenStorage;

        public StageService(HttpClient client, ITokenStorage tokenStorage)
        {
            _client = client;
            _tokenStorage = tokenStorage;
        }

        public async Task<List<Stage>> GetStagesAsync(CancellationToken cancellationToken)
        {
            return await _client.GetFromJsonAsync<List<Stage>>("api/Stage/get-stages", cancellationToken) ?? [];
        }

        public async Task<string> AddNewStageAsync(CreateStageDTO newStage, CancellationToken cancellationToken)
        {
            try
            {
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue
                    ("Bearer", _tokenStorage.AccessToken);

                var response = await _client.PostAsJsonAsync("api/Stage/create-stage", newStage, cancellationToken);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync(cancellationToken: cancellationToken);

                    return result ?? throw new Exception("Stage didn`t add");
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync(cancellationToken);
                    throw new Exception($"Add stage failed: {response.StatusCode} - {errorMessage}");
                }
            }
            catch(Exception ex)
            {
                throw new Exception($"Add stage failed: {ex.Message}");
            }
        }

        public async Task<string> EditStageAsync(EditStageDTO newStage, CancellationToken cancellationToken)
        {
            try
            {
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue
                ("Bearer", _tokenStorage.AccessToken);

                var response = await _client.PutAsJsonAsync("api/Stage/update-stage", newStage, cancellationToken);
                if(response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync(cancellationToken: cancellationToken);
                    return result ?? throw new Exception("Stage didn`t update");
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync(cancellationToken);
                    throw new Exception($"Edit stage failed: {response.StatusCode} - {errorMessage}");
                }
            }
            catch(Exception ex)
            {
                throw new Exception($"Edit stage failed: {ex.Message}");
            }
        }

        public async Task<string> DeleteStageAsync(Guid id)
        {
            try
            {
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue
                ("Bearer", _tokenStorage.AccessToken);
                var response = await _client.DeleteAsync($"api/Stage/delete-stage/{id}");
                if(response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return result ?? throw new Exception("Stage wasn`t deleted");
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Delete stage failed: {response.StatusCode} - {errorMessage}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Delete stage failed: {ex.Message}");
            }
        }
    }
}
