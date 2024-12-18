using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModels
{
    public class HotelPaginationViewModel
    {
        public List<Hotel> Hotels { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}