using Airport.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace Airport
{
    public class FlightsSchedule
    {
        public const string DatePattern = "dd.MM.yyyy HH:mm";
        
        private FlightsManager flightsManager;

        public FlightsSchedule()
        {
            flightsManager = new FlightsManager();
        }

        public void DrawFlights() 
        {
            flightsManager.Draw();
        }

        internal void AddFlight()
        {
            try
            {
                flightsManager.Create();
            }
            catch
            {
                IOHelper.DrawConsoleHeader("Data format was incorrect ", ConsoleColor.Red);
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadLine();
        }

        internal void EditFlight()
        {
            try
            {
                flightsManager.Edit();
            }
            catch
            {
                IOHelper.DrawConsoleHeader("Data format was incorrect to press any key to continue", ConsoleColor.Red);
                Console.ReadLine();
            }
        }

        internal void RemoveFlight()
        {
            try
            {
                flightsManager.Remove();
            }
            catch
            {
                IOHelper.DrawConsoleHeader("Some error was caught", ConsoleColor.Red);
                Console.ReadLine();
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadLine();
        }

        internal void SearchFlight(string number, DateTime? arrival, string destination, string depPlace)
        {
            try
            {
                flightsManager.Search(number, arrival, destination, depPlace);
            }
            catch (Exception)
            {
                Console.WriteLine("Some error was caught");
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadLine();
        }

        internal void SearchFlightByTime()
        {
            IOHelper.DrawConsoleHeader("List of nearest flights:", ConsoleColor.Green);

            try
            {
                flightsManager.Search(null, null, null, null, true);
            }
            catch (Exception)
            {
                Console.WriteLine("Some error was caught");
            }

            IOHelper.DrawConsoleHeader("To continue press any key...", ConsoleColor.Green);
            Console.ReadLine();
        }

        internal void AddPassenger() 
        {
            try
            {
                flightsManager.AddPassenger();
            }
            catch
            {
                IOHelper.DrawConsoleHeader("Data format was incorrect ", ConsoleColor.Red);
            }
            
            IOHelper.DrawConsoleHeader("The passenger was added, press any key to continue", ConsoleColor.Green);
            Console.ReadLine();
        }

        internal void EditPassenger() 
        {
            try
            {
                flightsManager.EditPassenger();
            }
            catch
            {
                IOHelper.DrawConsoleHeader("Data format was incorrect to press any key to continue", ConsoleColor.Red);
                Console.ReadLine();
            }
        }

        internal void RemovePasssenger() 
        {
            try
            {
                flightsManager.DeletePassenger();
            }
            catch
            {
                IOHelper.DrawConsoleHeader("Some error was caught", ConsoleColor.Red);
                Console.ReadLine();
            }
        }

        internal void SearchPassenger() 
        {
            try
            {
                flightsManager.SearchPassenger();
            }
            catch
            {
                IOHelper.DrawConsoleHeader("Some error was caught", ConsoleColor.Red);
                Console.ReadLine();
            }
        }

        internal void DrawPassenger() 
        {
            flightsManager.DrawPass();
        }
    }
}
