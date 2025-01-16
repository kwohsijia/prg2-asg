﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//==========================================================
// Student Name	: Ian Tan Jun Yang (S10268190F)
// Student Name	: Kwoh Si Jia (S10267641F)
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
            string code = flight.FlightNumber.Substring(0, 2);
            Airline? airline = null;
            
            foreach (KeyValuePair<string, Airline> a in Airline)
            {
                if (a.Value.Code == code)
                {
                    airline = a.Value;
                }
            }
            return airline;
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
