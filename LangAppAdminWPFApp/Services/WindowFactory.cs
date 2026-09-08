using LangApp.Admin.WPF.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LangApp.Admin.WPF.Services
{
    public class WindowFactory : IWindowFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public WindowFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public TWindow Create<TWindow>()
            where TWindow : Window
        {
            return _serviceProvider
                .GetRequiredService<TWindow>();
        }
    }
}
