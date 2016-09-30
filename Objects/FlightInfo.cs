using Airport.Enums;
using Airport.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Airport
{
    public class FlightInfo
    {
        public string Number { get; set; }
        public string Destination { get; set; }
        public string DeparturePlace { get; set; }
        public string AirLine { get; set; }
        public DateTime Departure { get; set; }
        public DateTime Arrival { get; set; }
        public string Gate { get; set; }
        public FlightStatus Status { get; set; }
        public List<Passenger> Passengers { get; set; }
        public List<FlightPrice> FlightPrices { get; set; }

        public FlightInfo()
        {
            Passengers = new List<Passenger>();
            FlightPrices = new List<FlightPrice>();
        }

        public decimal GetPrice(FlightClasses type)
        {
            decimal result = 0;
            foreach (var item in FlightPrices)
            {
                if (item.Type == type)
                {
                    result = item.Cost;
                    continue;
                }
            }
            return result;
        }

        public override string ToString()
        {
            return string.Format("{0} {1} {2} {3} {4} {5} {6} {7}", this.Number, this.Destination, this.DeparturePlace, this.AirLine, this.Departure, this.Arrival, this.Gate, this.Status);
        }
    }
}
