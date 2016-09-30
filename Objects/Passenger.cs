using Airport.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport
{
    public class Passenger
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Nationality { get; set; }
        public string Pasport { get; set; }
        public DateTime Birthday { get; set; }
        public Gender Gender { get; set; }
        public FlightClasses ClassType { get; set; }
    }
}
