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
    {
        // має бути string
        public Category CategoryToEdit { get; set; } = new Category();
        public string EditedCategory { get; set; } = string.Empty;
    }
}
