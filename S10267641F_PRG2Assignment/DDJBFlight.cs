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
    class DDJBFlight : Flight
    {
        public double RequestFee { get; set; }

        public DDJBFlight(string flightNumber, string origin, string destination, DateTime expectedTime, double requestFee = 300.0)
            : base(flightNumber, origin, destination, expectedTime)
        {
            RequestFee = requestFee;
        }
        public override double CalculateFees()
        {
            double total;
            if (Origin == "Singapore (SIN)")
            {
                total = 500.00 + 300.00;
            }
            else if (Destination == "Singapore (SIN)")
            {
                total = 800.00 + 300.00;
            }
            else
            {
                total = 300.00;
            }
            return total;
        }

        public override string ToString()
        {
            return base.ToString() + $" Request Fee: {RequestFee :F2} Total Fees: {CalculateFees() :F2}";
        }
    }
}
