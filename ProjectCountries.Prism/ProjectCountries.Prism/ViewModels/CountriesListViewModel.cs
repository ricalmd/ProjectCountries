using System.Collections.Generic;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Navigation;
using ProjectCountries.Common.Entities;
using ProjectCountries.Common.Services;
using Xamarin.Essentials;

namespace ProjectCountries.Prism.ViewModels
{
    public class CountriesListViewModel : ViewModelBase
    {
        private readonly INavigationService _navigation;
        private readonly IApiService _apiService;

        private bool _isRunning;
        private string _search;
        private List<Country> _countries;
        private DelegateCommand _searchCommand;

        public CountriesListViewModel(
            INavigationService navigation, 
            IApiService apiService) : base(navigation)
        {
            _navigation = navigation;
            _apiService = apiService;
            Title = "Countries";

            LoadCountriesAsync();
        }

        public List<Country> Countries
        {
            get => _countries;
            set => SetProperty(ref _countries, value);
        }

        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }

        public string Search
        {
            get => _search;
            set
            {
                SetProperty(ref _search, value);
            }
        }

        private async Task LoadApiCountriesAsync()
        {
            _isRunning = true;

            string url = "https://restcountries.eu",
            path = "/rest/v2/all?fields=name;flag";

            Response response = await _apiService.GetCountriesAsync<Country>(url, path);

            _isRunning = false;

            if (!response.Connect)
            {
                await App.Current.MainPage.DisplayAlert("Erro", response.Message, "Sair");
            }

            Countries = (List<Country>)response.Result;
        }

        private async void LoadCountriesAsync()
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await App.Current.MainPage.DisplayAlert("ERRO", "Aconteceu um erro na conexão à Internet", "Sair");
                return;
            }
            else
            {
                await LoadApiCountriesAsync();
            }
        }
    }
}
