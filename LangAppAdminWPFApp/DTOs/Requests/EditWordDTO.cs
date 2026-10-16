using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangApp.Admin.WPF.DTOs.Requests
{
    public class EditWordDTO
    {
        public Guid Id { get; set; }
        public string WordToEdit { get; set; } = string.Empty;
        public string EditedWord {  get; set; } = string.Empty;
    }
}
