using MyWebApiApp.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace MyWebApiApp.HttpClients
{
    public class SwapiClient : ISwapiClient
    {
        private readonly HttpClient _httpClient;
        public SwapiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        async Task<FilmsInfo> ISwapiClient.GetFilmsInfo()
        {
            var response = await _httpClient.GetAsync("films/");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<FilmsInfo>();
        }
    }
}
