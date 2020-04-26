using MyWebApiApp.Models;
using System.Threading.Tasks;

namespace MyWebApiApp.HttpClients
{
    public interface ISwapiClient
    {
        Task<FilmsInfo> GetFilmsInfo();
    }
}
