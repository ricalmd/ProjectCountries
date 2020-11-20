using System.Net;
using ProjectCountries.Common.Entities;

namespace ProjectCountries.Common.Services
{
    public class NetworkService : INetworkService
    {
        public Response CheckConnection()
        {
            var client = new WebClient();

            try
            {
                using (client.OpenRead("http://clients3.google.com/generate_204"))
                {
                    return new Response
                    {
                        Connect = true,
                    };
                }
            }
            catch
            {
                return new Response
                {
                    Connect = false,
                    Message = "Sem ligação à Internet",
                };
            }
        }
    }
}
