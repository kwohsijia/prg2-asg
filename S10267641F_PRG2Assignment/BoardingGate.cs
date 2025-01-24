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
    class BoardingGate
    {
        public string GateName { get; set; }
        public bool SupportsCFFT { get; set; }
        public bool SupportsDDJB { get; set; }
        public bool SupportsLWTT { get; set; }
        public Flight Flight { get; set; }

        public BoardingGate(string gateName, bool supportsCFFT, bool supportsDDJB, bool supportsLWTT)
        {
            GateName = gateName;
            SupportsCFFT = supportsCFFT;
            SupportsDDJB = supportsDDJB;
            SupportsLWTT = supportsLWTT;
            Flight = null;
        }

        public double CalculateFees()
        {
            double baseFee = 300.0;
            double arrdepFee = 0.0;
            double specialFee = 0.0;

            if (Flight.Destination == "Singapore (SIN)")
            {
                arrdepFee = 500.0;
            }

            else
            {
                arrdepFee = 800.0;
            }

            if (SupportsCFFT)
            {
                specialFee = 150.0;
            }

            else if (SupportsDDJB)
            {
                specialFee = 300.0;
            }

            else if (SupportsLWTT)
            {
                specialFee = 500.0;
            }
            return baseFee + arrdepFee;
        }

        public override string ToString()
        {
            return $"{GateName,-16}{SupportsDDJB,-23}{SupportsCFFT,-23}{SupportsLWTT}";
        }

    }
}
