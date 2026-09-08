using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LangApp.Admin.WPF.Services.Interfaces
{
    public interface IWindowFactory
    {
        TWindow Create<TWindow>()
           where TWindow : Window;
    }
}
