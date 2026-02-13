using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webbshop;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WeatherData.Ui
{
    public class StartMenu : IMenu
    {
        public List<string> OptionList { get; set; }
        public StartMenu()
        {
            OptionList = new List<string>()
                {
                    "1. Inomhus",
                    "2. Utomhus"
                };
        }

        public async Task<Menus> StartMenyHandler()
        {
            var window = new Window("", 50, 50, OptionList);
            window.Draw();

            int option = InputHelper.GetIntFromUser("Val: ");

            switch (option)
            {
                case 1:
                    return Menus.InsideMenu;
                case 2:
                    return Menus.OutsideMenu;
                case 3:
                    Console.WriteLine(InputHelper.GetDateFromUser(6, 12));
                    break;
                default:
                    break;
            }
            return Menus.StartMenu;
        }
    }
}
