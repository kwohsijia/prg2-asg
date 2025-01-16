using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//==========================================================
// Student Number	: S10267641F
// Student Name	: Kwoh Si Jia
// Partner Name	: Ian Tan Jun Yang 
//==========================================================

namespace S10267641F_PRG2Assignment
{
    class Airline
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public Dictionary<string, Flight> Flights { get; set; } = new Dictionary<string, Flight>();
        public Airline(string name, string code)
        {
            Name = name;
            Code = code;
        }

        public bool AddFlight(Flight flight)
        {
            if (Flights.ContainsKey(flight.FlightNumber))
            {
                return false;
            }
            else
            {
                Flights.Add(flight.FlightNumber, flight);
                return true;
            }
        }
        public double CalculateFees()
        {
            double totalFees = 0.0;
            foreach (Flight flight in Flights.Values)
            {
                totalFees += flight.CalculateFees();
            }

            return totalFees;
        }

        public bool RemoveFlight(Flight flight)
        {
            if (Flights.ContainsKey(flight.FlightNumber))
            {
                Flights.Remove(flight.FlightNumber);
                return true;
            }
            else
            {
                return false;
            }
        }
        public override string ToString()
        {
            return $"Airline: {Name, -20} (Code: {Code, -8}), Total Flights: {Flights.Count}";
        }
    }
}
