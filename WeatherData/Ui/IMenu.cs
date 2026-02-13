using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherData.Ui
{
    public enum Menus
    {
        StartMenu,
        OutsideMenu,
        InsideMenu
    }
    public interface IMenu
    {
        List<string> OptionList { get; set; }

        Task<Menus> StartMenyHandler();
    }
}
