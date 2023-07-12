using System;

namespace Common.Models
{
    public class Flight
    {
        public long Id { get; set; }
        public string Number { get; set; }
        public int PassengersCount { get; set; }
        public bool IsCritical { get; set; }
        public string Brand { get; set; }
        public Types Type { get; set; }
        public FlightStatuses FlightStatus { get; set; }
        public long FlightStatusId { get; set; }

        public DateTime LastUpdate { get; set; }

        public Flight()
        {

        }

        public Flight(string number, int passengersCount, bool isCritical, string brand, Types type, FlightStatuses flightStatuses)
        {
            this.Number = number;
            this.PassengersCount = passengersCount;
            this.IsCritical = isCritical;
            this.Brand = brand;
            this.Type = type;
            this.FlightStatus = flightStatuses;
            this.LastUpdate = DateTime.Now;
        }

        public void UpdateStatusFlight(FlightStatuses flightStatus)
        {
            this.FlightStatus = flightStatus;
            this.LastUpdate = DateTime.Now;
        }

    }
}
