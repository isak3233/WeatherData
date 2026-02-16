using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace WeatherData.Weather
{
    internal class WeatherService
    {
        public static async Task ShowAverageTempForDay(DateTime date, bool inhouse)
        {
            var weatherData = await WeatherDataCollection.GetWeatherDataForDay(date, inhouse);
            var averageTemp = WeatherDataCalculation.AverageTemp(weatherData);
            Console.WriteLine($"Medeltemperaturen den {date:yyyy-MM-dd} var {averageTemp:F2} °C");
        }

        public static async Task ShowAverageTempAndHumidForDay(DateTime date, bool inhouse)
        {
            var weatherData = await WeatherDataCollection.GetWeatherDataForDay(date, inhouse);
            var averageTemp = WeatherDataCalculation.AverageTemp(weatherData);
            var averageHumidity = WeatherDataCalculation.AverageHumidity(weatherData);
            Console.WriteLine($"Medeltemperaturen den {date:yyyy-MM-dd} var {averageTemp:F2} °C och luftfuktigheten var {averageHumidity}");
        }
        public static async Task ShowHotColdRanking(int month, bool inhouse)
        {
            var selectedDate = new DateTime(2016, month, 1);
            var days = await WeatherDataCollection.GetExistingDaysInMonth(selectedDate);
            List<(string date, decimal temp)> allTempAverage = new List<(string, decimal)>();

            foreach (int day in days)
            {
                selectedDate = new DateTime(2016, month, day);
                var weatherData = WeatherDataCollection.GetWeatherDataForDay(selectedDate, inhouse);
                var averageTemp = WeatherDataCalculation.AverageTemp(await weatherData);
                allTempAverage.Add((selectedDate.ToString("yyyy-MM-dd"), averageTemp));
            }

            var sortedAllTempAverage = allTempAverage.OrderByDescending(t => t.temp).ToList();

            Console.WriteLine("Varmaste");
            foreach (var (date, temp) in sortedAllTempAverage)
            {
                Console.WriteLine($"{date}, {temp:F2}");
            }
            Console.WriteLine("Kallaste");
        }
        public static async Task ShowHumidityRanking(int month, bool inhouse)
        {
            var selectedDate = new DateTime(2016, month, 1);
            var days = await WeatherDataCollection.GetExistingDaysInMonth(selectedDate);

            List<(string Date, decimal humidity)> allHumidityAverage = new List<(string, decimal)>();

            foreach (int day in days)
            {
                selectedDate = new DateTime(2016, month, day);
                var weatherData = WeatherDataCollection.GetWeatherDataForDay(selectedDate, inhouse);
                var averageHumidity = WeatherDataCalculation.AverageHumidity(await weatherData);
                allHumidityAverage.Add((selectedDate.ToString("yyyy-MM-dd"), averageHumidity));
            }

            var sortedAllHumidityAverage = allHumidityAverage.OrderBy(t => t.humidity).ToList();

            Console.WriteLine("Torast");
            foreach (var (date, humidity) in sortedAllHumidityAverage)
            {
                Console.WriteLine($"{date}, {humidity:F2}");
            }
            Console.WriteLine("Fuktigaste");
        }
        public static async Task ShowMoldRisk(int month, bool inhouse) 
        {
            var selectedDate = new DateTime(2016, month, 1);
            var days = await WeatherDataCollection.GetExistingDaysInMonth(selectedDate);

            List<(DateTime Date, decimal mold)> allMoldAverage = new List<(DateTime, decimal)>();
            foreach (int day in days)
            {
                
                selectedDate = new DateTime(2016, month, day);
                var weatherData = WeatherDataCollection.GetWeatherDataForDay(selectedDate, inhouse);
                var averageMoldRisk = WeatherDataCalculation.AverageMoldRisk(await weatherData);
                allMoldAverage.Add((selectedDate, averageMoldRisk));
            }
            var sortedAllMoldAverage = allMoldAverage.OrderByDescending(m => m.mold);
            Console.WriteLine("Allt över 20 så finns det risk för mögel växt");
            foreach(var (date, mold) in sortedAllMoldAverage)
            {
                Console.WriteLine($"{date.ToString("yyyy-MM-dd")}: {mold} risk");
            }
            var riskForMoldOverDays = WeatherDataCalculation.RiskForMoldOverDays(allMoldAverage);

            if(riskForMoldOverDays.Count != 0)
            {
                Console.WriteLine("Översikt: ");
            }
            
            foreach (var (startMoldingDate, endMoldingDate) in riskForMoldOverDays)
            {
                if(endMoldingDate.Day - startMoldingDate.Day >= 14)
                {
                    Console.WriteLine($"Finns stor risk att det finns mögel som växt mellan dagarna: {startMoldingDate.ToString("yyyy-MM-dd")} och {endMoldingDate.ToString("yyyy-MM-dd")}");
                }
                else if(endMoldingDate.Day - startMoldingDate.Day >= 7)
                {
                    Console.WriteLine($"Finns risk för mögel som har växt under dagarna: {startMoldingDate.ToString("yyyy-MM-dd")} och {endMoldingDate.ToString("yyyy-MM-dd")}");
                }
                else if (endMoldingDate.Day - startMoldingDate.Day >= 2)
                {
                    Console.WriteLine($"Finns risk för att mögel skulle kunna börja växa denna månaden mellan dagarna: {startMoldingDate.ToString("yyyy-MM-dd")} och {endMoldingDate.ToString("yyyy-MM-dd")}");
                }
                
            }

        }
        public static async Task ShowMetrologicalAutumn()
        {
            var weatherData = await WeatherDataCollection.GetAllWeatherData(false);
            var averageTemps = WeatherDataCalculation.AverageTempForDays(weatherData);
            var metrologicalDates = WeatherDataCalculation.MetrologicalTime(averageTemps, 10);

            if ((metrologicalDates[1] - metrologicalDates[0]).Days + 1 < 5)
            {
                Console.WriteLine("Hittade inte när den metrologiska höstens startade!");
                return;
            }

            string date1 = metrologicalDates[0].ToString("yyyy-MM-dd");
            string date2 = metrologicalDates[1].ToString("yyyy-MM-dd");
            Console.WriteLine($"Den metrologiska hösten startade efter datumen {date1} och {date2}");
        }
        public static async Task ShowMetrologicalWinter()
        {
            var weatherData = await WeatherDataCollection.GetAllWeatherData(false);
            var averageTemps = WeatherDataCalculation.AverageTempForDays(weatherData);
            var metrologicalDates = WeatherDataCalculation.MetrologicalTime(averageTemps, 0);

            string date1 = metrologicalDates[0].ToString("yyyy-MM-dd");
            string date2 = metrologicalDates[1].ToString("yyyy-MM-dd");

            if ((metrologicalDates[1] - metrologicalDates[0]).Days + 1 < 5)
            {
                
                Console.WriteLine("Hittade inte när den metrologiska vintern startade!");
                Console.WriteLine($"Men va som närmast mellan datumna {date1} och {date2}");
                return;
            }

            
            Console.WriteLine($"Den metrologiska vintern startade efter datumen {date1} och {date2}");
        }
        public static async Task WriteDataToFiles()
        { 
           
            WeatherDataWriter.WriteAverageHumidity();
            WeatherDataWriter.WriteAverageMold();
            WeatherDataWriter.WriteAverageTemp();
            WeatherDataWriter.WriteFallAndWinter();
            WeatherDataWriter.WriteOutMoldAlgo();


        }
    }
}
