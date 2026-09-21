using LangApp.Admin.WPF.DTOs.Requests;
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
    public class WordsService : IWordsService
    {
        private readonly HttpClient _client;
        private readonly ITokenStorage _tokenStorage;

        public WordsService(HttpClient client, ITokenStorage tokenStorage)
        {
            _client = client;
            _tokenStorage = tokenStorage;
        }

        public async Task<List<BaseWord>> GetBaseWordsAsync(CancellationToken cancellationToken)
        {
            return await _client.GetFromJsonAsync<List<BaseWord>>("api/Words/get-list-words", cancellationToken) ?? [];
        }

        public async Task<string> AddNewBaseWordsByCategoryAsync
            (CreateBaseWordsByCategoryDTO createBaseWordsByCategory, CancellationToken cancellationToken)
        {
            try
            {
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue
                    ("Bearer", _tokenStorage.AccessToken);

                var response = await _client
                    .PostAsJsonAsync("api/Words/add-list-words-by-category", createBaseWordsByCategory, cancellationToken);

                if(response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync(cancellationToken: cancellationToken);

                    return result ?? throw new Exception("Words didn`t add");
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync(cancellationToken);
                    throw new Exception($"Add words failed: {response.StatusCode} - {errorMessage}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while adding words: {ex.Message}", ex);
            }
        }

        public async Task<string> AddNewBaseWordAsync(CreateBaseWordDTO newBaseWord, CancellationToken cancellationToken)
        {
            try
            {
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue
                    ("Bearer", _tokenStorage.AccessToken);

                var response = await _client.PostAsJsonAsync("api/Words/add-word", newBaseWord, cancellationToken);

                if(response.IsSuccessStatusCode)
                {

                    var result = await response.Content.ReadAsStringAsync(cancellationToken: cancellationToken);

                    return result ?? throw new Exception("Word didn`t add");
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync(cancellationToken);
                    throw new Exception($"Add word failed: {response.StatusCode} - {errorMessage}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while adding the word: {ex.Message}", ex);
            }
        }
    }
}
