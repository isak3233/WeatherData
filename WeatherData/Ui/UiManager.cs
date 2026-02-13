using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherData.Weather;
using webbshop;

namespace WeatherData.Ui
{
    internal class UiManager
    {
        IMenu StartMenu {  get; set; }
        IMenu OutsideMenu {  get; set; }
        IMenu InsideMenu {  get; set; }

        public UiManager()
        {
            StartMenu = new StartMenu();
            OutsideMenu = new OutsideMenu();
            InsideMenu = new InsideMenu();
        }
        public async Task StartMenuProgram()
        {
            IMenu currentMenu = StartMenu;
            while (true)
            {
                Menus newMenu = await currentMenu.StartMenyHandler();
                switch (newMenu)
                {
                    case Menus.StartMenu:
                        currentMenu = StartMenu;
                        break;
                    case Menus.OutsideMenu:
                        currentMenu = OutsideMenu;
                        break;
                    case Menus.InsideMenu:
                        Console.WriteLine("yo1");
                        currentMenu = InsideMenu;
                        break;
                    default:
                        break;

                }
                Console.Clear();
            }
        }
    }
}
