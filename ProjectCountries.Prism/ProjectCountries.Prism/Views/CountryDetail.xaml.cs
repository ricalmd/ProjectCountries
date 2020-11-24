using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectCountries.Prism.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CountryDetail : ContentPage
    {
        public CountryDetail()
        {
            InitializeComponent();
        }
    }
}