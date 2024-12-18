using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Car
    {
        public int Id { get; set; }
        public string CarModel { get; set; }
        public string LicensePlate { get; set; }
        public int Capacity { get; set; } 
        public decimal RentPricePerDay { get; set; }
        public bool IsAvailable { get; set; }
        public string Location { get; set; }
    }
}
