using Microsoft.AspNetCore.Mvc;
using MyWebApiApp.HttpClients;
using System.Threading.Tasks;

namespace MyWebApiApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ISwapiClient _swapiClient;

        public TestController(ISwapiClient swapiClient)
        {
            _swapiClient = swapiClient;
        }

        // GET api/values
        [HttpGet]
        public async Task<int> Get()
        {
            var result = await _swapiClient.GetFilmsInfo();
            return result.Count;
        }
    }
}
