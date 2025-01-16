//==========================================================
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
        Console.WriteLine("=============================================\nList of Flights for Changi Airport Terminal 5\n=============================================");
        Console.WriteLine("{0,-15} {1,-15} {2,-10} {3,-15} {4,-10}", "Flight Number", "Airline Name", "Origin", "Destination", "Expected Departure/Arrival Time");
    }
    else if (option == 2)
    {
        ListBoardingGates(terminal);
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

void LoadFlights(Terminal t)
{
    using (StreamReader sr = new StreamReader("flights.csv"))
    {
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
        flightCount++;
    }
    
}