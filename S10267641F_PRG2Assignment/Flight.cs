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
    abstract class Flight : IComparable<Flight>
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
        public virtual double CalculateFees()
        {
            double baseFee = 0.0;
            if (Destination == "Singapore (SIN)")
            {
                baseFee = 500.0;
            }
            else if (Origin == "Singapore (SIN)")
            {
                baseFee = 800.0;
            }
            return baseFee;
        }


        public override string ToString()
        {
            return $"{FlightNumber, -15} {Origin, -23} {Destination, -23} {ExpectedTime, -10} {Status, -12}"; 
        }

        public int CompareTo(Flight other)
        {
            return this.ExpectedTime.CompareTo(other.ExpectedTime);
        }
        //we did the flight classes together in school on one laptop
    }
}
