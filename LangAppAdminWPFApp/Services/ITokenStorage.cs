using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangApp.Admin.WPF.Services
{
    public interface ITokenStorage
    {
        string? AccessToken { get; set; }

        void Clear();
    }
}
