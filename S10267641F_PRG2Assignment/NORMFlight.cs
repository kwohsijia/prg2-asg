using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

//==========================================================
// Student Number	: S10267641F
// Student Name	: Kwoh Si Jia
// Partner Name	: Ian Tan Jun Yang 
//==========================================================

namespace S10267641F_PRG2Assignment
{
    class NORMFlight : Flight
    {
        public NORMFlight(string flightNumber, string origin, string destination, DateTime expectedTime, string status = "On Time")
        : base(flightNumber, origin, destination, expectedTime, status)
        { }

        public override double CalculateFees()
        {
            double total;
            if (Origin == "SIN")
            {
                total = 500.00;
            }
            else if (Destination == "SIN")
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
            return base.ToString() + $", Fees: {CalculateFees():F2}";
        }
    }
}
