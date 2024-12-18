using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Bus
    {
        public int Id { get; set; }
        public string BusNumber { get; set; }
        public int TotalSeats { get; set; }
        public string Route { get; set; }
        public DateTime? DepartureTime { get; set; }
        public DateTime? ArrivalTime { get; set; }
        public decimal TicketPrice { get; set; }

        [ValidateNever]
        public List<string> Amenities { get; set; }
    }
}
