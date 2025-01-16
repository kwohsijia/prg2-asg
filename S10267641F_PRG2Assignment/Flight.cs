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
    abstract class Flight
    {
        public string FlightNumber {  get; set; }   
        public string Origin { get; set; }
        public string Destination { get; set; }
        public DateTime ExpectedTime { get; set; }
        public string Status {  get; set; }

        public Flight(string flightNumber, string origin, string destination, DateTime expectedTime, string status)
        {
            FlightNumber = flightNumber;
            Origin = origin;
            Destination = destination;
            ExpectedTime = expectedTime;
            Status = status;
        }
        public abstract double CalculateFees();
        
        public override string ToString()
        {
            return $"{FlightNumber, -8} {Origin, -20} {Destination, -20} {ExpectedTime, -24} {Status, -12}"; 
        }

        //we did the flight classes together in school on one laptop
    }
}
