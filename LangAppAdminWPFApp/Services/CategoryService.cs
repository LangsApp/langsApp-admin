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
    public class CategoryService : ICategoryService
    {
        private readonly HttpClient _client;
        private readonly ITokenStorage _tokenStorage;

        public CategoryService(HttpClient client, ITokenStorage tokenStorage)
        {
            _client = client;
            _tokenStorage = tokenStorage;
        }

        public async Task<List<Category>> GetCategoriesAsync(CancellationToken cancellationToken)
        {
            return await _client.GetFromJsonAsync<List<Category>>("api/Category/get-categories", cancellationToken) ?? [];
        }

        public async Task<string> AddNewCategoryAsync(string newCategory, CancellationToken cancellationToken)
        {
            try
            {
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue
                    ("Bearer", _tokenStorage.AccessToken);

                var request = new CreateCategoryDTO
                {
                    Name = newCategory
                };

                var response = await _client.PostAsJsonAsync("api/Category/add-category", request, cancellationToken);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync(cancellationToken: cancellationToken);

                    return result ?? throw new Exception("Category want added");
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync(cancellationToken);
                    throw new Exception($"Add category failed: {response.StatusCode} - {errorMessage}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Add category failed: {ex.Message}");
            }
        }
    }
}
