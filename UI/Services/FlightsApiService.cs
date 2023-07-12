using Common;
using Common.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace UI.Services
{
    public class FlightsApiService : IFlightsApiService
    {
        private HttpClient _HttpClient;
        public FlightsApiService()
        {
            _HttpClient = new HttpClient();
        }

        public async Task<PagedCollectionResponse<Flight>> GetFlightsByStatus(int statusId)
        {
            PagedCollectionResponse<Flight> pagedCollectionResponse = null;

            try
            {
                HttpResponseMessage response = await _HttpClient.GetAsync($"https://localhost:44349/api/flights?StatusId={statusId}");

                if (response.IsSuccessStatusCode)
                    pagedCollectionResponse = await response.Content.ReadAsAsync<PagedCollectionResponse<Flight>>();
            }
            catch
            {
                return null;
            }

            return pagedCollectionResponse;
        }
    }
}
