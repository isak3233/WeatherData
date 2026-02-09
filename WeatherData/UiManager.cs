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
        static public void ViewInsideMenu()
        {
            List<string> insideList = new List<string>();
            insideList.Add("1. Medeltemperatur för valt datum");
            insideList.Add("2. Sortering av varmast till kallaste dagen enligt medeltemperatur per dag");
            insideList.Add("3. Sortering av torrast till fuktigaste dagen enligt medelluftfuktighet per dag");
            insideList.Add("4. Sortering av minst till störst risk av mögel");
            insideList.Add("5. Gå tillbaka till huvudmenyn");

            var window = new Window("Inomhus", 0, 0, insideList);
            window.Draw();


            int option = InputHelper.GetIntFromUser("Val: ");
            Console.Clear();
            switch (option)
            {
                
                case 5:
                    ViewMainMenu();
                    break;
                default:
                    break;
            }
        }
        static public void ViewOutsideMenu()
        {
            List<string> outsideList = new List<string>();
            outsideList.Add("1. Medeltemperatur för valt datum");
            outsideList.Add("2. Sortering av varmast till kallaste dagen enligt medeltemperatur per dag");
            outsideList.Add("3. Sortering av torrast till fuktigaste dagen enligt medelluftfuktighet per dag");
            outsideList.Add("4. Sortering av minst till störst risk av mögel");
            outsideList.Add("5. Gå tillbaka till huvudmenyn");


            var window = new Window("Utomhus", 0, 0, outsideList);
            window.Draw();
            int option = InputHelper.GetIntFromUser("Val: ");
            Console.Clear();
            switch (option)
            {
                case 5:
                    ViewMainMenu();
                    break;
                default:
                    break;
            }

        }
        static public void ViewMainMenu()
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
                    ViewInsideMenu();
                    break;
                case 2:
                    ViewOutsideMenu();
                    break;
                //case 3:
                //    Console.WriteLine(InputHelper.GetDateFromUser(6, 12));
                //    break;
                default:
                    break;
            }
            
        }
    }
}
