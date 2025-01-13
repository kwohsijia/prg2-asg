using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10267641F_PRG2Assignment
{
    class NORMFlight : Flight
    {
        public NORMFlight(string flightNumber, string origin, string destination, DateTime expectedTime, string status = "On Time")
        : base(flightNumber, origin, destination, expectedTime, status)
        { }

        public override double CalculateFees()
        {
            return;
        }

        public override string ToString()
        {
            return base.ToString() + $", Fees: {CalculateFees()}";
        }
    }
}
