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
//==========================================================
// Student Number	: S10268190F
// Student Name	: Ian Tan Jun Yang
// Partner Name	: Kwoh Si Jia 
//==========================================================
namespace S10267641F_PRG2Assignment
{
    class LWTTFlight : Flight
    {
        public double RequestFee { get; set; }

        public LWTTFlight(string flightNumber, string origin, string destination, DateTime expectedTime, double requestFee, string status = "On Time")
            : base(flightNumber, origin, destination, expectedTime, status)
        {
            RequestFee = requestFee;
        }

        public override double CalculateFees()
        {
            double total;
            if (Origin == "SIN")
            {
                total = 500.00 + 500.00;
            }
            else if (Destination == "SIN")
            {
                total = 800.00 + 500.00;
            }
            else
            {
                total = 500.00;
            }
            return total;
        }

        public override string ToString()
        {
            return base.ToString() + $", Request Fee: {RequestFee :F2}, Total Fees: {CalculateFees():F2}";
        }
    }
}
