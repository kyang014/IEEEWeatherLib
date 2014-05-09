using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;

namespace IEEEWeather
{
    /// <summary>
    /// Listens for weather events
    /// </summary>
    public class WeatherListener
    {
        public event WeatherTickEventHandler WeatherTick;
        Timer wTick = new Timer();

        public WeatherListener()
        {
            wTick.Interval = 2000;
            wTick.Tick += wTick_Tick;
        }

        protected void OnWeatherTick(WeatherTickEventArgs e)
        {
            WeatherTick(this, e);
        }  

        void wTick_Tick(object sender, EventArgs e)
        {
            var rand = new Random();
            var wu = new WeatherUpdate();

            // Fow now, return some random values
            wu.Time = DateTime.Now;
            wu.Temperature = 25F + (float)rand.NextDouble();
            wu.Humidity = 50F + 10F*(float)rand.NextDouble();
            wu.Rain = (float)rand.NextDouble();
            wu.Irradiation = 900F + 100F*(float)rand.NextDouble();
            wu.Pressure = 1000F + 50F*(float)rand.NextDouble();
            wu.WindSpeedInstant = 5F * (float)rand.NextDouble();
            wu.WindSpeedAvg2Min = 2F + (float)rand.NextDouble();
            wu.WindSpeedGust10Min = 5F + (float)rand.NextDouble();
            wu.WindDirectionInstant = 45F + 5F * (float)rand.NextDouble();
            wu.WindDirectionAvg2Min = 45F + 1F * (float)rand.NextDouble();
            wu.WindDirectionGust10Min = 45F + 1F * (float)rand.NextDouble();

            var wtea = new WeatherTickEventArgs(wu);
            OnWeatherTick(wtea);
        }

        public void Start()
        {
            wTick.Start();
        }

        public void Stop()
        {
            wTick.Stop();
        }


    }

    public class WeatherTickEventArgs : EventArgs
    {
        private WeatherUpdate wu;
        public WeatherTickEventArgs(WeatherUpdate u)
        {
            this.wu = u;
        }

        public WeatherUpdate Update
        {
            get { return this.wu; }
        }
    }

    public delegate void WeatherTickEventHandler(object sender, WeatherTickEventArgs e);


}
