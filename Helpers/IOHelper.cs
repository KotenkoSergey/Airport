using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport.Helpers
{
    public static class IOHelper
    {
        public static string SetStringValue(string message)
        {
            Console.WriteLine("Please enter the {0}", message);
            var str = string.Empty;
            do
            {
                str = Console.ReadLine();
                if (string.IsNullOrEmpty(str))
                {
                    DrawConsoleHeader(string.Format("{0} can't be empty", message).ToUpper(), ConsoleColor.Red);
                }
            }
            while (string.IsNullOrEmpty(str));
            return str;
        }

        public static decimal SetNumberValue(string message)
        {
            Console.WriteLine("Please enter the {0}", message);
            var num = 0;
            string str = string.Empty;
            do
            {
                str = Console.ReadLine();
                bool result = int.TryParse(str, out num);
                if (!result)
                {
                    DrawConsoleHeader(string.Format("You enter {0}. It is wrong number format", str), ConsoleColor.Red);
                }
                if (num < 0)
                {
                    DrawConsoleHeader(string.Format("You enter {0}. The number must be greater than zero", num), ConsoleColor.Red);
                }
            }
            while (string.IsNullOrEmpty(str));
            return num;
        }

        public static DateTime? SetDate(string message, string datePattern, bool isNeedDate = true)
        {
            var isCorrect = true;

            Console.WriteLine(message);

            DateTime dateTime;
            DateTime? resultTime = null;

            do
            {
                var dateStr = Console.ReadLine();

                if (!DateTime.TryParseExact(dateStr, datePattern, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
                {

                    if (isNeedDate)
                    {
                        DrawConsoleHeader("You've entered wrong date.Try again", ConsoleColor.Red);
                    }
                    isCorrect = false;

                }
                else
                {
                    resultTime = dateTime;
                    isCorrect = true;
                }

            } while (!isCorrect && isNeedDate);

            return resultTime;
        }

        public static void DrawConsoleHeader(string text, ConsoleColor color)
        {
            Console.SetCursorPosition((Console.WindowWidth - text.Length) / 2, Console.CursorTop);
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        public static void DrawInformation(string[] headerCells, List<List<string>> rows)
        {
            Console.ForegroundColor = ConsoleColor.White;

            var ct = new ConsoleTableCreater { TextAlignment = ConsoleTableCreater.AlignText.ALIGN_RIGHT };

            ct.SetHeaders(headerCells);

            foreach (var row in rows)
            {
                ct.AddRow(row);
            }

            ct.PrintTable();
        }


    }
}
