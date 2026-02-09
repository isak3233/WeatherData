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
            string rule = $@"^\d{{4}}-{date:MM}-(\d{{2}})";
            Regex regex = new Regex(rule);

            string[] weatherData = File.ReadAllLines("../../../data/WeatherData.txt");

            var result = weatherData
                .Select(d => regex.Match(d))
                .Where(m => m.Success)
                .Select(m => int.Parse(m.Groups[1].Value)) 
                .Distinct()
                .ToArray();


            return result;
        }
    }
}
