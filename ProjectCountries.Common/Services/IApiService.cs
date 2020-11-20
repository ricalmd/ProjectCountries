using System.Threading.Tasks;
using ProjectCountries.Common.Entities;

namespace ProjectCountries.Common.Services
{
    public interface IApiService
    {
        Task<Response> GetCountries(string url, string controller);
    }
}
