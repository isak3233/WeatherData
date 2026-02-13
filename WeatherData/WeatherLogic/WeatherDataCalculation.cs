using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WeatherData.Weather
{
    internal class WeatherDataCalculation
    {
        static public decimal AverageTemp(string[] weatherData)
        {
            Regex regex = new Regex(@"(-?\d{1,2}\.\d{1})");

            //Tar ut tempraturen från våran data
            var temps = weatherData
                .Select(d => regex.Match(d))
                .Where(m => m.Success)
                .Select(m => m.Groups[1].Value)
                .ToArray();
            decimal sum = 0;
            foreach (string temp in temps)
            {
                sum += decimal.Parse(temp.Replace('.', ','));
            }
            return temps.Length == 0 ? 0 : Math.Round(sum / temps.Length, 2);
        }
        static public List<(DateTime day, decimal averageTemp)> AverageTempForDays(string[] weatherData)
        {
            string rule = @"^(\d{4}-\d{2}-\d{2}).*?,(-?\d{1,2}\.\d{1}),";
            Regex regex = new Regex(rule);

            var result = weatherData
                .Select(line => regex.Match(line))
                .Where(m => m.Success)
                .Select(m => new
                {
                    Date = DateTime.Parse(m.Groups[1].Value),
                    Temp = decimal.Parse(m.Groups[2].Value.Replace(".", ","))
                })
                .GroupBy(x => x.Date)
                .Select(g => (
                    day: g.Key,
                    averageTemp: g.Average(x => x.Temp)
                ))
                .OrderBy(x => x.day)
                .ToList();

            return result;
        }
        static public decimal AverageHumidity(string[] weatherData)
        {

            Regex regex = new Regex(@"(\d{2}$)");

            var humidityProcents = weatherData
                .Select(d => regex.Match(d))
                .Where(m => m.Success)
                .Select(m => m.Groups[1].Value)
                .ToArray();

            decimal sum = 0;

            foreach (string humidityProcent in humidityProcents)
            {
                sum += int.Parse(humidityProcent);
            }
            return humidityProcents.Length == 0 ? 0 : Math.Round(sum / humidityProcents.Length, 2);
        }
        static public decimal AverageMoldRisk(string[] weatherData)
        {
            Regex regex = new Regex(@"(-?\d{1,2}\.\d{1}).(\d{2}$)");

            var tempNHumiditys = weatherData
                .Select(d => regex.Match(d))
                .Where(m => m.Success)
                .Select(m => (m.Groups[1].Value, m.Groups[2].Value))
                .ToArray();
            decimal sum = 0;

            foreach ((string temp, string humidity) in tempNHumiditys)
            {
                decimal t = decimal.Parse(temp.Replace('.', ','));
                decimal h = decimal.Parse(humidity);

                decimal raw = h - (100 - t);
                decimal moldRisk;

                if (raw <= -50)
                {
                    moldRisk = 0;
                }
                else if (raw <= 0)
                {
                    moldRisk = (raw + 50) / 50 * 20;
                }
                else if (raw >= 30)
                {
                    moldRisk = 100;
                }
                else
                {
                    moldRisk = 20 + (raw / 30) * 80;
                }


                sum += moldRisk;
            }

            return tempNHumiditys.Length == 0 ? 0 : Math.Round(sum / tempNHumiditys.Length, 2);
        }
        static public List<(DateTime startMolding, DateTime endMolding)> RiskForMoldOverDays(List<(DateTime date, decimal mold)> moldAverage)
        {
            List<(DateTime startMolding, DateTime endMolding)> returnList = new List<(DateTime startMolding, DateTime endMolding)>();
            decimal riskThreshold = 20;
            DateTime startOfRiskDate = new DateTime();
            DateTime endOfRiskDate = new DateTime(); ;
            for (int i = 0; i < moldAverage.Count; i++)
            {
                var mold = moldAverage[i].mold;
                var date = moldAverage[i].date;

                if (mold > riskThreshold)
                {
                    if (startOfRiskDate.Year == 1)
                    {
                        startOfRiskDate = date;
                    }

                    endOfRiskDate = date;
                }
                else if (startOfRiskDate.Year != 1)
                {
                    returnList.Add((startOfRiskDate, endOfRiskDate));
                    startOfRiskDate = new DateTime();
                }

                if (i + 1 >= moldAverage.Count && startOfRiskDate.Year != 1)
                {
                    returnList.Add((startOfRiskDate, endOfRiskDate));
                    startOfRiskDate = new DateTime();
                }
            }

            return returnList;
        }

        public static DateTime[] MetrologicalTime(List<(DateTime day, decimal averageTemp)> averageTempForDays, int tempRequired)
        {
            Regex regex = new Regex(@"^(\d{4}-\d{2}-\d{2}).*?,([\d.]+),");

            //Tar ut tempraturen från våran data

            DateTime streakStart = DateTime.MinValue;
            DateTime previousDate = DateTime.MinValue;
            int daysInRow = 0;

            DateTime bestStart = DateTime.MinValue;
            DateTime bestEnd = DateTime.MinValue;
            int maxDays = 0;

            foreach (var (date, temp) in averageTempForDays)
            {
                if (temp < tempRequired)
                {
                    
                    if (daysInRow == 0)
                    {
                        streakStart = date;
                    }
                        

                    if (previousDate != DateTime.MinValue && date == previousDate.AddDays(1))
                    {
                        daysInRow++;
                    }
                    else
                    {
                        daysInRow = 1;
                    }

                    if (daysInRow > maxDays)
                    {
                        maxDays = daysInRow;
                        bestStart = streakStart;
                        bestEnd = date;
                    }

                    if (daysInRow >= 5)
                    {
                        return new DateTime[] { bestStart, bestEnd };
                    }
                }
                else
                {
                    daysInRow = 0;
                }

                previousDate = date;
            }

           return new DateTime[] { bestStart, bestEnd };
        }
    }
}
