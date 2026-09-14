using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangApp.Admin.WPF.DTOs.Requests
{
    public class CreateLanguageDTO
    {
        public string Name { get; set; } = string.Empty;
        public string LangCode { get; set; } = string.Empty;
    }
}
