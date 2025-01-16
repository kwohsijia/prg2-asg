using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

//==========================================================
// Student Name	: Ian Tan Jun Yang (S10268190F)
// Student Name	: Kwoh Si Jia (S10267641F)
//==========================================================

namespace S10267641F_PRG2Assignment
{
    class NORMFlight : Flight
    {
        public NORMFlight(string flightNumber, string origin, string destination, DateTime expectedTime, string status = "On Time")
        : base(flightNumber, origin, destination, expectedTime, status) { }

        public override double CalculateFees()
        {
            double total = 0.0;
            if (Origin == "Singapore (SIN)")
            {
                total = 500.00;
            }
            else if (Destination == "Singapore (SIN)")
            {
                total = 800.00;
            }
            else
            {
                total = 0.00;
            }
            return total;
        }

        public override string ToString()
        {
            return base.ToString() + $" Fees: {CalculateFees():F2}";
        }
    }
}
