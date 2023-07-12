using Common;
using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace UI.Services
{
    public class LegsApiService : ILegsApiService
    {
        private HttpClient _HttpClient;
        public LegsApiService()
        {
            _HttpClient = new HttpClient();
        }

        public async Task<IList<Leg>> GetLegs()
        {
            IList<Leg> legs = null;

            try
            {
                HttpResponseMessage response = await _HttpClient.GetAsync($"https://localhost:44349/api/legs");

                if (response.IsSuccessStatusCode)
                    legs = await response.Content.ReadAsAsync<IList<Leg>>();
            }
            catch
            {
                return null;
            }

            return legs;
        }
    }
}
