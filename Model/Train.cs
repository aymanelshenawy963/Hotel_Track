using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Train
    {
        public int TrainId { get; set; }
        public string TrainNumber { get; set; }
        public int TotalCoaches { get; set; }
        public string Route { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public decimal TicketPrice { get; set; }
        public string ClassType { get; set; }
    }
}
