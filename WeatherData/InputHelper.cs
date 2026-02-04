using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
