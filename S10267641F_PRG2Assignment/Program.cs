using S10267641F_PRG2Assignment;
//==========================================================
// Student Number	: S10267641F
// Student Name	: Kwoh Si Jia
// Partner Name	: Ian Tan Jun Yang 
//==========================================================

List<Airline> airlineList = new List<Airline>();
Dictionary<string, BoardingGate> boardinggateDict = new Dictionary<string, BoardingGate>();
void LoadFiles(List<Airline> airlineList, Dictionary<string,BoardingGate> boardinggateDict)
{
    using (StreamReader sr = new StreamReader("airlines.csv"))
    {
        string? s = sr.ReadLine();
        while ((s = sr.ReadLine()) != null)
        {
            string[] data = s.Split(',');
            string name = data[0];
            string code = data[1];
            Airline newairline = new Airline(name, code);
            airlineList.Add(newairline);
        }
    }
    using (StreamReader sr = new StreamReader("boardinggates.csv"))
    {
        string? s = sr.ReadLine();
        while ((s = sr.ReadLine()) != null)
        {
            string[] data = s.Split(',');
            string gateName = data[0];
            string supportDDJB = data[1];
            string supportCFFT = data[2];
            string supportLWTT = data[3];
            BoardingGate newboardinggate = new BoardingGate(gateName,supportDDJB, supportCFFT, supportLWTT, new Flight());
            boardinggateDict.Add(gateName, newboardinggate);
        }
    }
}

LoadFiles(airlineList, boardinggateDict);

//==========================================================
// Student Number	: S10268190F
// Student Name	: Ian Tan Jun Yang
// Partner Name	: Kwoh Si Jia 
//==========================================================

Dictionary<string, Flight> flightDict = new Dictionary<string, Flight>();
void LoadFlights()
{
    using (StreamReader sr = new StreamReader("flights.csv"))
    {
        string s = sr.ReadLine();
        while ((s = sr.ReadLine()) != null)
        {
            string[] data = s.Split(',');
            string flightNumber = data[0];
            string origin = data[1];
            string destination = data[2];
            DateTime expectedTime = DateTime.Parse(data[3]);
            string status = data[4];
            string type = data[5];
            double requestFee = double.Parse(data[6]);
            if (type == "CFFT")
            {
                CFFTFlight newflight = new CFFTFlight(flightNumber, origin, destination, expectedTime, requestFee, status);
                flightDict.Add(flightNumber, newflight);
            }
            else if (type == "DDJB")
            {
                DDJBFlight newflight = new DDJBFlight(flightNumber, origin, destination, expectedTime, requestFee, status);
                flightDict.Add(flightNumber, newflight);
            }
            else if (type == "LWTT")
            {
                LWTTFlight newflight = new LWTTFlight(flightNumber, origin, destination, expectedTime, requestFee, status);
                flightDict.Add(flightNumber, newflight);
            }
        }
    }
}

LoadFlights();