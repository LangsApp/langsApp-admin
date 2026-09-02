using LangApp.Admin.WPF.Services;
using LangApp.Admin.WPF.Services.Interfaces;
using LangApp.Admin.WPF.ViewModels;
using LangApp.Admin.WPF.ViewModels.PagesViewModels;
using LangApp.Admin.WPF.Views;
using LangApp.Admin.WPF.Views.PagesViews;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Configuration;
using System.Data;
using System.Net.Http;
using System.Windows;

namespace LangAppAdminWPFApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _host;

        public App()
        {
            HostApplicationBuilder builder =
                Host.CreateApplicationBuilder(
                    new HostApplicationBuilderSettings
                    {
                        ContentRootPath = AppContext.BaseDirectory
                    });

            string baseUrl =
                builder.Configuration["Api:BaseUrl"]
                ?? throw new InvalidOperationException(
                    "Api:BaseUrl is not configured.");

            void ConfigureHttpClient(HttpClient client)
            {
                client.BaseAddress = new Uri(baseUrl);
                client.Timeout = TimeSpan.FromSeconds(30);
            }

            //builder.Services
            //    .AddHttpClient<ILoginService, LoginService>(
            //        client =>
            //        {
            //            client.BaseAddress = new Uri(baseUrl);
            //            client.Timeout = TimeSpan.FromSeconds(30);
            //        });

            builder.Services.AddTransient<LoginWindowViewModel>();
            builder.Services.AddTransient<LoginWindow>();
            builder.Services.AddTransient<MainWindowViewModel>();
            builder.Services.AddTransient<MainWindow>();
            builder.Services.AddTransient<LanguagePageViewModel>();
            builder.Services.AddTransient<LanguagesPage>();
            builder.Services.AddTransient<StagePageViewModel>();
            builder.Services.AddTransient<StagesPage>();
            builder.Services.AddTransient<WordsPageViewModel>();
            builder.Services.AddTransient<WordsPage>();
            builder.Services.AddTransient<TranslationsPageViewModel>();
            builder.Services.AddTransient<TranslationsPage>();
            builder.Services.AddTransient<CategoriesPage>();
            builder.Services.AddTransient<CategoriesPageViewModel>();

            builder.Services.AddSingleton<IAppNavigationService, AppNavigationService>();
            builder.Services.AddSingleton<ITokenStorage, TokenStorage>();

            builder.Services.AddHttpClient<ILoginService, LoginService>(ConfigureHttpClient);
            builder.Services.AddHttpClient<ILanguageService, LanguageService>(ConfigureHttpClient);
            builder.Services.AddHttpClient<IStageService,  StageService>(ConfigureHttpClient);
            builder.Services.AddHttpClient<IWordsService,  WordsService>(ConfigureHttpClient);
            builder.Services.AddHttpClient<ITranslatesService, TranslatesService>(ConfigureHttpClient);
            builder.Services.AddHttpClient<ICategoryService, CategoryService>(ConfigureHttpClient);

            _host = builder.Build();
        }

        protected override async void OnStartup(
            StartupEventArgs e)
        {
            base.OnStartup(e);

            await _host.StartAsync();

            LoginWindow loginWindow =
                _host.Services.GetRequiredService<LoginWindow>();

            MainWindow = loginWindow;
            loginWindow.Show();
        }

        protected override async void OnExit(
            ExitEventArgs e)
        {
            await _host.StopAsync();
            _host.Dispose();

            base.OnExit(e);
        }
    }

}
