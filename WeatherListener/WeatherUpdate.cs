using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IEEEWeather
{
    public class WeatherUpdate
    {
        public WeatherUpdate()
        {
            this.Time = DateTime.MinValue;
            this.Temperature = null;
            this.Humidity = null;
            this.Pressure = null;
            this.Irradiation = null;
            this.Rain = null;
            this.WindDirectionAvg2Min = null;
            this.WindDirectionGust10Min = null;
            this.WindDirectionInstant = null;
            this.WindSpeedAvg2Min = null;
            this.WindSpeedGust10Min = null;
            this.WindSpeedInstant = null;
        }

        /// <summary>
        /// The time at which the measurements were taken
        /// </summary>
        public DateTime Time;

        /// <summary>
        /// The dry-bulb temperature in degrees Celcius
        /// </summary>
        public TemperatureValue Temperature;

        public PercentageValue Humidity;

        /// <summary>
        /// Calculated dew point in degrees Celcius
        /// http://www.calcunation.com/calculators/nature/dew-point.php
        /// </summary>
        public TemperatureValue DewPoint
        {
            get {
                if (Temperature == null || Humidity == null)
                    throw new NullReferenceException("Humidity and Temperature must not be null when calculating Dew Point.");

                double RHFracLn = Math.Log(this.Humidity / 100.0D);
                double RightFrac = (17.62D * Temperature) / (243.12 + Temperature);
                double WholeFrac = RHFracLn + RightFrac;
                double Tdp = (243.12 * WholeFrac) / (17.62 - WholeFrac);
                return (float)Tdp;
            }
        }

        public PressureValue Pressure;
        public IrradianceValue Irradiation;
        public RainValue Rain;

        public AngleValue WindDirectionInstant;
        public WindSpeedValue WindSpeedInstant;

        public AngleValue WindDirectionAvg2Min;
        public WindSpeedValue WindSpeedAvg2Min;
        public AngleValue WindDirectionGust10Min;
        public WindSpeedValue WindSpeedGust10Min;
    }
}
