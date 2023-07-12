using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace Simulator
{
    public class FlightsClient
    {
        private HttpClient _HttpClient;
        public FlightsClient()
        {
            _HttpClient = new HttpClient();
        }

        public async Task<bool> CreateFlight()
        {
            try
            {
                Random random = new Random();
                int passengersCount = random.Next(100, 501);
                int rndType = random.Next(1, 3);
                int rndBrand = random.Next(1, 3);
                Flight flight = new Flight
                {
                    Brand = rndBrand == 1 ? "ElAl": "Israir",
                    PassengersCount = passengersCount,
                    Type = rndType,
                    IsCritical = false
                };

                var content = JsonConvert.SerializeObject(flight);
                var buffer = Encoding.UTF8.GetBytes(content);
                var bytes = new ByteArrayContent(buffer);

                bytes.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

                HttpResponseMessage response = await _HttpClient.PostAsync("https://localhost:44349/api/flights",bytes);
            }
            catch(Exception ex)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> StartProcessAirport()
        {
            try
            {
                var content = JsonConvert.SerializeObject(null);
                var buffer = Encoding.UTF8.GetBytes(content);
                var bytes = new ByteArrayContent(buffer);

                bytes.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

                HttpResponseMessage response = await _HttpClient.PostAsync("https://localhost:44349/api/scheduler/processAirport", bytes);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
    }

    public class Flight
    {
        public int PassengersCount { get; set; }
        public bool IsCritical { get; set; }
        public string Brand { get; set; }
        public int Type { get; set; }
    }
}
