using Common;
using Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UI.Services
{
    public interface ILegsApiService
    {
        Task<IList<Leg>> GetLegs();
    }
}
