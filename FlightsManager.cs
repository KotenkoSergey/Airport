using Airport.Enums;
using Airport.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport
{
    public class FlightsManager
    {


        public const string DatePattern = "dd.MM.yyyy HH:mm";

        private List<FlightInfo> flights;


        public FlightsManager()
        {
            Fill();
        }

        #region flight methods

        private void Fill()
        {
            DateTime dateNow = DateTime.Now;

            flights = new List<FlightInfo>()
            {
                new FlightInfo {Number = "AC-231", Destination = "London", DeparturePlace = "Moscow", AirLine = "WizAir", 
                    Departure = new DateTime(dateNow.Year, dateNow.Month, dateNow.AddDays(1).Day, 15,30,0), 
                    Arrival = new DateTime(dateNow.Year, dateNow.Month, dateNow.AddDays(1).Day, dateNow.AddHours(4).Hour, 20,0), 
                    Gate ="D-09", Status = FlightStatus.checkIn, 
                    FlightPrices = new List<FlightPrice>(){new FlightPrice{Type = FlightClasses.Economy, Cost = 500}, new FlightPrice{Type = FlightClasses.Business, Cost = 700}}},
                new FlightInfo {Number = "AC-236", Destination = "Barselona", DeparturePlace = "Kharkov", AirLine = "MAU" , 
                    Departure = new DateTime(dateNow.Year, dateNow.Month, dateNow.AddDays(2).Day, 10,15,0), 
                    Arrival = new DateTime(dateNow.Year, dateNow.Month, dateNow.AddDays(2).Day, dateNow.AddHours(8).Hour, 0,0), 
                    Gate ="D-11", Status = FlightStatus.inFlight,
                    FlightPrices = new List<FlightPrice>(){new FlightPrice{Type = FlightClasses.Economy, Cost = 500}}},
                new FlightInfo {Number = "AC-178", Destination = "Madrid", DeparturePlace = "Warshava", AirLine = "WizAir", 
                    Departure = new DateTime(dateNow.Year, dateNow.Month, dateNow.Day, dateNow.Hour, 45, 0), 
                    Arrival = new DateTime(dateNow.Year, dateNow.Month, dateNow.Day, dateNow.AddHours(2).Hour, 10,0), 
                    Gate ="M-09", Status = FlightStatus.delayed,
                    FlightPrices = new List<FlightPrice>(){new FlightPrice{Type = FlightClasses.Economy, Cost = 280}, new FlightPrice{Type = FlightClasses.Business, Cost = 450},
                        }},
                new FlightInfo {Number = "AC-155", Destination = "Los Angeles", DeparturePlace = "Toronto", AirLine = "Low cost", 
                    Departure = new DateTime(dateNow.Year, dateNow.Month, dateNow.AddDays(1).Day, 22,10,0), 
                    Arrival = new DateTime(dateNow.Year, dateNow.Month, dateNow.AddDays(2).Day, 10,15,0), 
                    Gate ="A-19", Status = FlightStatus.arrived,
                    FlightPrices = new List<FlightPrice>(){new FlightPrice{Type = FlightClasses.Economy, Cost = 195}, new FlightPrice{Type = FlightClasses.Business, Cost = 320}, 
                        }},
                new FlightInfo {Number = "AC-88", Destination = "New York", DeparturePlace = "Mexico", AirLine = "Low cost", 
                    Departure = new DateTime(dateNow.Year, dateNow.Month, dateNow.Day, dateNow.AddHours(1).Hour, 30,0), 
                    Arrival = new DateTime(dateNow.Year, dateNow.Month, dateNow.Day, dateNow.AddHours(3).Hour, 30,0), 
                    Gate ="K-01", Status = FlightStatus.canceled,
                    FlightPrices = new List<FlightPrice>(){new FlightPrice{Type = FlightClasses.Economy, Cost = 480}, new FlightPrice{Type = FlightClasses.Business, Cost = 800}}},                
                null,
                null
            };
        }
        public void Create()
        {
            Console.Clear();

            Console.WriteLine(@"Do you want to add more flights? Y\N");

            var choice = Console.ReadLine();

            switch (choice.ToUpper())
            {
                case "Y":

                    var flight = new FlightInfo();

                    flight.Number = IOHelper.SetStringValue("number of flight");

                    flight.Destination = IOHelper.SetStringValue("destination of flight");

                    flight.DeparturePlace = IOHelper.SetStringValue("departure place of flight");

                    flight.AirLine = IOHelper.SetStringValue("airline of flight");

                    flight.Departure = IOHelper.SetDate("Please enter the departure date and time (use this format dd.MM.yyyy HH:mm )", DatePattern) ?? DateTime.Now;

                    flight.Arrival = IOHelper.SetDate("Please enter the arrival date and time (use this format dd.MM.yyyy HH:mm )", DatePattern) ?? DateTime.Now;

                    flight.Gate = IOHelper.SetStringValue("gate of flight");

                    flight.Status = (FlightStatus)Enum.Parse(typeof(FlightStatus), IOHelper.SetStringValue(@"status of flight 
(use this status checkIn, gateClosed, arrived, departedAt, unknown, canceled, expectedAt, delayed, inFlight)"));

                    AddPrices(flight);

                    flights.Add(flight);
                    IOHelper.DrawConsoleHeader("Flight was added successfully", ConsoleColor.Green);
                    break;

                default:
                    break;
            }
        }
        public void Edit()
        {
            Console.Clear();

            Draw();

            Console.WriteLine("Enter the number of flight to edit");
            var choice = Console.ReadLine();

            FlightInfo flight = null;

            foreach (var item in flights)
            {
                if (item != null && item.Number == choice)
                {
                    flight = item;
                    break;
                }
            }

            if (flight == null)
            {
                IOHelper.DrawConsoleHeader("We could not find a flight, do you want to continue? Press any button", ConsoleColor.Red);
                Console.ReadLine();
                return;
            }

            Console.Clear();

            IOHelper.DrawConsoleHeader(string.Format("Flight: {0} has been found. Press any key to continue", choice), ConsoleColor.Green);
            Draw(new List<FlightInfo> { flight });
            Console.ReadLine();

            Console.WriteLine("Please enter new number or press enter if you don't want to change");
            var number = Console.ReadLine();
            if (!String.IsNullOrEmpty(number))
                flight.Number = number;

            Console.WriteLine("Please enter new destination or press enter if you don't want to change");
            var destination = Console.ReadLine();
            if (!String.IsNullOrEmpty(destination))
                flight.Destination = destination;

            Console.WriteLine("Please enter new departure place or press enter if you don't want to change");
            var depPlace = Console.ReadLine();
            if (!String.IsNullOrEmpty(depPlace))
                flight.DeparturePlace = depPlace;

            Console.WriteLine("Please enter new airline or press enter if you don't want to change");
            var line = Console.ReadLine();
            if (!String.IsNullOrEmpty(line))
                flight.AirLine = line;

            var depTime = IOHelper.SetDate("Please enter new the departure date and time or press enter (use this format dd.MM.yyyy HH:mm )", DatePattern, false);
            if (depTime != null)
                flight.Departure = (DateTime)depTime;


            var arrTime = IOHelper.SetDate("Please enter new the departure date and time or press enter (use this format dd.MM.yyyy HH:mm )", DatePattern, false);
            if (arrTime != null)
                flight.Arrival = (DateTime)arrTime;

            Console.WriteLine("Please enter new gate or press enter if you don't want to change");
            var gate = Console.ReadLine();
            if (!String.IsNullOrEmpty(gate))
                flight.Gate = gate;

            Console.WriteLine(@"Please enter new status or press enter if you don't want to change 
(use this status checkIn, gateClosed, arrived, departedAt, unknown, canceled, expectedAt, delayed, inFlight)");
            var status = Console.ReadLine();
            if (!String.IsNullOrEmpty(status))
                flight.Status = (FlightStatus)Enum.Parse(typeof(FlightStatus), status);

            AddPrices(flight);

            IOHelper.DrawConsoleHeader("Changes have been made, press any key to continue", ConsoleColor.Green);
            Console.ReadLine();
        }
        public void Remove()
        {
            Console.WriteLine(@"Do you want to remove flights? Y\N");

            var choice = Console.ReadLine();

            Console.Clear();

            Draw();

            switch (choice.ToUpper())
            {
                case "Y":

                    Console.WriteLine("Enter the number of flight to delete");
                    string num = Console.ReadLine();


                    foreach (var item in flights)
                    {
                        if (item != null && item.Number == num)
                        {
                            flights.Remove(item);
                            IOHelper.DrawConsoleHeader("Flight was remove successfully", ConsoleColor.Green);
                            break;
                        }
                    }
                    break;

                default:
                    break;
            }
        }
        public void Search(string number, DateTime? arrival, string destination, string depPlace, bool isLastTime = false)
        {
            Console.Clear();

            List<FlightInfo> fc = new List<FlightInfo>();
            var dateNow = DateTime.Now;

            foreach (var item in flights)
            {
                if (!string.IsNullOrEmpty(number) && item.Number == number)
                {
                    fc.Add(item);
                    break;
                }
                else if (arrival != null && DateTime.Compare(item.Arrival, (DateTime)arrival) == 0)
                {
                    fc.Add(item);
                    break;
                }
                else if (!string.IsNullOrEmpty(destination) && item.Destination == destination)
                {
                    fc.Add(item);
                    break;
                }
                else if (!string.IsNullOrEmpty(depPlace) && item.DeparturePlace == depPlace)
                {
                    fc.Add(item);
                    break;
                }

                else if (item != null && isLastTime &&
                     ((item.Arrival <= dateNow.AddHours(1) && item.Arrival >= dateNow)
                     || (item.Departure <= dateNow.AddHours(1) && item.Departure >= dateNow)))
                {
                    fc.Add(item);
                    //break;
                }
            }

            if (fc.Count() == 0)
                IOHelper.DrawConsoleHeader("Sorry, flights were not found", ConsoleColor.Red);
            else
                Draw(fc);

           // IOHelper.DrawConsoleHeader("Press any key to continue", ConsoleColor.Green);
           // Console.ReadLine();
        }

        public void Draw()
        {
            Draw(this.flights);
        }

        public void Draw(List<FlightInfo> flightsCollection)
        {
            var fl = new List<List<string>>();

            foreach (var flight in flightsCollection)
            {
                if (flight == null) continue;

                string prices = string.Empty;
                foreach (var item in flight.FlightPrices)
                {
                    prices += string.Format("{0}:{1} ", item.Type, item.Cost);
                }

                fl.Add(new List<string> { flight.Number, flight.Destination, flight.DeparturePlace, flight.AirLine, flight.Departure.ToString(), flight.Arrival.ToString(), flight.Gate, flight.Status.ToString(), prices });
            }

            IOHelper.DrawInformation(new[] { "Number", "Destination", "Departure", "AirLine", "Departure time", "Arrival time", "Gate", "Status", "FlightPrices" }, fl);
        }

        #endregion

        #region passenger methods

        public void AddPassenger()
        {
            Console.Clear();

            Draw();

            FlightInfo flight = SearchByNumber();

            Console.Clear();

            if (flight == null) return;

            var passenger = new Passenger();

            Console.WriteLine("Please, enter the first name of the passenger");
            passenger.FirstName = Console.ReadLine();

            Console.WriteLine("Please, enter the second name of the passenger");
            passenger.SecondName = Console.ReadLine();

            Console.WriteLine("Please, enter the nationality of the passenger");
            passenger.Nationality = Console.ReadLine();

            Console.WriteLine("Please, enter the pasport of the passenger");
            passenger.Pasport = Console.ReadLine();

            passenger.Birthday = (DateTime)IOHelper.SetDate("Please, enter the birthday of the passenger (use this format dd.MM.yyyy)", "dd.MM.yyyy", true);

            Console.WriteLine("Please, enter the gender (Male or Female) of the passenger");
            passenger.Gender = (Gender)Enum.Parse(typeof(Gender), Console.ReadLine());

            Console.WriteLine("Please, enter the flight class (Business or Economy) of the passenger");
            passenger.ClassType = (FlightClasses)Enum.Parse(typeof(FlightClasses), Console.ReadLine());

            flight.Passengers.Add(passenger);
        }

        public void DeletePassenger()
        {
            Console.Clear();

            FlightInfo flight = SearchByNumber();

            if (flight == null) return;

            Console.WriteLine("Please, enter the first name of the passenger to delete");
            string firstName = Console.ReadLine();

            Console.WriteLine("Please, enter the second name of the passenger to delete");
            string secondName = Console.ReadLine();

            bool isFind = false;
            foreach (var item in flight.Passengers)
            {
                if (item.FirstName == firstName && item.SecondName == secondName)
                {
                    flight.Passengers.Remove(item);
                    isFind = true;
                    break;
                }

            }
            IOHelper.DrawConsoleHeader(string.Format("{0} press any key to continue...", !isFind ? "Sorry, but the passenger does not exist" : "User was deleted"), !isFind ? ConsoleColor.Red : ConsoleColor.Green);
            Console.ReadLine();
        }

        public void EditPassenger()
        {
            try
            {
                Console.Clear();

                FlightInfo flight = SearchByNumber();

                if (flight == null) return;

                Console.WriteLine("Please, enter the first name of the passenger to edit");
                string firstName = Console.ReadLine();

                Console.WriteLine("Please, enter the second name of the passenger to edit");
                string secondName = Console.ReadLine();

                Passenger passenger = null;

                foreach (var item in flight.Passengers)
                {
                    if (item.FirstName == firstName && item.SecondName == secondName)
                    {
                        passenger = item;
                        break;
                    }
                }

                if (passenger == null)
                {
                    IOHelper.DrawConsoleHeader("We could not find a passenger, do you want to continue? Press any button", ConsoleColor.Red);
                    Console.ReadLine();
                    return;
                }

                Console.Clear();

                DrawPassenger(new List<Passenger> { passenger });

                Console.WriteLine("Please enter new first name or press enter if you don't want to change");
                var pasFirstName = Console.ReadLine();
                if (!String.IsNullOrEmpty(pasFirstName))
                    passenger.FirstName = pasFirstName;

                Console.WriteLine("Please enter new second name or press enter if you don't want to change");
                var pasSecondName = Console.ReadLine();
                if (!String.IsNullOrEmpty(pasSecondName))
                    passenger.SecondName = pasSecondName;

                Console.WriteLine("Please enter new nationality or press enter if you don't want to change");
                var nationality = Console.ReadLine();
                if (!String.IsNullOrEmpty(nationality))
                    passenger.Nationality = nationality;

                Console.WriteLine("Please enter new pasport or press enter if you don't want to change");
                var pasport = Console.ReadLine();
                if (!String.IsNullOrEmpty(pasport))
                    passenger.Pasport = pasport;

                var birthday = (DateTime)IOHelper.SetDate("Please, enter the new birthday of the passenger (use this format dd.MM.yyyy)", "dd.MM.yyyy", true);
                if (birthday != null)
                    passenger.Birthday = birthday;

                Console.WriteLine("Please, enter the new gender (Male or Female) of the passenger");
                var gender = Console.ReadLine();
                if (!String.IsNullOrEmpty(gender))
                    passenger.Gender = (Gender)Enum.Parse(typeof(Gender), gender);

                Console.WriteLine("Please, enter the new flight class (Business or Economy) of the passenger");
                var flClass = Console.ReadLine();
                if (!String.IsNullOrEmpty(flClass))
                    passenger.ClassType = (FlightClasses)Enum.Parse(typeof(FlightClasses), flClass);

                IOHelper.DrawConsoleHeader("Changes have been made, press any key to continue", ConsoleColor.Green);
                Console.ReadLine();
            }

            catch
            {
                IOHelper.DrawConsoleHeader("Data format was incorrect to press any key to continue", ConsoleColor.Red);
                Console.ReadLine();
            }
        }

        public void SearchPassenger()
        {
            Console.Clear();
            Console.WriteLine("Enter the first name of passenger");
            string firstName = Console.ReadLine();
            Console.WriteLine("Enter the second name of passenger");
            string secondName = Console.ReadLine();


            List<List<string>> pass = new List<List<string>>();
            foreach (var item in flights)
            {
                if (item == null) continue;
                foreach (var pas in item.Passengers)
                {
                    if (pas.FirstName == firstName && pas.SecondName == secondName)
                    {
                        pass.Add(new List<string> { item.Number, pas.FirstName, pas.SecondName, pas.Nationality, pas.Pasport, pas.Birthday.ToShortDateString(), pas.Gender.ToString(), pas.ClassType.ToString() });
                    }
                    
                }
            }
            if (pass.Count == 0)
            {
                IOHelper.DrawConsoleHeader("We could not find a passenger, do you want to continue? Press any button", ConsoleColor.Red);
                Console.ReadLine();
                return;
            }
            IOHelper.DrawInformation(new[] { "FlightNumber", "FirstName", "SecondName", "Nationality", "Pasport", "Birthday", "Gender", "ClassType" }, pass);
            IOHelper.DrawConsoleHeader("Press any key to continue", ConsoleColor.Green);
            Console.ReadLine();
        }

        public void DrawPassenger(List<Passenger> passengersCollection)
        {
            var pass = new List<List<string>>();

            foreach (var passenger in passengersCollection)
            {
                if (passenger == null) continue;

                pass.Add(new List<string> { passenger.FirstName, passenger.SecondName, passenger.Nationality, passenger.Pasport, passenger.Birthday.ToShortDateString(), passenger.Gender.ToString(), passenger.ClassType.ToString() });
            }

            IOHelper.DrawInformation(new[] { "FirstName", "SecondName", "Nationality", "Pasport", "Birthday", "Gender", "ClassType" }, pass);
        }

        public void DrawPass()
        {
            Console.Clear();
            FlightInfo flight = SearchByNumber();

            DrawPassenger(flight.Passengers);
            Console.WriteLine("Press any key to continue...");
            Console.ReadLine();
        }

        #endregion

        private FlightInfo SearchByNumber()
        {

            Console.WriteLine("Enter the number of flight");
            string numFl = Console.ReadLine();

            FlightInfo flightPas = null;

            foreach (var item in flights)
            {
                if (item.Number == numFl)
                {
                    flightPas = item;
                    break;
                }
            }

            if (flightPas == null)
            {
                IOHelper.DrawConsoleHeader("We could not find a flight, do you want to continue? Press any button", ConsoleColor.Red);
                Console.ReadLine();
                return null;
            }

            IOHelper.DrawConsoleHeader(string.Format("Flight: {0} has been found", numFl), ConsoleColor.Green);
            Draw(new List<FlightInfo> { flightPas });

            return flightPas;
        }

        private void AddPrices(FlightInfo flight)
        {
            Console.WriteLine("You can add these types {0}", string.Join(" or ", Enum.GetNames(typeof(FlightClasses))));
            string answer = string.Empty;
            do
            {
                var price = new FlightPrice();
                Console.WriteLine("Please enter the type of flight");

                var flClass = Console.ReadLine();
                if (!String.IsNullOrEmpty(flClass))
                    price.Type = (FlightClasses)Enum.Parse(typeof(FlightClasses), flClass);

                Console.WriteLine("Please enter the cost of flight");
                price.Cost = Decimal.Parse(Console.ReadLine());

                for (int i = 0; i < flight.FlightPrices.Count; i++)
                {
                    if (flight.FlightPrices[i].Type == price.Type)
                    {
                        flight.FlightPrices.Remove(flight.FlightPrices[i]);
                    }
                }

                flight.FlightPrices.Add(price);
                Console.WriteLine("Do you want to continue? Y/N");
                answer = Console.ReadLine();

            }
            while (answer.ToUpper() == "Y");
        }
    }

}
