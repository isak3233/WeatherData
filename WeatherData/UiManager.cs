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
            List<string> insideList = new List<string>();
            insideList.Add("1. Medeltemperatur för valt datum");
            insideList.Add("2. Sortering av varmast till kallaste dagen enligt medeltemperatur per dag");
            insideList.Add("3. Sortering av torrast till fuktigaste dagen enligt medelluftfuktighet per dag");
            insideList.Add("4. Sortering av minst till störst risk av mögel");


            var window = new Window("Inomhus", 0, 0, insideList);
            window.Draw();
            Console.ReadLine();
        }
        static public void StartOutsideMenu()
        {
            List<string> outsideList = new List<string>();
            outsideList.Add("1. Medeltemperatur för valt datum");
            outsideList.Add("2. Sortering av varmast till kallaste dagen enligt medeltemperatur per dag");
            outsideList.Add("3. Sortering av torrast till fuktigaste dagen enligt medelluftfuktighet per dag");
            outsideList.Add("4. Sortering av minst till störst risk av mögel");


            var window = new Window("Utomhus", 0, 0, outsideList);
            window.Draw();
            Console.ReadLine();
        }
        static public void StartMainMenu()
        {
            List<string> mainMenuList = new List<string>();
            mainMenuList.Add("1. Inomhus");
            mainMenuList.Add("2. Utomhus");
            var window = new Window("", 50, 50, mainMenuList);
            window.Draw();

            int option = InputHelper.GetIntFromUser("Val: ");
            Console.Clear();
            switch(option)
            {
                case 1:
                    StartInsideMenu();
                    break;
                case 2:
                    StartOutsideMenu();
                    break;
                default:
                    break;
            }
            
        }
    }
}
