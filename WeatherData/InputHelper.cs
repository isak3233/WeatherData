using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace WeatherData
{
    internal class InputHelper
    {
        static public int GetIntFromUser(string inputString = "Skriv ett nummer: ")
        {
            while(true)
            {
                int result;
                Console.Write(inputString);
                if (int.TryParse(Console.ReadLine(), out result))
                {
                    return result;
                } else
                {
                    Console.WriteLine("Du skrev inte ett nummer.");
                }
            }
        }
        static public int GetMonthFromUser(int startMonth, int endMonth)
        {
            DateTime selectedDate = new DateTime(2016, startMonth, 1);

            int monthOn = 0;
            for (int i = startMonth; i <= endMonth; i++)
            {
                string monthString = selectedDate.AddMonths(monthOn).ToString("MMMM");
                Console.WriteLine($"{i}: {monthString}");
                monthOn++;
            }


            int selectedMonth;
            while (true)
            {
                selectedMonth = GetIntFromUser("Månad: ");
                if (selectedMonth >= startMonth && selectedMonth <= endMonth)
                {
                    return selectedMonth;
                    break;
                }
                else
                {
                    Console.WriteLine("Den utvalda månaden finns inte!");
                }
            }
        }
        static public DateTime GetDateFromUser(int startMonth, int endMonth)
        {
            DateTime selectedDate = new DateTime(2016, startMonth, 1);
            int selectedMonth = GetMonthFromUser(startMonth, endMonth);
            selectedDate = new DateTime(selectedDate.Year, selectedMonth, selectedDate.Day);

            while(true)
            {
                var days = WeatherDataCollection.GetExistingDaysInMonth(selectedDate);
                Console.WriteLine("Dagar att välja mellan:");
                foreach (int day in days)
                {
                    Console.WriteLine(day);
                }
                int selectedDay = GetIntFromUser($"Välj dag: ");
                Console.Clear();
                if (days.Contains(selectedDay))
                {
                    selectedDate = new DateTime(selectedDate.Year, selectedDate.Month, selectedDay);
                    return selectedDate;
                }
                else
                {
                    Console.WriteLine("Valt datum finns inte");
                }
                
            }


        }
    }
}
