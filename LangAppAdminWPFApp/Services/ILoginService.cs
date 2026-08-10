using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangApp.Admin.WPF.Services
{
    public interface ILoginService
    {
        public Task<string> Login(string Login, string Password, CancellationToken cancellationToken);
    }
}
