using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangApp.Admin.WPF.DTOs.Requests
{
    public class EditTranslateDTO
    {
        public string TranslateToEdit { get; set; } = string.Empty;
        public string EditedTranslate { get;set; } = string.Empty;
    }
}
