using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Models
{
    public class LegToLeg
    {
        public long Id { get; set; }

        [ForeignKey("FromId")]
        public Leg From { get; set; }
        public long? FromId { get; set; }

        [ForeignKey("ToId")]
        public Leg To { get; set; }
        public long? ToId { get; set; }

        public Types Type { get; set; }

        public LegToLeg()
        {

        }

        public LegToLeg(Leg from, Leg to, Types type)
        {
            this.From = from;
            this.To = to;
            this.Type = type;
        }
    }
}
