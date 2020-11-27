using Prism;
using Prism.Ioc;
using ProjectCountries.Prism.ViewModels;
using ProjectCountries.Prism.Views;
using Xamarin.Essentials.Interfaces;
using Xamarin.Essentials.Implementation;
using Xamarin.Forms;
using ProjectCountries.Common.Services;

namespace ProjectCountries.Prism
{
    public partial class App
    {
        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync($"NavigationPage/{nameof(CountriesList)}");

            Device.SetFlags(new string[] { "Shapes_Experimental" });
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();
            containerRegistry.Register<IApiService, ApiService>();
            containerRegistry.Register<IVerifyEmptyService, VerifyEmptyService>();
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<CountryDetail, CountryDetailViewModel>();
            containerRegistry.RegisterForNavigation<CountriesList, CountriesListViewModel>();
        }
    }
}
