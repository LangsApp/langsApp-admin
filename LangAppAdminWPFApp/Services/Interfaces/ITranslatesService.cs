using LangApp.Admin.WPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangApp.Admin.WPF.Services.Interfaces
{
    public interface ITranslatesService
    {
        public Task<List<Translate>> GetTranslatesAsync(CancellationToken cancellationToken);
    }
}
