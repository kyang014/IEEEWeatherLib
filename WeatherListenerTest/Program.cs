using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IEEEWeather;

namespace WeatherListenerTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Creating WeatherListener");
            var wl = new WeatherListener();


            wl.WeatherTick += wl_WeatherTick;
            wl.Start();

            Console.ReadLine();
        }

        static void wl_WeatherTick(object sender, WeatherTickEventArgs e)
        {
            var wu = e.Update;
            Console.WriteLine("Temperature: " + wu.Temperature + " C (" + wu.Temperature.ToF() + " F");
        }
    }
}
