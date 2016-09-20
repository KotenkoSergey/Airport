using Airport.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport
{
    public class Menu
    {
        FlightsSchedule schedule;
        
        public Menu() 
        {
             schedule = new FlightsSchedule(); 
        }

        public string DrawMenu()
        {
            string option = string.Empty;

            Console.Clear();

            try
            {
                IOHelper.DrawConsoleHeader("FLIGHTS MESSAGE BOARD", ConsoleColor.Green);

                schedule.DrawFlights();
                

                Console.ForegroundColor = ConsoleColor.Yellow;

                Console.WriteLine("Select operation:");
                Console.WriteLine();
                Console.WriteLine("[1] Modify fights schedule.");
                Console.WriteLine("[2] Search flight.");
                Console.WriteLine("[3] Search the nearest flight.");
                Console.WriteLine("[4] Passengers menu.");
                Console.WriteLine("[5] Exit.");                      
                
                option = Console.ReadLine();
                Console.ResetColor();
            }
            catch 
            {
                Console.WriteLine("Some error has been Occured");
            }
            return option;
        }

        public void Execute(string key)
        {
            Console.Clear();

            switch (key)
            {
                case "1":
                    ModifyFlightsMenu(key);
                    break;
                case "2":
                    FilterFlightsMenu(key);
                    break;
                case "3":
                    schedule.SearchFlightByTime();
                    Execute(DrawMenu());
                    break;
                case "4":
                    FilterPassengerMenu(key);
                    break;
                default:                
                     break;
            }
        }

        public void ModifyFlightsMenu(string key)
        {
            Console.WriteLine("\nSelect any option:");
            Console.WriteLine("[1] Add flight");
            Console.WriteLine("[2] Edit flight");
            Console.WriteLine("[3] Delete flight");
            Console.WriteLine("[4] Exit");

            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    schedule.AddFlight();
                    Execute(key);
                    break;
                case "2":
                    schedule.EditFlight();
                    Execute(key);
                    break;
                case "3":
                    schedule.RemoveFlight();
                    Execute(key);
                    break;
                default:
                    Execute(DrawMenu());
                    break;
            }
        }

        public void FilterFlightsMenu(string key)
        {
            Console.WriteLine("\nSelect any option:");
            Console.WriteLine("[1] Search by the flight number");
            Console.WriteLine("[2] Search by arrival time");
            Console.WriteLine("[3] Search by destination");
            Console.WriteLine("[4] Search by departure place");
            Console.WriteLine("[5] Exit");

            var answer = Console.ReadLine();

            Console.Clear();
            switch (answer)
            {
                case "1":
                    Console.WriteLine("Enter the number of flight to search");
                    string strNum = Console.ReadLine();

                    schedule.SearchFlight(strNum, null, null, null);
                    Execute(key);
                    break;
                case "2":
                    var strTime = IOHelper.SetDate("Please enter new the departure date and time to search (use this format dd.MM.yyyy HH:mm )", FlightsSchedule.DatePattern, true);
                    schedule.SearchFlight(null, strTime, null, null);
                    Execute(key);
                    break;
                case "3":
                    Console.WriteLine("Enter the destination of flight to search");
                    string strDest = Console.ReadLine();
                    schedule.SearchFlight(null, null, strDest, null);
                    Execute(key);
                    break;
                case "4":
                    Console.WriteLine("Enter the departure place of flight to search");
                    string strDepPlace = Console.ReadLine();
                    schedule.SearchFlight(null, null, null, strDepPlace);
                    Execute(key);
                    break;
                case "5":
                    Execute(DrawMenu());
                    break;
            }
        }

        public void FilterPassengerMenu(string key) 
        {
            Console.WriteLine("\nSelect any option:");
            Console.WriteLine("[1] Display passenger");
            Console.WriteLine("[2] Add passenger");
            Console.WriteLine("[3] Edit passenger");
            Console.WriteLine("[4] Delete passenger");
            Console.WriteLine("[5] Search passenger");
            Console.WriteLine("[6] Exit");

            var option = Console.ReadLine();

            switch (option)
            { 
                case "1":
                    schedule.DrawPassenger();
                    Execute(key);
                    break;
                case "2":
                    schedule.AddPassenger();
                    Execute(key);
                    break;
                case "3":
                    schedule.EditPassenger();
                    Execute(key);
                    break;
                case "4":
                    schedule.RemovePasssenger();
                    Execute(key);
                    break;
                case "5":
                    schedule.SearchPassenger();
                    Execute(key);
                    break;
                default:
                    Execute(DrawMenu());
                    break;
            }
        }
    }
}
