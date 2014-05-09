using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IEEEWeather;

namespace WeatherListenerTestGUI
{
    public partial class Form1 : Form
    {
        WeatherListener wl;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            wl = new WeatherListener();

            // Event handler for weather updates
            wl.WeatherTick += wl_WeatherTick;

            // Start recieving weather updates
            wl.Start();

            tbConsole.Text = "WeatherListener started. Waiting for updates...\n";
        }

        void wl_WeatherTick(object sender, WeatherTickEventArgs e)
        {
            // extract the WeatherUpdate object
            WeatherUpdate wu = e.Update;

            StringBuilder sb = new StringBuilder();

            // Show time
            sb.AppendFormat("Time: {0}\r\n\r\n", wu.Time.ToLocalTime());

            // Show temperature
            float tempC = wu.Temperature;
            float tempF = wu.Temperature.ToF();

            sb.AppendFormat("Temperature: {0} °C ({1} °F)\r\n", tempC, tempF);
            sb.AppendLine(WeatherValueProperties(wu.Temperature));

            // Show humidity
            float humid = wu.Humidity;
            sb.AppendFormat("\r\nRelative Humidity: {0}%\r\n", humid);
            sb.AppendLine(WeatherValueProperties(wu.Humidity));

            // Show dew point
            float dpC = wu.DewPoint;
            float dpF = wu.DewPoint.ToF();
            sb.AppendFormat("\r\nDew Point: {0} °C ({1} °F)\r\n", dpC, dpF);
            sb.AppendLine(WeatherValueProperties(wu.DewPoint));

            // Show pressure
            float pressure = wu.Pressure;
            sb.AppendFormat("\r\nAtmospheric Pressure: {0} hPa\r\n", pressure);
            sb.AppendLine(WeatherValueProperties(wu.Pressure));

            // Show irradiation
            float irrad = wu.Irradiation;
            sb.AppendFormat("\r\nSolar Radiation: {0} Watts per square meter\r\n", irrad);
            sb.AppendLine(WeatherValueProperties(wu.Irradiation));

            // Show rain
            float rain = wu.Rain;
            sb.AppendFormat("\r\nHourly rainfall: {0} inches\r\n", rain);
            sb.AppendLine(WeatherValueProperties(wu.Rain));

            // Show wind speeds
            sb.AppendFormat("\r\nWind Speeds (MPH):\r\n\tInstant {0}\r\n\t2 Min Avg: {1}\r\n\t10 Min Gust: {2}\r\n",
                (float)wu.WindSpeedInstant.ToMPH(),
                (float)wu.WindSpeedAvg2Min.ToMPH(),
                (float)wu.WindSpeedGust10Min.ToMPH());
            sb.AppendLine(WeatherValueProperties(wu.WindSpeedInstant));

            // Show wind directions
            sb.AppendFormat("\r\nWind Directions:\r\n\tInstant {0}°\r\n\t2 Min Avg: {1}°\r\n\t10 Min Gust: {2}°\r\n",
                (float)wu.WindDirectionInstant,
                (float)wu.WindDirectionAvg2Min,
                (float)wu.WindDirectionGust10Min);
            sb.AppendLine(WeatherValueProperties(wu.WindDirectionInstant));

            // Angle math test, view with debugger
            AngleValue windangle = wu.WindDirectionInstant - 180F;  // point the other way
            double rads = (double)(windangle.ToRad());
            double cos = Math.Cos((double)rads);
            double sin = Math.Sin((double)rads);

            // Update textbox
            tbConsole.Text = sb.ToString();
        }

        /// <summary>
        /// Prints the various properties of a weathervalue object
        /// </summary>
        /// <param name="wv">The weathervalue object to look at</param>
        /// <returns></returns>
        private string WeatherValueProperties(WeatherValue wv)
        {
            return string.Format("Type: '{0}', Default unit: '{1}', Minimum value: '{2}', Maximum value '{3}'",
                wv.GetType().Name, wv.Unit, wv.Min, wv.Max);
        }
    }
}
