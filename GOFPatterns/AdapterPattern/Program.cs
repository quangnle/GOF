using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdapterPattern
{
    class Client
    {
        static void Main(string[] args)
        {
            TwoPinPlug plug = new TwoPinPlug();
            ThreePinPlug adapter = new Adapter(plug);
            adapter.Plug();

            Console.ReadLine();
        }
    }

    interface ThreePinPlug // Target
    {
        void Plug(); // request
    }

    class Adapter : ThreePinPlug // adapter
    {
        private TwoPinPlug _plug;

        public Adapter(TwoPinPlug plug)
        {
            _plug = plug;
        }

        public void Plug() // implement phương thức request
        {
            Console.WriteLine("Plugging to socket");
            _plug.PlugToSocket();
        }
    }

    class TwoPinPlug // adaptee
    {
        public void PlugToSocket() // specific request
        {
            Console.WriteLine("Two pin to the socket");
        }
    }
}
