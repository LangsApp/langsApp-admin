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
    public sealed class LoginService : ILoginService
    {
        private readonly HttpClient _client;

        public LoginService(HttpClient client)
        {
            _client = client;
        }

        public async Task<string> Login(string Email, string Password, CancellationToken cancellationToken)
        {
            var credentials = new { Email, Password };
            try
            {
                var response = await _client.PostAsJsonAsync("api/Account/Login", credentials, cancellationToken);
                if (response.IsSuccessStatusCode)
                {
                    var token = await response.Content.ReadAsStringAsync(cancellationToken: cancellationToken);
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
