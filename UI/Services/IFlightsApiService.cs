using Common;
using Common.Models;
using System.Threading.Tasks;

namespace UI.Services
{
    public interface IFlightsApiService
    {
        Task<PagedCollectionResponse<Flight>> GetFlightsByStatus(int statusId);
    }
}
