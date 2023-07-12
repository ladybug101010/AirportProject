namespace Common.Models
{
    public class FlightStatuses
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public FlightStatuses()
        {

        }

        public FlightStatuses(string name)
        {
            this.Name = name;
        }
    }
}
