using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
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
            return Math.Round(sum / temps.Length, 2);
        }
    }
}
