using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webbshop;

namespace WeatherData
{
    internal class UiManager
    {
        static public void StartInsideMenu()
        {
            Console.Clear();
            List<string> insideList = new List<string>();
            insideList.Add("1. Medeltemperatur för valt datum");
            insideList.Add("2. Sortering av varmast till kallaste dagen enligt medeltemperatur per dag");
            insideList.Add("3. Sortering av torrast till fuktigaste dagen enligt medelluftfuktighet per dag");
            insideList.Add("4. Sortering av minst till störst risk av mögel");
            insideList.Add("5. Gå tillbaka till huvudmenyn");

            var window = new Window("Inomhus", 0, 0, insideList);
            window.Draw();


            int option = InputHelper.GetIntFromUser("Val: ");
            switch (option)
            {
                case 1:
                    var selectedDateForAverage = InputHelper.GetDateFromUser(6, 12);
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
                    int selectedMonthForMold = InputHelper.GetMonthFromUser(6,12);
                    WeatherService.ShowMoldRisk(selectedMonthForMold, false);
                    break;

                case 5:
                    StartMainMenu();
                    break;
                default:
                    break;
            }
        }
        static public void StartOutsideMenu()
        {
            Console.Clear();
            List<string> outsideList = new List<string>();
            outsideList.Add("1. Medeltemperatur och luftfuktighet per dag, för valt datum");
            outsideList.Add("2. Sortering av varmast till kallaste dagen enligt medeltemperatur per dag");
            outsideList.Add("3. Sortering av torrast till fuktigaste dagen enligt medelluftfuktighet per dag");
            outsideList.Add("4. Sortering av minst till störst risk av mögel");
            outsideList.Add("5. Datum för meteorologisk Höst");
            outsideList.Add("6. Datum för meteologisk Vinter");

            outsideList.Add("7. Gå tillbaka till huvudmenyn");


            var window = new Window("Utomhus", 0, 0, outsideList);
            window.Draw();
            int option = InputHelper.GetIntFromUser("Val: ");
            switch (option)
            {
                case 1:
                    var selectedDateForAverage = InputHelper.GetDateFromUser(6, 12);
                    WeatherService.ShowAverageTempForDay(selectedDateForAverage, false);
                    break;
                case 7:
                    StartMainMenu();
                    break;
                default:
                    break;
            }

        }
        static public void StartMainMenu()
        {
            Console.Clear();
            List<string> mainMenuList = new List<string>();
            mainMenuList.Add("1. Inomhus");
            mainMenuList.Add("2. Utomhus");
            var window = new Window("", 50, 50, mainMenuList);
            window.Draw();

            int option = InputHelper.GetIntFromUser("Val: ");

            switch(option)
            {
                case 1:
                    StartInsideMenu();
                    break;
                case 2:
                    StartOutsideMenu();
                    break;
                case 3:
                    Console.WriteLine(InputHelper.GetDateFromUser(6, 12));
                    break;
                default:
                    break;
            }
            
        }

    }
}
