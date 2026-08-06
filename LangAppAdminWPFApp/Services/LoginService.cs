using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LangApp.Admin.WPF.Services
{
    class LoginService : ILoginService
    {
        HttpClient _client;
        JsonSerializerOptions _serializerOptions;
        
        public LoginService()
        {
            string baseUrl = ConfigurationManager.AppSettings["ApiAddress"] ?? string.Empty;
            _client = new HttpClient()
            {
                BaseAddress = new Uri(baseUrl)
            };

            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }

        public async Task<string> Login(string Login, string Password, CancellationToken cancellationToken)
        {
            var credentials = new { Login, Password };
            try
            {
                var response = await _client.PostAsJsonAsync("api/Account/Login", credentials, cancellationToken);
                if (response.IsSuccessStatusCode)
                {
                    var token = await response.Content.ReadFromJsonAsync<string>(cancellationToken: cancellationToken);
                    return token ?? throw new Exception("Token is null");
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync(cancellationToken);
                    throw new Exception($"Login failed: {response.StatusCode} - {errorMessage}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Login failed: {ex.Message}");
            }
        }
    }
}
