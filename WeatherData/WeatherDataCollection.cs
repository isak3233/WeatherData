using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WeatherData
{
    internal class WeatherDataCollection
    {
        static public string[] GetWeatherDataForDay(DateTime date, bool inhouse)
        {
            string datePart = date.ToString("yyyy-MM-dd");
            string place = inhouse ? "Inne" : "Ute";

            string rule = $@"{datePart}.*,{place},";
            Regex regex = new Regex(rule);

            string[] weatherData = File.ReadAllLines("../../../data/WeatherData.txt");
            string[] result = weatherData.Where(d => regex.IsMatch(d)).ToArray();

            return result;

        }
        static public string[] GetWeatherDataForMonth(DateTime date, bool inhouse)
        {
            string datePart = date.ToString("yyyy-MM");
            string place = inhouse ? "Inne" : "Ute";
            string rule = $@"{datePart}.*,{place},";
            Regex regex = new Regex(rule);

            string[] weatherData = File.ReadAllLines("../../../data/WeatherData.txt");
            string[] result = weatherData.Where(d => regex.IsMatch(d)).ToArray();

            return result;
        }
        static public int[] GetExistingDaysInMonth(DateTime date)
        {
            string month = date.ToString("MM");
            string rule = $@".*-{month}-.*";
            Regex regex = new Regex(rule);

            string[] weatherData = File.ReadAllLines("../../../data/WeatherData.txt");
            //Plockar ut unika dagar i månaden
            var result = weatherData
                .Where(d => regex.IsMatch(d))
                .Select(d => int.Parse(d.Substring(8, 2)))
                .Distinct()
                .ToArray();
   
            
            return result;
        }
    }
}
