﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//==========================================================
// Student Number	: S10267641F
// Student Name	: Kwoh Si Jia
// Partner Name	: Ian Tan Jun Yang 
//==========================================================
// Student Number	: S10268190F
// Student Name	: Ian Tan Jun Yang
// Partner Name	: Kwoh Si Jia 
//==========================================================
//==========================================================
namespace S10267641F_PRG2Assignment
{
    class DDJBFlight : Flight
    {
        public double RequestFee { get; set; }

        public DDJBFlight(string flightNumber, string origin, string destination, DateTime expectedTime, double requestFee = 300.0, string status = "On Time")
            : base(flightNumber, origin, destination, expectedTime, status)
        {
            RequestFee = requestFee;
        }
        public override double CalculateFees()
        {
            double total;
            if (Origin.Split(" ")[1].Trim('(', ')') == "SIN")
            {
                total = 500.00 + 300.00;
            }
            else if (Destination.Split(" ")[1].Trim('(', ')') == "SIN")
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
