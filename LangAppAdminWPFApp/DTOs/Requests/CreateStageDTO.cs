using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangApp.Admin.WPF.DTOs.Requests
{
    public class CreateStageDTO
    {
        public string Name { get; set; } = string.Empty;
        public int Order {  get; set; }
    }
}
