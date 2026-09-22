using LangApp.Admin.WPF.Models;
using LangApp.Admin.WPF.Services.Interfaces;
using LangApp.Admin.WPF.Views.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangApp.Admin.WPF.Services
{
    public class DialogService : IDialogService
    {
        private readonly IWindowFactory _windowFactory;

        public DialogService(IWindowFactory windowFactory)
        {
            _windowFactory = windowFactory;
        }

        public void OpenAddCategoryDialog()
        {
            AddCategoryWindow window = _windowFactory.Create<AddCategoryWindow>();
            window.ShowDialog();
        }

        public void OpenAddLanguageDialog()
        {
            AddLanguageWindow window = _windowFactory.Create<AddLanguageWindow>();
            window.ShowDialog();
        }
        public void OpenAddStageDialog()
        {
            AddStageWindow window = _windowFactory.Create<AddStageWindow>();
            window.ShowDialog();
        }

        public void OpenAddWordDialog()
        {
            AddWordWindow window = _windowFactory.Create<AddWordWindow>();
            window.ShowDialog();
        }
        
        public void OpenEditCategoryDialog(Category selectedCategory)
        {
            EditCategoryWindow window = _windowFactory.Create<EditCategoryWindow>(selectedCategory);
            window.ShowDialog();
        }
    }
}
