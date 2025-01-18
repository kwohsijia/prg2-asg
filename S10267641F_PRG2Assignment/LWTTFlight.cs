using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

//==========================================================
// Student Number	: S10267641F
// Student Name	: Kwoh Si Jia
// Partner Name	: Ian Tan Jun Yang 
//==========================================================
// Student Name	: Ian Tan Jun Yang (S10268190F)
// Student Name	: Kwoh Si Jia (S10267641F)
//==========================================================
namespace S10267641F_PRG2Assignment
{
    class LWTTFlight : Flight
    {
        public double RequestFee { get; set; }

        public LWTTFlight(string flightNumber, string origin, string destination, DateTime expectedTime, double requestFee = 500.0, string status = "On Time")
            : base(flightNumber, origin, destination, expectedTime, status)
        {
            RequestFee = requestFee;
        }

        public override double CalculateFees()
        {
            return base.CalculateFees() + RequestFee;
        }

        public override string ToString()
        {
            return base.ToString() + $" Request Fee: {RequestFee :F2} Total Fees: {CalculateFees():F2}";
        }
    }
}
