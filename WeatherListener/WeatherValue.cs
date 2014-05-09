using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IEEEWeather
{
    public abstract class WeatherValue
    {
        protected float val;
        
        /// <summary>
        /// The maximum value in the default unit
        /// </summary>
        /// 
        public abstract float Max { get; }
        /// <summary>
        /// The minimum value in the default unit
        /// </summary>
        public abstract float Min { get; }

        /// <summary>
        /// The default unit as a string
        /// </summary>
        public abstract string Unit { get; }

        public WeatherValue(float value) { this.Value = value; }

        public virtual float Value
        {
            get
            {
                return this.val;
            }
            set
            {
                this.val = Math.Min(value, this.Max);
                this.val = Math.Max(this.val, this.Min);
            }
        }

        public static implicit operator float(WeatherValue w)
        {
            return w.Value;
        }
    }

    /// <summary>
    /// Represents a temperature in degrees Celcius
    /// </summary>
    public class TemperatureValue : WeatherValue
    {
        public TemperatureValue(float val = 0) : base(val) { }
        public override string Unit { get { return "C"; } }
        public override float  Min { get { return -50.0F; } }
        public override float Max { get { return 100.0F; } }

        /// <summary>
        /// Converts this Temperature to Farenheit
        /// </summary>
        /// <returns></returns>
        public float ToF()
        {
            return this.Value * 9 / 5 + 32;
        }

        /// <summary>
        /// Creates a new TemperatureValue from a value in Farenheit.
        /// </summary>
        /// <param name="f">Temperature in degrees Farenheit</param>
        /// <returns>A TemperatureValue representing the temperature</returns>
        public static TemperatureValue FromF(float f)
        {
            return new TemperatureValue((f - 32) * 5 / 9);
        }

        public static implicit operator TemperatureValue(float v) { return new TemperatureValue(v); }
    }

    public class WindSpeedValue : WeatherValue
    {
        public WindSpeedValue(float val = 0) : base(val) { }
        public override string Unit { get { return "m/s"; } }
        public override float Min { get { return 0.0F; } }
        public override float Max { get { return 50.0F; } }
        public static implicit operator WindSpeedValue(float v) { return new WindSpeedValue(v); }

        /// <summary>
        /// Converts this speed to Miles Per Hour
        /// </summary>
        /// <returns></returns>
        public float ToMPH()
        {
            return this.Value * 2.23694F;
        }
    }

    public class AngleValue : WeatherValue
    {
        public AngleValue(float val = 0) : base(val) { }
        public override string Unit { get { return "degrees"; } }
        public override float Min { get { return 0.0F; } }
        public override float Max { get { return 360.0F; } }

        /// <summary>
        /// The angle in degrees. Wraps around when assigned a value greater than 360 degrees
        /// </summary>
        public override float Value
        {
            get
            {
                return this.val;
            }
            set
            {
                val = value % 360.0F;
                if (val < 0.0F) val += 360.0F;
            }
        }

        /// <summary>
        /// Converts this Angle to Radians
        /// </summary>
        /// <returns></returns>
        public float ToRad()
        {
            return this.Value / 180.0F * (float)Math.PI;
        }

        /// <summary>
        /// Creates a new AngleValue from an angle in radians
        /// </summary>
        /// <param name="r">Angle in radians</param>
        /// <returns>An AngleValue representing the angle</returns>
        public static AngleValue FromF(float r)
        {
            return new AngleValue(r * 180.0F / (float)Math.PI);
        }

        public static implicit operator AngleValue(float v) { return new AngleValue(v); }
    }

    public class PercentageValue : WeatherValue
    {
        public PercentageValue(float val = 0) : base(val) { }
        public override string Unit { get { return "percent"; } }
        public override float Min { get { return 0.0F; } }
        public override float Max { get { return 100.0F; } }
        public static implicit operator PercentageValue(float v) { return new PercentageValue(v); }
    }

    public class IrradianceValue : WeatherValue
    {
        public IrradianceValue(float val = 0) : base(val) { }
        public override string Unit { get { return "W/m^2"; } }
        public override float Min { get { return 0.0F; } }
        public override float Max { get { return 1250.0F; } }
        public static implicit operator IrradianceValue(float v) { return new IrradianceValue(v); }
    }

    public class PressureValue : WeatherValue
    {
        public PressureValue(float val = 0) : base(val) { }
        public override string Unit { get { return "hPa"; } }
        public override float Min { get { return 500.0F; } }
        public override float Max { get { return 1100.0F; } }
        public static implicit operator PressureValue(float v) { return new PressureValue(v); }
    }

    public class RainValue : WeatherValue
    {
        public RainValue(float val = 0) : base(val) { }
        public override string Unit { get { return "in/Hr"; } }
        public override float Min { get { return 0.0F; } }
        public override float Max { get { return 15.0F; } }
        public static implicit operator RainValue(float v) { return new RainValue(v); }
    }

}
