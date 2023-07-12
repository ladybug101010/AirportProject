namespace AirportProject.Queries
{
    public class FlightQuery
    {
        public int Limit { get; set; } = 25;
        public int Page { get; set; } = 1;
        public int? StatusId { get; set; }
    }
}
