using AirportProject.Context;
using Common.Models;
using log4net;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AirportProject.Services
{
    public class ProcessAirportService : IProcessAirportService
    {
        private ILog _logger;
        private DBContext _dbContext;

        public ProcessAirportService(ILog logger, DBContext dbContext)

        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public async Task ProcessAirport()
        {
            try
            {
                //var transaction = await _dbContext.Database.BeginTransactionAsync();
                var waitingFlightStatus = await _dbContext.FlightStatuses.Where(x => x.Id == 1).FirstOrDefaultAsync();
                var processingFlightStatus = await _dbContext.FlightStatuses.Where(x => x.Id == 2).FirstOrDefaultAsync();
                var completedFlightStatus = await _dbContext.FlightStatuses.Where(x => x.Id == 3).FirstOrDefaultAsync();

                var flights = await _dbContext.Flights.Where(x => x.FlightStatusId == 2).ToListAsync();

                foreach (var flight in flights)
                {
                    var exisitngLeg = await _dbContext.Legs.Include(x => x.Flight).Where(x => x.Flight.Id == flight.Id).FirstOrDefaultAsync();

                    if (exisitngLeg == null)
                    {
                        flight.UpdateStatusFlight(completedFlightStatus);
                        await _dbContext.SaveChangesAsync();
                    }
                }

                var firstLegForLanding = await _dbContext.Legs.Include(x => x.FromLegs).Include(x => x.ToLegs).Include(x => x.Flight).Where(x => x.Type == Types.Landing && x.IsFirstStop == true && x.Flight == null).FirstOrDefaultAsync();

                if (firstLegForLanding != null)
                {
                    var waitingFlight = await _dbContext.Flights.Include(x => x.FlightStatus).Where(x => x.FlightStatus.Id == 1 && x.Type == Types.Landing).FirstOrDefaultAsync();

                    if (waitingFlight != null)
                    {
                        _logger.Info($"flight number {waitingFlight.Number} entered to {firstLegForLanding.Number}");
                        firstLegForLanding.AddFlight(waitingFlight);
                        waitingFlight.UpdateStatusFlight(processingFlightStatus);
                        await _dbContext.SaveChangesAsync();
                    }


                    var waitingFlights = await _dbContext.Flights.Include(x => x.FlightStatus).Where(x => x.FlightStatus.Id == 1 && x.Type == Types.TakeOff).ToListAsync();

                    foreach (var flight in waitingFlights)
                    {
                        var firstLegForTakeOff = await _dbContext.Legs.Include(x => x.FromLegs).Include(x => x.ToLegs).Include(x => x.Flight).Where(x => x.Type != Types.Landing && x.IsFirstStop == true && x.Flight == null).ToListAsync();

                        if (firstLegForTakeOff.Find(x => x.Id == 7) != null)
                        {
                            _logger.Info($"flight number {flight.Number} entered to {firstLegForTakeOff[0].Number}");
                            firstLegForTakeOff[0].AddFlight(flight);
                            flight.UpdateStatusFlight(processingFlightStatus);
                            await _dbContext.SaveChangesAsync();
                        }
                    }
                }

                var legs = await _dbContext.Legs.Include(x => x.FromLegs).Include(x => x.ToLegs).Include(x => x.Flight).ToListAsync();

                foreach (var leg in legs)
                {
                    if (leg.Flight != null)
                    {
                        Flight flight = await _dbContext.Flights.Where(x => x.Number == leg.Flight.Number).FirstOrDefaultAsync();

                        var toLegs = leg.FromLegs.Where(x => x.Type == flight.Type).ToList();

                        if (toLegs != null && toLegs.Any())
                        {
                            bool isUpdatedLeg = false;

                            foreach (var toLeg in toLegs)
                            {
                                if (toLeg.To.Flight == null && !isUpdatedLeg)
                                {
                                    _logger.Info($"flight number {flight.Number} moved from {leg.Number} to {toLeg.To.Number}");
                                    leg.Flight = null;
                                    toLeg.To.Flight = flight;
                                    isUpdatedLeg = true;
                                }
                            }
                        }

                        else
                        {
                            _logger.Info($"flight number {flight.Number} marked as completed");
                            leg.Flight = null;
                            flight.UpdateStatusFlight(completedFlightStatus);
                        }
                    }
                    await _dbContext.SaveChangesAsync();
                    //Thread.Sleep(1000);
                }

                //await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
