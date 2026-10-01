using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangApp.Admin.WPF.DTOs.Requests
{
    public class EditStageDTO
    {
        public int EditedOrder {  get; set; }
        public string EditedName { get; set; } = string.Empty;
        public string NameToEdit { get; set; } = string.Empty;
    }
}
