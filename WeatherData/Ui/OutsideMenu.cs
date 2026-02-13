using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherData.Weather;
using webbshop;

namespace WeatherData.Ui
{
    internal class OutsideMenu : IMenu
    {
        public List<string> OptionList { get; set; }
        public OutsideMenu()
        {
            OptionList = new List<string>()
                {
                    "1. Medeltemperatur och luftfuktighet per dag, för valt datum",
                    "2. Sortering av varmast till kallaste dagen enligt medeltemperatur per dag",
                    "3. Sortering av torrast till fuktigaste dagen enligt medelluftfuktighet per dag",
                    "4. Sortering av minst till störst risk av mögel",
                    "5. Datum för meteorologisk Höst",
                    "6. Datum för meteologisk Vinter",
                    "7. Gå tillbaka till huvudmenyn"
                };
        }

        public async Task<Menus> StartMenyHandler()
        {
            var window = new Window("Utomhus", 0, 0, OptionList);
            window.Draw();

            int option = InputHelper.GetIntFromUser("Val: ");
            switch (option)
            {
                case 1:
                    var selectedDateForAverage = await InputHelper.GetDateFromUser(6, 12);
                    WeatherService.ShowAverageTempAndHumidForDay(selectedDateForAverage, false);
                    break;
                case 2:
                    int selectedMonthForTemps = InputHelper.GetMonthFromUser(6, 12);
                    WeatherService.ShowHotColdRanking(selectedMonthForTemps, false);
                    break;
                case 3:
                    int selectedMonthForHumidity = InputHelper.GetMonthFromUser(6, 12);
                    WeatherService.ShowHumidityRanking(selectedMonthForHumidity, false);
                    break;
                case 4:
                    int selectedMonthForMold = InputHelper.GetMonthFromUser(6, 12);
                    WeatherService.ShowMoldRisk(selectedMonthForMold, false);
                    break;
                case 5:
                    WeatherService.ShowMetrologicalAutumn();
                    break;

                case 6:
                    WeatherService.ShowMetrologicalWinter();
                    break;
                case 7:
                    return Menus.StartMenu;
                default:
                    break;
            }
            Console.ReadLine();
            return Menus.OutsideMenu;
        }
    }
}
