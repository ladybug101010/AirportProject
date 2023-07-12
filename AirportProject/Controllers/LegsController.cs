using AirportProject.Context;
using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AirportProject.Controllers
{
    [Route("api/[controller]")]
    public class LegsController : ControllerBase
    {
        private ILog _logger;
        private DBContext _dbContext;
        public LegsController(ILog logger, DBContext dbContext)

        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetLegs()
        {
            try
            {
                _logger.Info("Calling get all legs");
                var legs = await _dbContext.Legs.Include(x=>x.Flight).ToListAsync();
                var res = legs.OrderBy(x => x.OrderNo);
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.Error($"when Calling get all legs thrown exception - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
