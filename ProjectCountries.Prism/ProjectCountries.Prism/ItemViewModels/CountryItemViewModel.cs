using Prism.Commands;
using Prism.Navigation;
using ProjectCountries.Common.Entities;
using ProjectCountries.Prism.Views;

namespace ProjectCountries.Prism.ItemViewModels
{
    public class CountryItemViewModel : Country
    {
        private readonly INavigationService _navigation;
        private DelegateCommand _selectCountryCommand;

        public CountryItemViewModel(INavigationService navigation)
        {
            _navigation = navigation;
        }

        public DelegateCommand SelectCountryCommand => _selectCountryCommand ??
            (_selectCountryCommand = new DelegateCommand(SelectCountryAsync));

        private async void SelectCountryAsync()
        {
            NavigationParameters parameters = new NavigationParameters
            {
                { "country", this }
            };

            await _navigation.NavigateAsync(nameof(CountryDetail), parameters);
        }
    }
}
