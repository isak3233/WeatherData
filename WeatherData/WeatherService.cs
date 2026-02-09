using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherData
{
    internal class WeatherService
    {
        public static void ShowAverageTempForDay(DateTime date, bool inhouse)
        {
            var weatherData = WeatherDataCollection.GetWeatherDataForDay(date, inhouse);
            var averageTemp = WeatherDataCalculation.AverageTemp(weatherData);
            Console.WriteLine($"Medeltemperaturen den {date:yyyy-MM-dd} var {averageTemp:F2} °C");
        }
        public static void ShowHotColdRanking(int month)
        {
            var selectedDate = new DateTime(2016, month, 1);
            var days = WeatherDataCollection.GetExistingDaysInMonth(selectedDate);
            List<(string Date, decimal Temp)> allAverage = new List<(string, decimal)>();

            foreach (int day in days)
            {
                selectedDate = new DateTime(2016, month, day);
                var weatherData = WeatherDataCollection.GetWeatherDataForDay(selectedDate, true);
                allAverage.Add((selectedDate.ToString("yyyy-MM-dd"), WeatherDataCalculation.AverageTemp(weatherData)));
            }

            var sortedAllAverage = allAverage.OrderByDescending(t => t.Temp).ToList();

            Console.WriteLine("Varmaste");
            foreach (var (date, temp) in sortedAllAverage)
            {
                Console.WriteLine($"{date}, {temp:F2}");
            }
            Console.WriteLine("Kallaste");
        }
    }
}
