using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangApp.Admin.WPF.Services
{
    public sealed class TokenStorage : ITokenStorage
    {
        public string? AccessToken { get; set; }

        public void Clear()
        {
            AccessToken = null;
        }
    }
}
