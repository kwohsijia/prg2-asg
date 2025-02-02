using System;
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
        public Dictionary<string, Airline> Airlines { get; set; }
        public Dictionary<string, Flight> Flights { get; set; }
        public Dictionary<string, BoardingGate> BoardingGates { get; set; }
        public Dictionary<string, double> GateFees { get; set; }

        public Terminal(string terminalName)
        {
            TerminalName = terminalName;
            Airlines = new Dictionary<string, Airline>();
            Flights = new Dictionary<string, Flight>();
            BoardingGates = new Dictionary<string, BoardingGate>();
            GateFees = new Dictionary<string, double>();
        }
        public bool AddAirline(Airline airline)
        {
            if (Airlines.ContainsKey(airline.Code))
            {
                return false;
            }
            else
            {
                Airlines.Add(airline.Code, airline);
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
            
            foreach (KeyValuePair<string, Airline> a in Airlines)
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
            Console.WriteLine("=============================================\nTotal Airline Fees for the day\n=============================================");
            Console.WriteLine($"{"Airline Code",-15}{"Airline Name",-20}{"Subtotal Fees",-15}{"Subtotal Discounts",-20}{"Total Fees",-15}");
            double totalFees = 0.0;
            double totalDiscounts = 0.0;
            foreach (Airline airline in Airlines.Values)
            {
                double subtotalFees = airline.CalculateFees();
                double subtotalDiscounts = 0.0;

                foreach (Flight flight in airline.Flights.Values)
                {
                    //Ad boarding gate base fees
                    foreach (BoardingGate boardingGate in BoardingGates.Values)
                    {
                        if (boardingGate.Flight == flight)
                        {
                            subtotalFees += boardingGate.CalculateFees();
                        }
                    }

                    //Check if flight is arriving/departing before 11am or after 9pm
                    if (flight.ExpectedTime.Hour < 11 || (flight.ExpectedTime.Hour == 21 && flight.ExpectedTime.Minute > 0) ||flight.ExpectedTime.Hour > 21)
                    {
                        subtotalDiscounts += 110;
                    }
                    //Check if flight origin is Dubai, Bangkok or Tokyo
                    if (new List<string> { "Dubai (DXB)", "Bangkok (BKK)", "Tokyo (NRT)" }.Contains(flight.Origin))
                    {
                        subtotalDiscounts += 25;
                    }
                    //Check if flight has special code
                    if (flight is NORMFlight)
                    {
                        subtotalDiscounts += 50;
                    }
                }
                //Caclulate discount if there are more than 5 flights for the airline
                if (airline.Flights.Values.Count > 5)
                {
                   subtotalDiscounts += subtotalFees * 0.03;
                }
                subtotalDiscounts += Math.Floor(airline.Flights.Values.Count / 3.0) * 350.0;
                Console.WriteLine($"{airline.Code,-15}{airline.Name,-20}{subtotalFees,-15:C2}{subtotalDiscounts,-20:C2}{subtotalFees - subtotalDiscounts,-15:C2}");
                totalFees += subtotalFees;
                totalDiscounts += subtotalDiscounts;
            }
            Console.WriteLine();
            Console.WriteLine($"Subtotal of all Airline fees: {totalFees:C2}");
            Console.WriteLine($"Subtotal of all Airline discounts: {totalDiscounts:C2}");
            Console.WriteLine($"Grand total of Airline fees: {totalFees - totalDiscounts:C2}");
            Console.WriteLine($"Percentage of the subtotal discounts over final fees: {(totalDiscounts / (totalFees - totalDiscounts)) * 100.0:F2}%");
            Console.WriteLine();
        }

        public override string ToString()
        {
            return "Terminal: " + TerminalName + "Airline: " + Airlines + "Flights: " + Flights + "Boarding Gates: " + BoardingGates + "Gate Fees: " + GateFees;
        }
    }
}
