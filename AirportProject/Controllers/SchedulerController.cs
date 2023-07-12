using AirportProject.Context;
using AirportProject.Services;
using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AirportProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchedulerController : ControllerBase
    {
        private ILog _logger;
        private DBContext _dbContext;
        private IProcessAirportService _processAirportService;
        public SchedulerController(ILog logger, DBContext dbContext, IProcessAirportService processAirportService)

        {
            _logger = logger;
            _dbContext = dbContext;
            _processAirportService = processAirportService;
        }

        [HttpPost]
        [Route("processAirport")]
        public async Task<IActionResult> ProcessAirport()
        {
            try
            {
                _logger.Info("Calling process airport");
                await _processAirportService.ProcessAirport();
                return Accepted();
            }
            catch (Exception ex)
            {
                _logger.Error($"when Calling process airport thrown exception - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
