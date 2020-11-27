using System.Collections.Generic;
using ProjectCountries.Common.Entities;

namespace ProjectCountries.Common.Services
{
    public interface IVerifyEmptyService
    {
        List<Currency> VerifyCurrency(List<Currency> currencies);

        List<object> VerifyEmptyObjects(List<object> items);

        string VerifyEmptyString(string item);

        List<string> VerifyEmptyStringList(List<string> items);

        List<RegionalBloc> VerifyRegionalBloc(List<RegionalBloc> list);
    }
}
