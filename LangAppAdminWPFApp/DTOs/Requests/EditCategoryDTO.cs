using LangApp.Admin.WPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace LangApp.Admin.WPF.DTOs.Requests
{
    public class EditCategoryDTO
    {   public Guid Id { get; set; }
        public string CategoryToEditName { get; set; } = string.Empty;
        public string EditedCategory { get; set; } = string.Empty;
    }
}
