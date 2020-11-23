using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
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

        private List<Country> _countries;

        public CountriesListViewModel(
            INavigationService navigation, 
            IApiService apiService) : base(navigation)
        {
            _navigation = navigation;
            _apiService = apiService;
            Title = "Countries";

            LoadCountries();
        }

        public List<Country> Countries
        {
            get => _countries;
            set => SetProperty(ref _countries, value);
        }

        private async Task LoadApiCountries()
        {
            string url = "https://restcountries.eu",
            path = "/rest/v2/all?fields=name";

            var response = await _apiService.GetCountries(url, path);

            Countries = (List<Country>)response.Result;
        }

        private async void LoadCountries()
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await App.Current.MainPage.DisplayAlert("ERRO", "Aconteceu um erro na conexão à Internet", "Sair");
                return;
            }
            else
            {
                await LoadApiCountries();
            }
        }
    }
}
