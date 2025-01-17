﻿//==========================================================
// Student Name	: Ian Tan Jun Yang (S10268190F)
// Student Name	: Kwoh Si Jia (S10267641F)
//==========================================================
using S10267641F_PRG2Assignment;

Terminal terminal = new Terminal("Terminal 5");
int flightCount = 0;
int airlinecount = 0;
int boardingcount = 0;
LoadFlights(terminal);
LoadAirline(terminal);
LoadBoardingGate(terminal);
Console.WriteLine("Loading Airlines...");
Console.WriteLine($"{airlinecount} Airlines Loaded!");
Console.WriteLine("Loading Boarding Gates...");
Console.WriteLine($"{boardingcount} Boarding Gates Loaded!");
Console.WriteLine("Loading Flights...");
Console.WriteLine($"{flightCount} Flights Loaded!");
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
        ListFlights(terminal);
    }
    else if (option == 2)
    {
        ListBoardingGates(terminal);
    }
    else if (option == 3)
    {
        AssignBoardingGate(terminal);
    }
    else if (option == 4)
    {

    }
    else if (option == 5)
    {
        DisplayAirlineFlights(terminal);
    }
}

void LoadAirline(Terminal t)
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
            t.AddAirline(newairline);
            airlinecount++;
        }
    }
}

void LoadBoardingGate (Terminal t)
{
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
            BoardingGate boardingGate = new BoardingGate(gateName, Convert.ToBoolean(supportCFFT), Convert.ToBoolean(supportDDJB), Convert.ToBoolean(supportLWTT));
            t.AddBoardingGate(boardingGate);
            boardingcount++;
        }
    }
}


void ListBoardingGates(Terminal t) //this is option 2 in the sample output
{
    Console.WriteLine("=============================================");
    Console.WriteLine("List of Boarding Gates for Changi Airport Terminal 5");
    Console.WriteLine("=============================================");
    Console.WriteLine($"{"Gate Name",-16}{"DDJB",-23}{"CFFT",-23}LWTT");
    foreach (KeyValuePair<string, BoardingGate> kvp in t.BoardingGates)
    {
        BoardingGate boardingGate = kvp.Value;
        Console.WriteLine(boardingGate.ToString());
    }
}

void DisplayAirlineFlights(Terminal t)
{
    Console.WriteLine("=============================================");
    Console.WriteLine("List of Airlines for Changi Airport Terminal 5");
    Console.WriteLine("=============================================");

    Console.WriteLine($"{"Airline Code",-15}{"Airline Name",-20}");
    foreach(KeyValuePair<string, Airline> kvp in t.Airline)
    {
        Airline airline = kvp.Value;
        Console.WriteLine(airline.ToString());
    }
    Console.Write("Enter Airline Code: ");
    string? airlinecode = Console.ReadLine().ToUpper();
    int i = 0;
    //if (t.Airline.ContainsKey(airlinecode))
    //{
    //    Airline airline = t.Airline[airlinecode];

    //    Console.WriteLine("=============================================");
    //    Console.WriteLine($"List of Flights for {airline.Name}");
    //    Console.WriteLine("=============================================");
    //    Console.WriteLine($"{"Flight Number",-15}{"Airline Name",-23}{"Origin",-23}{"Destination",-23}Expected Departure/Arrival Time");

    //    if (airline.Flights.Count == 0)
    //    {
    //        Console.WriteLine("There are no available flights for this airline.");
    //    }
    //    else
    //    {
    //        foreach (Flight f in airline.Flights.Values)
    //        {
    //            Console.WriteLine($"{f.FlightNumber,-15}{t.GetAirlineFromFlight(f).Name,-23}{f.Origin,-23}{f.Destination,-23}{f.ExpectedTime}");
    //        }
    //    }
    //}
    foreach (Flight f in t.Flights.Values)
    {
        if (airlinecode == f.FlightNumber.Substring(0, 2))
        {

            Console.WriteLine("There are no available flights for this airline.");

            i++;

        }
    }
    
    if (i != 0)
    {
        Console.WriteLine("=============================================");
        Console.WriteLine($"List of Flights for {t.Airline[airlinecode]}");
        Console.WriteLine("=============================================");
        Console.WriteLine($"{"Flight Number",-15}{"Airline Name",-23}{"Origin",-23}{"Destination",-23}Expected Departure/Arrival Time");
        foreach (Flight f in t.Flights.Values)
        {
            if (airlinecode == f.FlightNumber.Substring(0, 2))
                Console.WriteLine($"{f.FlightNumber,-15}{t.GetAirlineFromFlight(f).Name,-23}{f.Origin,-23}{f.Destination,-23}{f.ExpectedTime}");
        }
    }

    else
    {
        Console.WriteLine("There are no available flights for this airline.");
    }

}

void LoadFlights(Terminal t)
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
                t.Flights.Add(flightNumber, newflight);
            }
            else if (type == "DDJB")
            {
                DDJBFlight newflight = new DDJBFlight(flightNumber, origin, destination, expectedTime);
                t.Flights.Add(flightNumber, newflight);
            }
            else if (type == "LWTT")
            {
                LWTTFlight newflight = new LWTTFlight(flightNumber, origin, destination, expectedTime);
                t.Flights.Add(flightNumber, newflight);
            }
            else
            {
                NORMFlight newflight = new NORMFlight(flightNumber, origin, destination, expectedTime);
                t.Flights.Add(flightNumber, newflight);
            }
            flightCount++;
        }
    }
}

void ListFlights(Terminal t)
{
    Console.WriteLine("=============================================\nList of Flights for Changi Airport Terminal 5\n=============================================");
    Console.WriteLine("{0,-15} {1,-23} {2,-23} {3,-23} {4,-10}", "Flight Number", "Airline Name", "Origin", "Destination", "Expected Departure/Arrival Time");
    foreach (Flight f in t.Flights.Values)
    {
        Console.WriteLine("{0,-15} {1,-23} {2,-23} {3,-23} {4,-10}", f.FlightNumber, t.GetAirlineFromFlight(f).Name, f.Origin, f.Destination, f.ExpectedTime);
    }
}

void AssignBoardingGate(Terminal t)
{
    Console.WriteLine("============================================\nAssign a Boarding Gate to a Flight\n============================================");
    Console.WriteLine("Enter Flight Number:");
    string flightNumber = Console.ReadLine();
    Console.WriteLine("Enter Boarding Gate Name:");
    string gateName = Console.ReadLine();
    Console.WriteLine($"Flight Number: {flightNumber}");
    Flight flight = t.Flights[flightNumber];
    Console.WriteLine($"Origin: {flight.Origin}");
    Console.WriteLine($"Destination: {flight.Destination}");
    Console.WriteLine($"Expected Time: {flight.ExpectedTime}");
    
}