using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Won.Shared.Dtos
{
    public class CreateTripDto
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Location { get; set; }
        public string Details { get; set; }
        public int Budget { get; set; }
        public int GroupSize { get; set; }
    }
}
