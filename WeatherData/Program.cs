using System;
using WeatherData.Ui;
using WeatherData.Weather;

namespace WeatherData
{
    class Program
    {
        static async Task Main(string[] args)
        {
            WeatherService.WriteDataToFiles(); // kör detta i bakgrunden 
            UiManager uiManager = new UiManager();
            await uiManager.StartMenuProgram();


        }
    }
}