using AirportProject.Context;
using AirportProject.Queries;
using Common;
using Common.Models;
using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace AirportProject.Controllers
{
    [Route("api/[controller]")]
    public class FlightsController : ControllerBase
    {
        private ILog _logger;
        private DBContext _dbContext;
        public FlightsController(ILog logger, DBContext dbContext)

        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetFlights([FromQuery] FlightQuery flightQuery)
        {
            PagedCollectionResponse<Flight> response = new PagedCollectionResponse<Flight>();

            var flightsQuery = _dbContext.Flights.AsQueryable();

            try
            {
                _logger.Info("Calling get all flights");

                if (flightQuery.StatusId != null)
                {
                    flightsQuery = flightsQuery.Where(x => x.FlightStatusId == flightQuery.StatusId);
                }

                long count = await flightsQuery.LongCountAsync();

                response.TotalResult = int.Parse(count.ToString());

                IList<Flight> flights;

                if (flightQuery.StatusId == 3)
                {
                    flights = await flightsQuery.Include(x=>x.FlightStatus)
                                            .Take(flightQuery.Limit)
                                            .OrderByDescending(x => x.Id)
                                            .ToListAsync();
                }

                else
                {

                    flights = await flightsQuery.Include(x => x.FlightStatus)
                                                .Skip((flightQuery.Page - 1) * flightQuery.Limit)
                                                .Take(flightQuery.Limit)
                                                .ToListAsync();
                }

                response.Items = flights;

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.Error($"when Calling get all flights thrown exception - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetFlightById(long id)
        {
            try
            {
                _logger.Info($"Calling get flight {id}");

                var flight = await _dbContext.Flights.Where(x => x.Id == id).FirstOrDefaultAsync();

                if (flight == null)
                {
                    return NotFound($"flight with id {id} not found");
                }

                return Ok(flight);
            }
            catch (Exception ex)
            {
                _logger.Error($"when Calling get flight {id} thrown exception - {ex.Message}");

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteFlightById(long id)
        {
            try
            {
                _logger.Info($"Calling delete flight {id}");

                var flight = await _dbContext.Flights.Where(x => x.Id == id).FirstOrDefaultAsync();

                if (flight == null)
                {
                    return NotFound($"flight with id {id} not found");
                }

                _dbContext.Flights.Remove(flight);
                await _dbContext.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.Error($"when Calling delete flight {id} thrown exception - {ex.Message}");

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPatch]
        [Route("{id}")]
        public async Task<IActionResult> UpdateFlight(long id, [FromBody] JsonElement body)
        {
            try
            {
                _logger.Info($"Calling update flight {id}");

                string json = System.Text.Json.JsonSerializer.Serialize(body);
                Flight flight = JsonSerializer.Deserialize<Flight>(json);

                if (flight == null)
                {
                    return BadRequest("some fields are missing");
                }

                var currentFlight = await _dbContext.Flights.Include(x => x.FlightStatus).Where(x => x.Id == id).FirstOrDefaultAsync();

                if (currentFlight == null)
                {
                    return NotFound($"flight with id {id} not found");
                }

                if (flight.PassengersCount > 0 && currentFlight.PassengersCount != flight.PassengersCount)
                {
                    currentFlight.PassengersCount = flight.PassengersCount;
                }

                if (currentFlight.IsCritical != flight.IsCritical)
                {
                    currentFlight.IsCritical = flight.IsCritical;
                }

                if (!string.IsNullOrWhiteSpace(flight.Brand) && !currentFlight.Brand.Equals(flight.Brand))
                {
                    currentFlight.Brand = flight.Brand;
                }

                if (flight.FlightStatus?.Id > 0 && !currentFlight.FlightStatus.Id.Equals(flight.FlightStatus?.Id))
                {
                    FlightStatuses flightStatus = _dbContext.FlightStatuses.Where(x => x.Id == flight.FlightStatus.Id).FirstOrDefault();

                    if (flightStatus == null)
                    {
                        return NotFound($"flight status with id {flight.FlightStatus.Id} not found");
                    }

                    currentFlight.FlightStatus = flightStatus;
                }

                await _dbContext.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.Error($"when Calling update flight {id} thrown exception - {ex.Message}");

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateFlight([FromBody] JsonElement body)
        {
            try
            {
                _logger.Info($"Calling create flight");

                string json = System.Text.Json.JsonSerializer.Serialize(body);
                Flight flight = JsonSerializer.Deserialize<Flight>(json);

                if (flight == null)
                {
                    return BadRequest("some required fields are missing - Number, PassengersCount, Brand, Type");
                }

                string type = Enum.GetName(typeof(Types), flight.Type);

                if (flight.PassengersCount <= 0 || string.IsNullOrWhiteSpace(flight.Brand) || string.IsNullOrEmpty(type))
                {
                    return BadRequest("some required fields are missing - Number, PassengersCount, Brand, Type");
                }

                FlightStatuses waiting = _dbContext.FlightStatuses.Where(x => x.Id == 1).SingleOrDefault();
                Types flightType = Enum.Parse<Types>(type);
                flight.FlightStatus = waiting;
                flight.Type = flightType;
                flight.Number = Guid.NewGuid().ToString();

                _dbContext.Flights.Add(flight);

                await _dbContext.SaveChangesAsync();

                return Created($"/api/flights/{flight.Id}", flight.Id);
            }
            catch (Exception ex)
            {
                _logger.Error($"when Calling create flight thrown exception - {ex.Message}");

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        //POST
    }


}
