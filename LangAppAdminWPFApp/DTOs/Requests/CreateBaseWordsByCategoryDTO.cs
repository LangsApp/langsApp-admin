using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangApp.Admin.WPF.DTOs.Requests
{
    public class CreateBaseWordsByCategoryDTO
    {
        public string CategoryName { get; set; } = string.Empty;
        public List<CreateBaseWordDTO> Words { get; set; } = new();
    }
}
