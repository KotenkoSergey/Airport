using Airport.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport
{
    public class FlightPrice
    {
        public FlightClasses Type { get; set; }
        public decimal Cost { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1}", this.Type, this.Cost);
        }
    }


}
