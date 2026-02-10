using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WeatherData
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

                if (raw <= -50m)
                    moldRisk = 0m;
                else if (raw <= 0m)
                    moldRisk = (raw + 50m) / 50m * 20m;
                else if (raw >= 30m)
                    moldRisk = 100m;
                else
                    moldRisk = 20m + (raw / 30m) * 80m;

                sum += moldRisk;
            }

            return tempNHumiditys.Length == 0
                ? 0
                : Math.Round(sum / tempNHumiditys.Length, 2);
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
                    if(startOfRiskDate.Year == 1)
                    {
                        startOfRiskDate = date;
                    } 
                    
                    endOfRiskDate = date;
                } else if(startOfRiskDate.Year != 1)
                {
                    returnList.Add((startOfRiskDate, endOfRiskDate));
                    startOfRiskDate = new DateTime();
                }
            }

            return returnList;
        }
    }
}
