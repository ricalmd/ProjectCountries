using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Navigation;
using ProjectCountries.Common.Entities;
using ProjectCountries.Common.Services;
using ProjectCountries.Prism.ItemViewModels;
using Xamarin.Essentials;

namespace ProjectCountries.Prism.ViewModels
{
    public class CountriesListViewModel : ViewModelBase
    {
        private readonly INavigationService _navigation;
        private readonly IApiService _apiService;
        private readonly IVerifyEmptyService _verifyEmptyService;
        private bool _isRunning;
        private string _search;
        private List<Country> _countriesList;
        private DelegateCommand _searchCommand;

        ObservableCollection<CountryItemViewModel> _countries;

        public CountriesListViewModel(
            INavigationService navigation, 
            IApiService apiService,
            IVerifyEmptyService verifyEmptyService) : base(navigation)
        {
            _navigation = navigation;
            _apiService = apiService;
            _verifyEmptyService = verifyEmptyService;
            Title = "Countries";

            LoadCountriesAsync();
        }

        public DelegateCommand SearchCommand => _searchCommand ?? (_searchCommand =
            new DelegateCommand(ShowCountries));

        public ObservableCollection<CountryItemViewModel> Countries
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
                ShowCountries();
            }
        }

        private async Task LoadApiCountriesAsync()
        {
            _isRunning = true;

            string url = "https://restcountries.eu",
            path = "/rest/v2/all";

            Response response = await _apiService.GetCountriesAsync<Country>(url, path);

            _isRunning = false;

            if (!response.Connect)
            {
                await App.Current.MainPage.DisplayAlert("Erro", response.Message, "Sair");
            }

            _countriesList = (List<Country>)response.Result;
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

        private void ShowCountries()
        {
            if (string.IsNullOrEmpty(Search))
            {
                Countries = new ObservableCollection<CountryItemViewModel>(
                    _countriesList.Select(c =>
                    new CountryItemViewModel(_navigation)
                    {
                        Alpha2Code = _verifyEmptyService.VerifyEmptyString(c.Alpha2Code),
                        Alpha3Code = _verifyEmptyService.VerifyEmptyString(c.Alpha3Code),
                        AltSpellings = _verifyEmptyService.VerifyEmptyStringList(c.AltSpellings),
                        Area = c.Area,
                        Borders = _verifyEmptyService.VerifyEmptyStringList(c.Borders),
                        CallingCodes = _verifyEmptyService.VerifyEmptyStringList(c.CallingCodes),
                        Capital = _verifyEmptyService.VerifyEmptyString(c.Capital),
                        Cioc = _verifyEmptyService.VerifyEmptyString(c.Cioc),
                        Currencies = _verifyEmptyService.VerifyCurrency(c.Currencies),
                        Demonym = _verifyEmptyService.VerifyEmptyString(c.Demonym),
                        Flag = c.Flag,
                        Gini = c.Gini,
                        Languages = c.Languages,
                        Latlng = c.Latlng,
                        Name = c.Name,
                        NativeName = _verifyEmptyService.VerifyEmptyString(c.NativeName),
                        NumericCode = _verifyEmptyService.VerifyEmptyString(c.NumericCode),
                        Population = c.Population,
                        Region = _verifyEmptyService.VerifyEmptyString(c.Region),
                        RegionalBlocs = _verifyEmptyService.VerifyRegionalBloc(c.RegionalBlocs),
                        Subregion = _verifyEmptyService.VerifyEmptyString(c.Subregion),
                        Timezones = _verifyEmptyService.VerifyEmptyStringList(c.Timezones),
                        TopLevelDomain = _verifyEmptyService.VerifyEmptyStringList(c.TopLevelDomain),
                        Translations = c.Translations
                    })
                    .ToList());
            }
            else
            {
                Countries = new ObservableCollection<CountryItemViewModel>(
                    _countriesList.Select(c =>
                    new CountryItemViewModel(_navigation)
                    {
                        Alpha2Code = _verifyEmptyService.VerifyEmptyString(c.Alpha2Code),
                        Alpha3Code = _verifyEmptyService.VerifyEmptyString(c.Alpha3Code),
                        AltSpellings = _verifyEmptyService.VerifyEmptyStringList(c.AltSpellings),
                        Area = c.Area,
                        Borders = _verifyEmptyService.VerifyEmptyStringList(c.Borders),
                        CallingCodes = _verifyEmptyService.VerifyEmptyStringList(c.CallingCodes),
                        Capital = _verifyEmptyService.VerifyEmptyString(c.Capital),
                        Cioc = _verifyEmptyService.VerifyEmptyString(c.Cioc),
                        Currencies = c.Currencies,
                        Demonym = _verifyEmptyService.VerifyEmptyString(c.Demonym),
                        Flag = c.Flag,
                        Gini = c.Gini,
                        Languages = c.Languages,
                        Latlng = c.Latlng,
                        Name = c.Name,
                        NativeName = _verifyEmptyService.VerifyEmptyString(c.NativeName),
                        NumericCode = _verifyEmptyService.VerifyEmptyString(c.NumericCode),
                        Population = c.Population,
                        Region = _verifyEmptyService.VerifyEmptyString(c.Region),
                        RegionalBlocs = _verifyEmptyService.VerifyRegionalBloc(c.RegionalBlocs),
                        Subregion = _verifyEmptyService.VerifyEmptyString(c.Subregion),
                        Timezones = _verifyEmptyService.VerifyEmptyStringList(c.Timezones),
                        TopLevelDomain = _verifyEmptyService.VerifyEmptyStringList(c.TopLevelDomain),
                        Translations = c.Translations
                    })
                    .Where(c => c.Name.ToLower().Contains(Search.ToLower()))
                    .ToList());
            }
        }
    }
}
