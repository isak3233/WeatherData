using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherData.Weather;
using webbshop;

namespace WeatherData.Ui
{
    internal class InsideMenu : IMenu
    {
        public List<string> OptionList { get; set; }
        public InsideMenu()
        {
            OptionList = new List<string>()
                {
                    "1. Medeltemperatur för valt datum",
                    "2. Sortering av varmast till kallaste dagen enligt medeltemperatur per dag",
                    "3. Sortering av torrast till fuktigaste dagen enligt medelluftfuktighet per dag",
                    "4. Sortering av minst till störst risk av mögel",
                    "5. Gå tillbaka till huvudmenyn"
                };
        }

        public async Task<Menus> StartMenyHandler()
        {
            Console.WriteLine("Yo11");
            var window = new Window("Inomhus", 0, 0, OptionList);
            window.Draw();


            int option = InputHelper.GetIntFromUser("Val: ");
            switch (option)
            {
                case 1:
                    var selectedDateForAverage = await InputHelper.GetDateFromUser(6, 12);
                    WeatherService.ShowAverageTempForDay(selectedDateForAverage, true);
                    break;
                case 2:
                    int selectedMonthForTemps = InputHelper.GetMonthFromUser(6, 12);
                    WeatherService.ShowHotColdRanking(selectedMonthForTemps, true);
                    break;
                case 3:
                    int selectedMonthForHumidity = InputHelper.GetMonthFromUser(6, 12);
                    WeatherService.ShowHumidityRanking(selectedMonthForHumidity, true);
                    break;
                case 4:
                    int selectedMonthForMold = InputHelper.GetMonthFromUser(6, 12);
                    WeatherService.ShowMoldRisk(selectedMonthForMold, true);
                    break;
                case 5:
                    return Menus.StartMenu;
                default:
                    break;
            }
            Console.ReadLine();
            return Menus.InsideMenu;
        }
    }
}
