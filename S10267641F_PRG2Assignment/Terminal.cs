using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10267641F_PRG2Assignment
{
    class Terminal
    {
        public string TerminalName { get; set; }
        public Dictionary<string, Airline> Airline { get; set; }
        public Dictionary<string, Flight> Flights { get; set; }
        public Dictionary<string, BoardingGate> BoardingGates { get; set; }
        public Dictionary<string, double> GateFees { get; set; }

        public Terminal(string terminalName)
        {
            TerminalName = terminalName;
            Airline = new Dictionary<string, Airline>();
            Flights = new Dictionary<string, Flight>();
            BoardingGates = new Dictionary<string, BoardingGate>();
            GateFees = new Dictionary<string, double>();
        }
        public bool AddAirline(Airline airline)
        {

        }

        public bool AddBoardingGate(BoardingGate boardingGate)
        {

        }

        public Airline GetAirlineFromFlight(Flight flight)
        {

        }

        public void PrintAirlineFees()
        {

        }

        public override string ToString()
        {

        }
    }
}
