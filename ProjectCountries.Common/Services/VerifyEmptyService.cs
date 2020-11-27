using System.Collections.Generic;
using ProjectCountries.Common.Entities;

namespace ProjectCountries.Common.Services
{
    public class VerifyEmptyService : IVerifyEmptyService
    {
        public List<Currency> VerifyCurrency(List<Currency> currencies)
        {
            foreach (var item in currencies)
            {
                item.Code = VerifyEmptyString(item.Code);
                item.Name = VerifyEmptyString(item.Name);
                item.Symbol = VerifyEmptyString(item.Symbol);
            }

            return currencies;
        }

        public List<object> VerifyEmptyObjects(List<object> items)
        {
            if (items.Count == 0)
            {
                items.Add("-");
                return items;
            }

            return items;
        }

        public string VerifyEmptyString(string item)
        {
            if(string.IsNullOrEmpty(item))
            {
                return "-";
            }

            return item;
        }

        public List<string> VerifyEmptyStringList(List<string> items)
        {
            if(items.Count == 0)
            {
                items.Add("-");
                return items;
            }

            return items;
        }

        public List<RegionalBloc> VerifyRegionalBloc(List<RegionalBloc> list)
        {
            foreach(var item in list)
            {
                item.Name = VerifyEmptyString(item.Name);
                item.Acronym = VerifyEmptyString(item.Acronym);
                item.OtherNames = VerifyEmptyObjects(item.OtherNames);
                item.OtherAcronyms = VerifyEmptyObjects(item.OtherAcronyms);
            }

            return list;
        }
    }
}
