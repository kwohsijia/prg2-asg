using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10267641F_PRG2Assignment
{
    class DDJBFlight : Flight
    {
        public double RequestFee { get; set; }

        public DDJBFlight(string flightNumber, string origin, string destination, DateTime expectedTime, double requestFee, string status = "On Time")
            : base(flightNumber, origin, destination, expectedTime, status)
        {
            RequestFee = requestFee;
        }
        public override double CalculateFees()
        {
            double total;
            if (Origin == "SIN")
            {
                total = 500.00 + 300.00;
            }
            else if (Destination == "SIN")
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
