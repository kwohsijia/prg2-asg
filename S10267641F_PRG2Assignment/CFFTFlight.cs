using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10267641F_PRG2Assignment
{
    class CFFTFlight : Flight
    {
        public double RequestFee { get; set; }
        public CFFTFlight(string flightNumber, string origin, string destination, DateTime expectedTime, double requestFee, string status = "On Time"):base(flightNumber, origin, destination, expectedTime, status)
        {
            RequestFee = requestFee;
        }
        public override double CalculateFees()
        {
            return ;
        }
        public override string ToString()
        {
            return base.ToString() + $", Request Fee: {RequestFee}, Total Fees: {CalculateFees()}";
        }
    }
}
