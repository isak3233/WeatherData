using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherData.Weather
{
    internal class WeatherDataWriter
    {
        public static async Task WriteAverageHumidity()
        {
            List<string> humidityLines = new List<string>();
            for (int i = 6; i < 13; i++)
            {
                var date = new DateTime(2016, i, 1);
                var weatherDataInhouse = WeatherDataCollection.GetWeatherDataForMonth(date, true);
                var weatherDataOuthouse = WeatherDataCollection.GetWeatherDataForMonth(date, false);
                var averageHumidityInhouse = WeatherDataCalculation.AverageHumidity(await weatherDataInhouse);
                var averageHumidityOuthouse = WeatherDataCalculation.AverageHumidity(await weatherDataOuthouse);
                humidityLines.Add($"{date.ToString("MMMM")}: Medel fuktigheten inomhus {averageHumidityInhouse}%, Medel fuktigheten utomhus {averageHumidityOuthouse}%");
            }
            await File.WriteAllLinesAsync("../../../data/AverageHumidityPerMonth.txt", humidityLines);
        }
        public static async Task WriteAverageMold()
        {
            List<string> moldLines = new List<string>();
            for (int i = 6; i < 13; i++)
            {
                var date = new DateTime(2016, i, 1);
                var weatherDataInhouse = WeatherDataCollection.GetWeatherDataForMonth(date, true);
                var weatherDataOuthouse = WeatherDataCollection.GetWeatherDataForMonth(date, false);
                var averageMoldInhouse = WeatherDataCalculation.AverageMoldRisk(await weatherDataInhouse);
                var averageMoldOuthouse = WeatherDataCalculation.AverageMoldRisk(await weatherDataOuthouse);
                moldLines.Add($"{date.ToString("MMMM")}: Medel mögel risken inomhus {averageMoldInhouse} risk, Medel mögel risken utomhus {averageMoldOuthouse} risk");
            }
            await File.WriteAllLinesAsync("../../../data/AverageMoldRiskPerMonth.txt", moldLines);
        }
        public static async Task WriteAverageTemp()
        {
            List<string> tempLines = new List<string>();
            for (int i = 6; i < 13; i++)
            {
                var date = new DateTime(2016, i, 1);
                var weatherDataInhouse = WeatherDataCollection.GetWeatherDataForMonth(date, true);
                var weatherDataOuthouse = WeatherDataCollection.GetWeatherDataForMonth(date, false);
                var averageTempInhouse = WeatherDataCalculation.AverageTemp(await weatherDataInhouse);
                var averageTempOuthouse = WeatherDataCalculation.AverageTemp(await weatherDataOuthouse);
                tempLines.Add($"{date.ToString("MMMM")}: Medel tempraturen inomhus {averageTempInhouse} °C, Medel tempraturen utomhus {averageTempOuthouse} °C");
            }
            await File.WriteAllLinesAsync("../../../data/AverageTempPerMonth.txt", tempLines);
        }

        public static async Task WriteFallAndWinter()
        {
            var weatherDataOuthouse = WeatherDataCollection.GetAllWeatherData(false);
            var averageTemps = WeatherDataCalculation.AverageTempForDays(await weatherDataOuthouse);
            var fallTime = WeatherDataCalculation.MetrologicalTime(averageTemps, 10);
            var winterTime = WeatherDataCalculation.MetrologicalTime(averageTemps, 0);

            var fallDate1 = fallTime[0].ToString("yyyy-MM-dd");
            var fallDate2 = fallTime[1].ToString("yyyy-MM-dd");

            var winterDate1 = winterTime[0].ToString("yyyy-MM-dd");
            var winterDate2 = winterTime[1].ToString("yyyy-MM-dd");

            List<string> linesToWrite = new List<string>();
            if ((fallTime[1] - fallTime[0]).Days + 1 >= 5)
            {
                linesToWrite.Add($"Den metrologiska hösten startade mellan datumna: {fallDate1} och {fallDate2}");
            } else
            {
                linesToWrite.Add($"Den metrologiska hösten startade inte men var närmast datumna: {fallDate1} och {fallDate2}");
            }

            if ((winterTime[1] - winterTime[0]).Days + 1 >= 5)
            {
                linesToWrite.Add($"Den metrologiska vintern startade mellan datumna: {winterDate1} och {winterDate2}");
            }
            else
            {
                linesToWrite.Add($"Den metrologiska vintern startade inte men var närmast datumna: {winterDate1} och {winterDate2}");
            }

            await File.WriteAllLinesAsync("../../../data/FallAndWinterTime.txt", linesToWrite);


        }
        public static async Task WriteOutMoldAlgo()
        {
            List<string> moldAlgo = new List<string>()
            {
                "risk = humidity procent - (100 - temprature)",

                "risk < -50 = 0 risk",

                "risk <= 0 then = (risk + 50) / 50 * 20",

                "risk < 30 and risk > 0 then = 20 + (risk / 30) * 80",

                "risk >= 30 = 100 risk"

            };
            await File.WriteAllLinesAsync("../../../data/MoldAlgo.txt", moldAlgo);
        }
    }
}
