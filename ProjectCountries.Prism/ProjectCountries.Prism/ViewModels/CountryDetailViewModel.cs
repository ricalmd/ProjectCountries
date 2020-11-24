using Prism.Navigation;
using ProjectCountries.Common.Entities;

namespace ProjectCountries.Prism.ViewModels
{
    public class CountryDetailViewModel : ViewModelBase
    {
        private Country _country;

        public CountryDetailViewModel(INavigationService navigation) : base(navigation)
        {
            Title = "Country";
        }

        public Country Country
        {
            get => _country;
            set => SetProperty(ref _country, value);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey("country"))
            {
                Country = parameters.GetValue<Country>("country");
                Title = Country.Name;
            }
        }
    }
}
