using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//==========================================================
// Student Number	: S10268190F
// Student Name	: Ian Tan Jun Yang
// Partner Name	: Kwoh Si Jia 
//==========================================================

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
            if (Airline.ContainsKey(airline.Code))
            {
                return false;
            }
            else
            {
                Airline.Add(airline.Code, airline);
                return true;
            }
        }

        public bool AddBoardingGate(BoardingGate boardingGate)
        {
            if (BoardingGates.ContainsKey(boardingGate.GateName))
            {
                return false;
            }
            else
            {
                BoardingGates.Add(boardingGate.GateName, boardingGate);
                return true;
            }
        }

        public Airline GetAirlineFromFlight(Flight flight)
        {
            foreach (var Airline in Airline.Values)
            {
                if (Airline.Flights.ContainsKey(flight.FlightNumber))
                {
                    return Airline;
                }
            }
            return null;
        }

        public void PrintAirlineFees()
        {

        }

        public override string ToString()
        {
            return "Terminal: " + TerminalName + "Airline: " + Airline + "Flights: " + Flights + "Boarding Gates: " + BoardingGates + "Gate Fees: " + GateFees;
        }
    }
}
