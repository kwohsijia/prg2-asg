using S10267641F_PRG2Assignment;
using System.Numerics;
//==========================================================
// Student Number	: S10267641F
// Student Name	: Kwoh Si Jia
// Partner Name	: Ian Tan Jun Yang 
//==========================================================

Dictionary<string, Airline> airlineDict = new Dictionary<string, Airline>();
Dictionary<string, BoardingGate> boardinggateDict = new Dictionary<string, BoardingGate>();
void LoadFiles(Dictionary<string, Airline> airlineDict, Dictionary<string,BoardingGate> boardinggateDict)
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
            airlineDict.Add(name, newairline);
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
            BoardingGate newboardinggate = new BoardingGate(gateName,Convert.ToBoolean(supportDDJB), Convert.ToBoolean(supportCFFT), Convert.ToBoolean(supportLWTT), new Flight());
            boardinggateDict.Add(gateName, newboardinggate);
        }
    }
}

LoadFiles(airlineDict, boardinggateDict);

void ListBoardingGates(Dictionary<string, BoardingGate> boardinggateDict) //this is option 2 in the sample output
{
    Console.WriteLine("=============================================");
    Console.WriteLine("List of Boarding Gates for Changi Airport Terminal 5");
    Console.WriteLine("=============================================");
    Console.WriteLine($"{"Gate Name",-16}{"DDJB",-23}{"CFFT",-23}LWTT");
    foreach (BoardingGate bg in boardinggateDict.Values)
    {
        Console.WriteLine(bg.ToString());
    }
}

void DisplayFlightDetails(Dictionary<string, Airline> airlineDict, Dictionary<string, BoardingGate> boardinggateDict) //this is option 5 in the sample output 
{

}
//==========================================================
// Student Number	: S10268190F
// Student Name	: Ian Tan Jun Yang
// Partner Name	: Kwoh Si Jia 
//==========================================================
void LoadFlights()
{
    using (StreamReader sr = new StreamReader("flights.csv"))
    {

    }
}