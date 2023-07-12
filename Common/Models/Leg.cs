using System.Collections.Generic;

namespace Common.Models
{
    public class Leg
    {
        public long Id { get; set; }
        public int Number { get; set; }
        public int Capacity { get; set; }
        public Types Type { get; set; }
        public Flight? Flight { get; set; }
        public bool IsFirstStop { get; set; }

        public int OrderNo { get; set; }

        public IList<LegToLeg> FromLegs { get; set; }
        public IList<LegToLeg> ToLegs { get; set; }

        public Leg()
        {
            this.FromLegs = new List<LegToLeg>();
            this.ToLegs = new List<LegToLeg>();
        }

        public Leg(int number, int capacity, Types type, int orderNo, Flight? flight, bool isFirstStop=false)
        {
            this.Number = number;
            this.Capacity = capacity;
            this.Type = type;
            this.OrderNo = orderNo;
            this.Flight = flight;
            this.IsFirstStop = IsFirstStop;
            this.FromLegs = new List<LegToLeg>();
            this.ToLegs = new List<LegToLeg>();
        }

        public void AddFlight(Flight flight)
        {
            this.Flight = flight;
        }

        public void RemoveFlight()
        {
            this.Flight = null;
        }
    }
}
