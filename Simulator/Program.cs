using System;
using System.Timers;

namespace Simulator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Timer timer = new Timer();
            timer.Elapsed += new ElapsedEventHandler(CreateFlight);
            timer.Interval = 1000;
            timer.Enabled = true;

            Timer timer1 = new Timer();
            timer1.Elapsed += new ElapsedEventHandler(StartProcessAirport);
            timer1.Interval = 1000;
            timer1.Enabled = true;
            Console.ReadLine();
        }

        private static void StartProcessAirport(object sender, ElapsedEventArgs e)
        {
            FlightsClient client = new FlightsClient();
            bool res = client.StartProcessAirport().Result;
            Console.WriteLine("start process airport");
        }

        private static void CreateFlight(object source, ElapsedEventArgs e)
        {
            FlightsClient client = new FlightsClient();
            bool res = client.CreateFlight().Result;
            Console.WriteLine("flight created");
        }
    }
}
