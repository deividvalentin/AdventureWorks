using System;

namespace API.Models
{
    public class SalesReason
    {
        public int SalesReasonId { get; set; }
        public string Name { get; set; }
        public string ReasonType { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}