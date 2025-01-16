﻿using S10267641F_PRG2Assignment;

Dictionary<string, Airline> airlineDict = new Dictionary<string, Airline>();
Dictionary<string, BoardingGate> boardinggateDict = new Dictionary<string, BoardingGate>();
Dictionary<string, Flight> flightDict = new Dictionary<string, Flight>();
int count = 0;
LoadFlights();
Console.WriteLine("Loading Flights...");
Console.WriteLine($"{count} Flights Loaded!");
while (true)
{
    Console.WriteLine("=============================================");
    Console.WriteLine("Welcome to Changi Airport Terminal 5");
    Console.WriteLine("=============================================");
    Console.WriteLine("1. List All Flights");
    Console.WriteLine("2. List Boarding Gates");
    Console.WriteLine("3. Assign Boarding Gate to a Flight");
    Console.WriteLine("4. Create Flight");
    Console.WriteLine("5. Display Airline Flights");
    Console.WriteLine("6. Modify Flight Details");
    Console.WriteLine("7. Display Flight Schedule");
    Console.WriteLine("0. Exit");
    Console.WriteLine("\nPlease select your option:");
    int option = Convert.ToInt32(Console.ReadLine());

    if (option == 0)
    {
        break;
    }

    else if (option == 1)
    {
        Console.WriteLine("Flight Number");
    }
    else if (option == 2)
    {
        ListBoardingGates(boardinggateDict);
    }
}
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

            foreach (KeyValuePair<string, Airline> kvp in airlineDict)
            {
                foreach(KeyValuePair<string, Flight> kvp2 in kvp.Value.Flights)
                {
                    BoardingGate newboardinggate = new BoardingGate(gateName, Convert.ToBoolean(supportDDJB), Convert.ToBoolean(supportCFFT), Convert.ToBoolean(supportLWTT), kvp2.Value);
                    boardinggateDict.Add(gateName, newboardinggate);
                }
            }
            
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
    foreach(KeyValuePair<string, BoardingGate> kvp in boardinggateDict)
    {
        BoardingGate boardingGate = kvp.Value;
        Console.WriteLine(boardingGate.ToString());
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

//Basic Feature 2
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
            string type = data[4];
            if (type == "CFFT")
            {
                CFFTFlight newflight = new CFFTFlight(flightNumber, origin, destination, expectedTime);
                flightDict.Add(flightNumber, newflight);
            }
            else if (type == "DDJB")
            {
                DDJBFlight newflight = new DDJBFlight(flightNumber, origin, destination, expectedTime);
                flightDict.Add(flightNumber, newflight);
            }
            else if (type == "LWTT")
            {
                LWTTFlight newflight = new LWTTFlight(flightNumber, origin, destination, expectedTime);
                flightDict.Add(flightNumber, newflight);
            }
            count++;
        }
    }
}