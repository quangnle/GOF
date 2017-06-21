using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObserverPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            var sensor = new ThermometerSensor();
            sensor.Attach(new WateringDevice());
            sensor.Attach(new AirconDevice());

            sensor.Temperature = 40;
            sensor.Temperature = 33;
            sensor.Temperature = 8;

            Console.Read();
        }
    }

    public abstract class Sensor // Subject
    {
        protected double _temperature = 27.0;
        // list chứa observer
        private List<IConnectingDevice> _listDevices = new List<IConnectingDevice>(); 

        public void Attach(IConnectingDevice device)
        {
            _listDevices.Add(device);
        }

        public void Detach(IConnectingDevice device)
        {
            _listDevices.Add(device);
        }

        public double GetTemperature()
        {
            return _temperature;
        }

        public void Notify() // notify cho toàn bộ các observer
        {
            foreach (var device in _listDevices)
            {
                device.Update(this);
            }
        }
    }

    public class ThermometerSensor : Sensor // Concrete subject
    {   
        public double Temperature
        {
            get { return _temperature; }
            set
            {
                if (_temperature != value)
                {
                    _temperature = value;
                    Notify(); // thông báo thay đổi trạng thái
                }
            }
        }
    }

    public interface IConnectingDevice // Bbserver
    {
        void Update(Sensor sensor);
    }

    public class WateringDevice : IConnectingDevice // Concrete Observer
    {
        public void Update(Sensor sensor)
        {
            var t = sensor.GetTemperature();
            if (t >= 30 && t < 40)
                Console.WriteLine("Do watering");
        }
    }

    public class AirconDevice : IConnectingDevice // Concrete Observer
    {
        public void Update(Sensor sensor)
        {
            var t = sensor.GetTemperature();
            if (t < 10)
                Console.WriteLine("Do warming");
            else if  (t > 40)
                Console.WriteLine("Do cooling");
        }
    }
}
