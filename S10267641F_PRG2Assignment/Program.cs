//==========================================================
// Student Name	: Ian Tan Jun Yang (S10268190F)
// Student Name	: Kwoh Si Jia (S10267641F)
//==========================================================
using S10267641F_PRG2Assignment;
using System.ComponentModel;
using System.Diagnostics.Metrics;

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
    //list all the Airlines available
    Console.WriteLine("=============================================");
    Console.WriteLine("List of Airlines for Changi Airport Terminal 5");
    Console.WriteLine("=============================================");
    Console.WriteLine($"{"Airline Code",-15}{"Airline Name",-20}");

    foreach(KeyValuePair<string, Airline> kvp in t.Airline)
    {
        Airline airline = kvp.Value;
        Console.WriteLine(airline.ToString());
    }
    
    Console.Write("Enter Airline Code: ");//prompt the user to enter the 2 - Letter Airline Code(e.g.SQ or MH, etc.)
    string? airlinecode = Console.ReadLine().ToUpper();

    //retrieve the Airline object selected
    Console.WriteLine("=============================================");
    Console.WriteLine($"List of Flights for {t.Airline[airlinecode].Name}");
    Console.WriteLine("=============================================");

    //for each Flight from that Airline, show their Airline Number, Origin and Destination
    int i = 0;
    int j = 0;

    foreach(Flight f in t.Flights.Values)
    {
        if (airlinecode == f.FlightNumber.Substring(0, 2))
        {
            i++;
        }
    }

    if (i != 0)
    {
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

    //prompt the user to select a Flight Number
    Console.Write("Enter Flight Number: ");
    string? flightno = Console.ReadLine().ToUpper();

    //retrieve the Flight object selected
    foreach (Flight f in t.Flights.Values)
    {
        if (flightno == f.FlightNumber)
        {
            i++;
        }
    }

    if (i != 0)
    {
        Console.WriteLine($"{"Flight Number",-15}{"Airline Name",-23}{"Origin",-23}{"Destination",-23}Expected Departure/Arrival Time");
        foreach (Flight f in t.Flights.Values)
        {
            if (flightno == f.FlightNumber)
                Console.WriteLine($"{f.FlightNumber,-15}{t.GetAirlineFromFlight(f).Name,-23}{f.Origin,-23}{f.Destination,-23}{f.ExpectedTime}"); //havent do special req code and bg
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
    if (t.BoardingGates.ContainsKey(gateName))
    {
        Console.WriteLine("Boarding Gate has already been assigned.");
    }
    else
    {
        foreach (Flight f in t.Flights.Values)
        {
            if (f.FlightNumber == flightNumber)
            {
                Console.WriteLine($"Flight Number: {f.FlightNumber}");
                Console.WriteLine($"Origin: {f.Origin}");
                Console.WriteLine($"Destination: {f.Destination}");
                Console.WriteLine($"Expected Departure/Arrival: {f.ExpectedTime}");

                if (f is DDJBFlight)
                {
                    Console.WriteLine($"Special Request Code: DDJB");
                }
                else if (f is CFFTFlight)
                {
                    Console.WriteLine($"Special Request Code: CFFT");
                }
                else if (f is LWTTFlight)
                {
                    Console.WriteLine($"Special Request Code: LWTT");
                }
                else
                {
                    Console.WriteLine($"Special Request Code: None");
                }
                break;
            }
        }
    }
}

    //Console.WriteLine("Enter Boarding Gate Name:");
    //string gateName = Console.ReadLine();
    //BoardingGate selectedbg = null;
    //int i = 0;
    //while (true)
    //{

    //    if (!t.BoardingGates.ContainsKey(gateName))
    //    {
    //        Console.WriteLine("Invalid Boarding Gate ID. Please try again.");
    //        continue;
    //    }

    //    selectedbg = t.BoardingGates[gateName];
        
    //    if (selectedbg.Flight != null)
    //    {
    //        while (i < 1)
    //        {
    //            Console.WriteLine($"Boarding Gate {gateName} is already assigned to Flight {selectedbg.Flight.FlightNumber}. Please choose a different gate.");
    //            break;
    //        }
    //        i++;
    //        continue;
    //    }

    //    foreach (Flight f in t.Flights.Values)
    //    {
    //        selectedbg.Flight = f;
    //        Console.WriteLine("Boarding Gate assigned successfully!");
    //        Console.WriteLine($"Flight Number: {f.FlightNumber}");
    //        Console.WriteLine($"Boarding Gate: {selectedbg.GateName}");
    //        break;
    //    }
    //}

    //Console.WriteLine($"Flight Number: {flightNumber}");
    //Flight flight = t.Flights[flightNumber];
    //Console.WriteLine($"Origin: {flight.Origin}");
    //Console.WriteLine($"Destination: {flight.Destination}");
    //Console.WriteLine($"Expected Time: {flight.ExpectedTime}");