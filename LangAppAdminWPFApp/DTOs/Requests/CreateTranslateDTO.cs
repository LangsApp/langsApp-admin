using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangApp.Admin.WPF.DTOs.Requests
{
    public class CreateTranslateDTO
    {
        public Guid BaseWordId { get; set; }
        public Guid LangCodeId { get; set; }
        public string TranslatedText { get; set; } = string.Empty;
    }
}
