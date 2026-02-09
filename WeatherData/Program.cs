using System;

namespace WeatherData
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                UiManager.StartMainMenu();
                Console.ReadLine();
            }
            
        }
    }
}