//==========================================================
// Student Name	: Ian Tan Jun Yang (S10268190F)
// Student Name	: Kwoh Si Jia (S10267641F)
//==========================================================
using S10267641F_PRG2Assignment;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Diagnostics.Metrics;

Terminal terminal = new Terminal("Terminal 5");
//Dictionary to store the special request code of each flight
Dictionary<string, string> flightToCode = new Dictionary<string, string>();
LoadAirline(terminal);
LoadBoardingGate(terminal);
LoadFlights(terminal, flightToCode);

//Main program
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
    Console.WriteLine("8. Assign remaining flights to boarding gates");
    Console.WriteLine("9. Display Airline Fees For The Day");
    Console.WriteLine("0. Exit");

    try
    {
        Console.WriteLine("\nPlease select your option:");
        string? input = Console.ReadLine();

        // Ensure input is not null or empty
        if (string.IsNullOrEmpty(input))
        {
            Console.WriteLine("Input cannot be empty. Please enter a valid option.");
            continue;
        }

        // Attempt to parse input
        if (!int.TryParse(input, out int option))
        {
            Console.WriteLine("Invalid input. Please enter a number between 0 and 9.");
            continue;
        }

        // Validate the range of the option
        if (option < 0 || option > 9)
        {
            Console.WriteLine("Please enter a valid option from 0 to 9.");
            continue;
        }

        // Handle menu options
        switch (option)
        {
            case 0:
                Console.WriteLine("Goodbye!");
                return;
            case 1:
                ListFlights(terminal);
                break;
            case 2:
                ListBoardingGates(terminal);
                break;
            case 3:
                AssignBoardingGate(terminal);
                break;
            case 4:
                CreateFlight(terminal);
                break;
            case 5:
                DisplayAirlineFlights(terminal);
                break;
            case 6:
                ModifyFlightDetails(terminal);
                break;
            case 7:
                DisplayFlightDetails(terminal);
                break;
            case 8:
                ProcessFlightsInBulk(terminal);
                break;
            case 9:
                DisplayAirlineFees(terminal);
                break;
            default:
                Console.WriteLine("Unexpected error occurred. Please try again.");
                break;
        }
    }
    catch (Exception ex)
    {
        // Catch unexpected exceptions
        Console.WriteLine($"An unexpected error occurred: {ex.Message}");
    }
}


//Basic Feature 1: Load files (airlines and boarding gates)
void LoadAirline(Terminal t)
{
    try
    {
        int airlinecount = 0;
        //Load the airlines.csv file
        using (StreamReader sr = new StreamReader("airlines.csv"))
        {
            string? s = sr.ReadLine();
            while ((s = sr.ReadLine()) != null)
            {
                string[] data = s.Split(',');
                string name = data[0];
                string code = data[1];
                //Create airline object
                Airline newairline = new Airline(name, code);
                //Add airline object
                t.AddAirline(newairline);
                airlinecount++;
            }
            Console.WriteLine("Loading Airlines...");
            Console.WriteLine($"{airlinecount} Airlines Loaded!");
        }
    }

    catch (ArgumentException)
    {
        Console.WriteLine("Error: airlines.csv file is empty. Please ensure the file is not empty.");
    }
    catch (FileNotFoundException)
    {
        Console.WriteLine("Error: airlines.csv file not found. Please ensure the file is in the correct directory.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred: {ex.Message}");
    }
}

void LoadBoardingGate (Terminal t)
{
    int boardingcount = 0;
    //Load the boardinggates.csv file
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
            //Create boarding gate objects
            BoardingGate boardingGate = new BoardingGate(gateName, Convert.ToBoolean(supportCFFT), Convert.ToBoolean(supportDDJB), Convert.ToBoolean(supportLWTT));
            ////Add boarding gate objects
            t.AddBoardingGate(boardingGate);
            boardingcount++;
        }
        Console.WriteLine("Loading Boarding Gates...");
        Console.WriteLine($"{boardingcount} Boarding Gates Loaded!");
    }
}

//Basic Feature 2: Load files (flights)
void LoadFlights(Terminal t, Dictionary<string, string> d)
{
    try
    {
        int flightCount = 0;
        //Load flights.csv file
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
                //create flight objects and add into dictionary according to special request code
                if (type == "CFFT")
                {
                    CFFTFlight newflight = new CFFTFlight(flightNumber, origin, destination, expectedTime);
                    t.Flights.Add(flightNumber, newflight);
                    t.GetAirlineFromFlight(newflight).AddFlight(newflight);
                    flightToCode.Add(flightNumber, "CFFT");
                }
                else if (type == "DDJB")
                {
                    DDJBFlight newflight = new DDJBFlight(flightNumber, origin, destination, expectedTime);
                    t.Flights.Add(flightNumber, newflight);
                    t.GetAirlineFromFlight(newflight).AddFlight(newflight);
                    flightToCode.Add(flightNumber, "DDJB");
                }
                else if (type == "LWTT")
                {
                    LWTTFlight newflight = new LWTTFlight(flightNumber, origin, destination, expectedTime);
                    t.Flights.Add(flightNumber, newflight);
                    t.GetAirlineFromFlight(newflight).AddFlight(newflight);
                    flightToCode.Add(flightNumber, "LWTT");
                }
                else
                {
                    NORMFlight newflight = new NORMFlight(flightNumber, origin, destination, expectedTime);
                    t.Flights.Add(flightNumber, newflight);
                    t.GetAirlineFromFlight(newflight).AddFlight(newflight);
                    flightToCode.Add(flightNumber, "None");
                }
                flightCount++;
            }
            Console.WriteLine("Loading Flights...");
            Console.WriteLine($"{flightCount} Flights Loaded!");
        }
    }
    catch (ArgumentException)
    {
        Console.WriteLine("Error: flights.csv file is empty. Please ensure the file is not empty.");
    }
    catch (FileNotFoundException)
    {
        Console.WriteLine("Error: flights.csv file not found. Please ensure the file is in the correct directory.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred: {ex.Message}");

    }
}

    //Basic Feature 3: List all flights with their basic information
    void ListFlights(Terminal t)
{
    Console.WriteLine("=============================================\nList of Flights for Changi Airport Terminal 5\n=============================================");
    Console.WriteLine("{0,-15} {1,-23} {2,-23} {3,-23} {4,-10}", "Flight Number", "Airline Name", "Origin", "Destination", "Expected Departure/Arrival Time");
    try
    {
        //list all flights in the flight dictionary
        foreach (Flight f in t.Flights.Values)
        {
            Console.WriteLine("{0,-15} {1,-23} {2,-23} {3,-23} {4,-10}", f.FlightNumber, t.GetAirlineFromFlight(f).Name, f.Origin, f.Destination, f.ExpectedTime);
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred: {ex.Message}");
    }
}

//Basic Feature 4: List all boarding gates
void ListBoardingGates(Terminal t) 
{
    Console.WriteLine("=============================================");
    Console.WriteLine("List of Boarding Gates for Changi Airport Terminal 5");
    Console.WriteLine("=============================================");
    Console.WriteLine($"{"Gate Name",-16}{"DDJB",-23}{"CFFT",-23}{"LWTT",-23}Flight");
    try
    {
        foreach (KeyValuePair<string, BoardingGate> kvp in t.BoardingGates)
        {
            if (kvp.Value.Flight == null)
            {
                Console.WriteLine($"{kvp.Key,-16}{kvp.Value.SupportsDDJB,-23}{kvp.Value.SupportsCFFT,-23}{kvp.Value.SupportsLWTT,-23}");
            }
            else
            {
                Console.WriteLine($"{kvp.Key,-16}{kvp.Value.SupportsDDJB,-23}{kvp.Value.SupportsCFFT,-23}{kvp.Value.SupportsLWTT,-23}{kvp.Value.Flight.FlightNumber}");
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred: {ex.Message}");
    }
}

//Basic Feature 5: Assign a boarding gate to a flight
void AssignBoardingGate(Terminal t) // Basic feature 5
{

    Console.WriteLine("============================================\nAssign a Boarding Gate to a Flight\n============================================");
    while (true)
    {
        try
        {
            Console.WriteLine("Enter Flight Number:");
            string flightNumber = Console.ReadLine().ToUpper();

            // Validate if the flight number exists
            if (!t.Flights.ContainsKey(flightNumber))
            {
                Console.WriteLine("Error: Flight number does not exist. Please try again.");
                continue; // Prompt user again
            }

            Console.WriteLine("Enter Boarding Gate Name:");
            string gateName = Console.ReadLine().ToUpper();

            // Validate if the boarding gate exists
            if (!t.BoardingGates.ContainsKey(gateName))
            {
                Console.WriteLine("Error: Boarding gate does not exist. Please try again.");
                continue; // Prompt user again
            }

            // Check if the boarding gate is assigned to a flight
            if (t.BoardingGates[gateName].Flight == null)
            {
                // Assign flight to boarding gate
                Flight f = t.Flights[flightNumber];
                t.BoardingGates[gateName].Flight = f;

                Console.WriteLine($"Flight Number: {f.FlightNumber}");
                Console.WriteLine($"Origin: {f.Origin}");
                Console.WriteLine($"Destination: {f.Destination}");
                Console.WriteLine($"Expected Time: {f.ExpectedTime}");
                Console.WriteLine($"Boarding Gate Name: {gateName}");

                // Display special request code
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
                Console.WriteLine($"Supports DDJB: {t.BoardingGates[gateName].SupportsDDJB}");
                Console.WriteLine($"Supports CFFT: {t.BoardingGates[gateName].SupportsCFFT}");
                Console.WriteLine($"Supports LWTT: {t.BoardingGates[gateName].SupportsLWTT}");

                Console.WriteLine("Would you like to update the status of the flight? (Y/N)");
                string updateStatus = Console.ReadLine().ToUpper();
                if (updateStatus == "Y")
                {
                    Console.WriteLine("1. Delayed\n2. Boarding\n3. On Time");
                    Console.WriteLine("Please select the new status of the flight:");
                    string statusOption = Console.ReadLine();

                    if (statusOption == "1")
                    {
                        f.Status = "Delayed";
                    }
                    else if (statusOption == "2")
                    {
                        f.Status = "Boarding";
                    }
                    else if (statusOption == "3")
                    {
                        f.Status = "On Time";
                    }
                    else
                    {
                        Console.WriteLine("Error: Invalid status option. Keeping status as 'On Time'.");
                        f.Status = "On Time";
                    }
                    Console.WriteLine($"Flight {flightNumber} has been assigned to Boarding Gate {gateName}!");
                }
                else
                {
                    f.Status = "On Time";
                    Console.WriteLine($"Flight {flightNumber} has been assigned to Boarding Gate {gateName}!");
                }
                break; // Exit the loop after successful assignment
            }
            else
            {
                Console.WriteLine("Boarding Gate has already been assigned. Please try a different boarding gate.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}

//Basic feature 6: Create a new flight
void CreateFlight(Terminal t) 
{
    while (true)
    {
        try
        {
            Console.Write("Enter Flight Number: ");
            string newFlightNumber = Console.ReadLine();

            // Check if the flight number already exists
            if (t.Flights.ContainsKey(newFlightNumber))
            {
                Console.WriteLine("Error: Flight number already exists. Please enter a unique flight number.");
                continue; // Prompt user again
            }

            Console.Write("Enter Origin: ");
            string newOrigin = Console.ReadLine();

            Console.Write("Enter Destination: ");
            string newDestination = Console.ReadLine();

            Console.Write("Enter Expected Departure/Arrival Time (dd/MM/yyyy HH:mm): ");
            DateTime newExpectedTime;
            if (!DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy HH:mm", null, System.Globalization.DateTimeStyles.None, out newExpectedTime))
            {
                Console.WriteLine("Error: Invalid date format. Please enter in the format dd/MM/yyyy HH:mm.");
                continue; // Prompt user again
            }

            Console.Write("Enter Special Request Code (CFFT/DDJB/LWTT/None): ");
            string newType = Console.ReadLine().ToUpper();


            // Create flight according to special request code and append to flights.csv
            Flight newFlight;
            switch (newType)
            {
                case "CFFT":
                    newFlight = new CFFTFlight(newFlightNumber, newOrigin, newDestination, newExpectedTime);
                    break;
                case "DDJB":
                    newFlight = new DDJBFlight(newFlightNumber, newOrigin, newDestination, newExpectedTime);
                    break;
                case "LWTT":
                    newFlight = new LWTTFlight(newFlightNumber, newOrigin, newDestination, newExpectedTime);
                    break;
                default:
                    newFlight = new NORMFlight(newFlightNumber, newOrigin, newDestination, newExpectedTime);
                    break;
            }

            // Add the new flight to the terminal and the flight-to-code mapping
            t.Flights.Add(newFlightNumber, newFlight);
            t.GetAirlineFromFlight(newFlight).AddFlight(newFlight);
            flightToCode.Add(newFlightNumber, newType == "None" ? string.Empty : newType);

            // Append to flights.csv
            using (StreamWriter sw = new StreamWriter("flights.csv", true))
            {
                sw.WriteLine($"{newFlightNumber},{newOrigin},{newDestination},{newExpectedTime:dd/MM/yyyy HH:mm},{newType}");
            }

            Console.WriteLine($"Flight {newFlightNumber} has been added!");

            Console.WriteLine("Would you like to add another flight? (Y/N)");
            string addAnotherFlight = Console.ReadLine().ToUpper();
            if (addAnotherFlight == "N")
            {
                break;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
//Basic feature 7: Display full flight details from an airline
void DisplayAirlinefromCode (Terminal t)
{
    //list all the Airlines available
    Console.WriteLine("=============================================");
    Console.WriteLine("List of Airlines for Changi Airport Terminal 5");
    Console.WriteLine("=============================================");
    Console.WriteLine($"{"Airline Code",-15}{"Airline Name",-20}");

    foreach (KeyValuePair<string, Airline> kvp in t.Airlines)
    {
        Airline airline = kvp.Value;
        Console.WriteLine(airline.ToString());
    }

    Console.Write("Enter Airline Code: ");//prompt the user to enter the 2 - Letter Airline Code(e.g.SQ or MH, etc.)
    string? airlinecode = Console.ReadLine().ToUpper();

    //retrieve the Airline object selected
    Console.WriteLine("=============================================");
    Console.WriteLine($"List of Flights for {t.Airlines[airlinecode].Name}");
    Console.WriteLine("=============================================");

    //for each Flight from that Airline, show their Airline Number, Origin and Destination
    int i = 0;
    foreach (Flight flight in t.Flights.Values)
    {
        if (airlinecode == flight.FlightNumber.Substring(0, 2))
        {
            i++;
        }
    }

    if (i != 0)
    {
        Console.WriteLine($"{"Flight Number",-15}{"Airline Name",-23}{"Origin",-23}{"Destination",-23}Expected Departure/Arrival Time");
        foreach (Flight flight in t.Flights.Values)
        {
            if (airlinecode == flight.FlightNumber.Substring(0, 2))
                Console.WriteLine($"{flight.FlightNumber,-15}{t.GetAirlineFromFlight(flight).Name,-23}{flight.Origin,-23}{flight.Destination,-23}{flight.ExpectedTime}");
        }
    }
    else
    {
        Console.WriteLine("There are no available flights for this airline.");
    }
}
void DisplayAirlineFlights(Terminal t)
{
    try
    {
        DisplayAirlinefromCode(t);
        int i = 0;

        // Prompt the user to select a Flight Number
        Console.Write("Enter Flight Number: ");
        string? flightno = Console.ReadLine()?.ToUpper();

        if (string.IsNullOrEmpty(flightno))
        {
            Console.WriteLine("Flight number cannot be empty. Please try again.");
            return;
        }

        // Retrieve the Flight object selected
        Flight? f;
        if (!t.Flights.TryGetValue(flightno, out f))
        {
            Console.WriteLine("Flight number not found. Please try again.");
            return;
        }

        foreach (Flight flight in t.Flights.Values)
        {
            if (flightno == flight.FlightNumber)
            {
                i++;
            }
        }

        if (i != 0)
        {
            Console.WriteLine($"{"Flight Number",-15}{"Airline Name",-23}{"Origin",-23}{"Destination",-23}{"Expected Departure/Arrival Time",-33}{"Special Request Code",-23}{"Boarding Gate",-15}");
            foreach (Flight flight in t.Flights.Values)
            {
                if (flight.FlightNumber == flightno)
                {
                    // Determine the special request code
                    string specialreq = "";

                    if (flight is DDJBFlight)
                    {
                        specialreq = "DDJB";
                    }
                    else if (flight is CFFTFlight)
                    {
                        specialreq = "CFFT";
                    }
                    else if (flight is LWTTFlight)
                    {
                        specialreq = "LWTT";
                    }

                    // Determine Boarding Gate 
                    string boardingGate = "Not Assigned";
                    Console.WriteLine($"{flight.FlightNumber,-15}{t.GetAirlineFromFlight(flight).Name,-23}{flight.Origin,-23}{flight.Destination,-23}{flight.ExpectedTime,-33}{specialreq,-23}{boardingGate}");
                }
            }
        }
        else
        {
            Console.WriteLine("There are no available flights for this airline.");
        }
    }
    catch (KeyNotFoundException)
    {
        Console.WriteLine("The flight number you entered does not exist.");
    }
    catch (NullReferenceException)
    {
        Console.WriteLine("An error occurred while accessing flight information. Please ensure all data is initialized.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An unexpected error occurred: {ex.Message}");
    }
}

//Basic Feature 8: Modify Flight Details
void ModifyFlightDetails(Terminal t)
{
    try
    {
        DisplayAirlinefromCode(t);

        Console.WriteLine("Choose an existing Flight to modify or delete:  ");
        string flightNum = Console.ReadLine().ToUpper();

        if (!t.Flights.ContainsKey(flightNum))
        {
            Console.WriteLine("Error: Flight not found. Please enter a valid flight number.");
            return;
        }

        Console.WriteLine("1. Modify Flight");
        Console.WriteLine("2. Delete Flight");
        Console.WriteLine("Choose an option: ");

        if (!int.TryParse(Console.ReadLine(), out int option) || (option != 1 && option != 2))  //.TryParse() prevents runtime exception if user enter non-numeric values. This check ensures that if the option is not 1 or 2 or if the option is non-numeric, then an error message will be printed. 
        {
            Console.WriteLine("Invalid option. Please enter 1 or 2.");
            return;
        }
        Flight flight = t.Flights[flightNum];

        if (option == 1)
        {
            Console.WriteLine("Choose an option to modify:");
            Console.WriteLine("1. Modify Origin/Destination/Expected Time");
            Console.WriteLine("2. Modify Flight Status");
            Console.WriteLine("3. Modify Special Request Code");
            Console.WriteLine("4. Modify Boarding Gate");
            Console.Write("Enter your choice: ");

            if (!int.TryParse(Console.ReadLine(), out int modifyOption) || modifyOption < 1 || modifyOption > 4)
            {
                Console.WriteLine("Invalid option. Please enter a number between 1 and 4.");
                return;
            }

            switch (modifyOption)
            { //switch is an alternative to if-else. it is better as it automatically exits after executing a case due to break. it performs better when checking against multiple values.

                case 1:

                    Console.Write("Enter new Origin: ");
                    flight.Origin = Console.ReadLine();

                    Console.Write("Enter new Destination: ");
                    flight.Destination = Console.ReadLine();

                    Console.Write("Enter new Expected Departure/Arrival Time (dd/mm/yyyy hh:mm): ");
                    if (!DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy HH:mm", null, System.Globalization.DateTimeStyles.None, out DateTime newTime)) //.TryParseExact() ensures a consistent date format
                    {
                        Console.WriteLine("Invalid date format. Please use dd/MM/yyyy HH:mm.");
                        return;
                    }

                    flight.ExpectedTime = newTime;
                    Console.WriteLine("Flight details updated successfully. ");
                    break;

                case 2:

                    Console.WriteLine("1. Delayed");
                    Console.WriteLine("2. Boarding");
                    Console.WriteLine("3. On Time");
                    Console.Write("Choose new status: ");

                    if (!int.TryParse(Console.ReadLine(), out int newStatus) || newStatus < 1 || newStatus > 3) //similar to option but this one restrict the input value to 1,2 and 3
                    {
                        Console.WriteLine("Invalid option. Please enter 1, 2, or 3.");
                        return;
                    }

                    if (newStatus == 1)
                    {
                        flight.Status = "Delayed";
                    }
                    else if (newStatus == 2)
                    {
                        flight.Status = "Boarding";
                    }
                    else if (newStatus == 3)
                    {
                        flight.Status = "On Time";
                    }
                    Console.WriteLine("Flight status has been updated.\n");
                    break;


                case 3:

                    Console.Write("Enter new Special Request Code (CFFT/DDJB/LWTT/None): ");
                    string specialcode = Console.ReadLine().ToUpper();

                    if (specialcode != "CFFT" && specialcode != "DDJB" && specialcode != "LWTT" && specialcode != "NONE")
                    {
                        Console.WriteLine("Invalid code. Please enter CFFT, DDJB, LWTT, or None.");
                        Console.WriteLine();
                    }

                    flightToCode[flightNum] = specialcode;
                    Console.WriteLine("Special Request Code updated successfully!");
                    break;

                case 4:

                    Console.Write("Enter new Boarding Gate: ");
                    string newGate = Console.ReadLine().ToUpper();
                    if (!t.BoardingGates.ContainsKey(newGate))
                    {
                        Console.WriteLine("Invalid boarding gate. Please enter a valid gate.");
                        return;
                    }

                    if (t.BoardingGates[newGate].Flight != null)
                    {
                        Console.WriteLine("Error: This boarding gate is already assigned to another flight.");
                        return;
                    }

                    t.BoardingGates[newGate].Flight = flight;
                    Console.WriteLine("Boarding Gate updated successfully!");
                    break;
            }
        }
        else if (option == 2) // Delete Flight
        {
            Console.Write("Are you sure you want to delete this flight? (Y/N): ");
            string confirm = Console.ReadLine().ToUpper();

            if (confirm == "Y")
            {
                t.Flights.Remove(flightNum);
                Console.WriteLine("Flight deleted successfully.");
            }
            else
            {
                Console.WriteLine("Flight deletion cancelled.");
            }
        }
        string assignedGate = "Unassigned";

        foreach (var gate in t.BoardingGates.Values)
        {
            if (gate.Flight == flight)
            {
                assignedGate = gate.GateName;
                break; // Stop searching once we find the assigned gate
            }
        }
        Console.WriteLine("\nUpdated Flight Details:");
        Console.WriteLine($"Flight Number: {flight.FlightNumber}");
        Console.WriteLine($"Airline Name: {t.GetAirlineFromFlight(flight).Name}");
        Console.WriteLine($"Origin: {flight.Origin}");
        Console.WriteLine($"Destination: {flight.Destination}");
        Console.WriteLine($"Expected Departure/Arrival Time: {flight.ExpectedTime}");
        Console.WriteLine($"Status: {flight.Status}");
        Console.WriteLine($"Special Request Code: {(flightToCode.ContainsKey(flightNum) ? flightToCode[flightNum] : "None")}");
        Console.WriteLine($"Boarding Gate: {assignedGate}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred: {ex.Message}");
    }
}
//Basic Feature 9: Display scheduled flights in chronological order, with boarding gates assignments where applicable
void DisplayFlightDetails(Terminal t)
{
    Console.WriteLine("=============================================\nFlight Schedule for Changi Airport Terminal 5\n=============================================");
    Console.WriteLine("{0,-15} {1,-21} {2,-20} {3,-18} {4,-31} {5,-9} {6,-13} {7}", "Flight Number", "Airline Name", "Origin", "Destination", "Expected Departure/Arrival Time", "Status", "Boarding Gate", "Special Request Code");
    // create list and add flights so that it can be sorted accoridng to expected time
    List<Flight> flights = new List<Flight>();
    foreach (Flight f in t.Flights.Values)
    {
        flights.Add(f);
    }
    flights.Sort();
    foreach (Flight f in flights)
    {
        string boardingGate = "Unassigned";

        // Check if the flight has an assigned boarding gate
        foreach (BoardingGate b in t.BoardingGates.Values)
        {
            if (b.Flight != null && b.Flight.FlightNumber == f.FlightNumber)
            {
                boardingGate = b.GateName;
                break; // Exit the loop once the matching gate is found
            }
        }
        Console.WriteLine("{0,-15} {1,-21} {2,-20} {3,-18} {4,-31} {5,-9} {6,-13} {7}", f.FlightNumber, t.GetAirlineFromFlight(f).Name, f.Origin, f.Destination, f.ExpectedTime, f.Status, boardingGate, flightToCode[f.FlightNumber]);
    }
}

//Advanced Feature (a)
void ProcessFlightsInBulk(Terminal t)
{
    try
    {
        Queue<Flight> unassignedFlights = new Queue<Flight>();
        List<BoardingGate> availableGates = new List<BoardingGate>();
        int initiallyAssignedFlights = 0;
        int initiallyAssignedGates = 0;

        foreach (var gate in t.BoardingGates.Values)
        {
            if (gate.Flight == null)
            {
                availableGates.Add(gate);
            }
            else
            {
                initiallyAssignedGates++;
            }
        }

        foreach (var flight in t.Flights.Values)
        {
            bool isAssigned = false;
            foreach (var gate in t.BoardingGates.Values)
            {
                if (gate.Flight == flight)
                {
                    isAssigned = true;
                    initiallyAssignedFlights++;
                    break;
                }
            }
            if (!isAssigned)
            {
                unassignedFlights.Enqueue(flight);
            }
        }

        Console.WriteLine($"Total unassigned flights: {unassignedFlights.Count}");
        Console.WriteLine($"Total available boarding gates: {availableGates.Count}");

        int assignedCount = 0;
        while (unassignedFlights.Count > 0 && availableGates.Count > 0)
        {
            Flight flight = unassignedFlights.Dequeue();
            BoardingGate assignedGate = null;

            foreach (var gate in availableGates)
            {
                if ((flight is CFFTFlight && gate.SupportsCFFT) ||
                    (flight is DDJBFlight && gate.SupportsDDJB) ||
                    (flight is LWTTFlight && gate.SupportsLWTT) ||
                    (!(flight is CFFTFlight || flight is DDJBFlight || flight is LWTTFlight)))
                {
                    assignedGate = gate;
                    break;
                }
            }

            if (assignedGate != null)
            {
                assignedGate.Flight = flight;
                availableGates.Remove(assignedGate);
                assignedCount++;
                Console.WriteLine($"Assigned Flight {flight.FlightNumber} to Gate {assignedGate.GateName}");
            }
            else
            {
                Console.WriteLine($"No suitable gate found for Flight {flight.FlightNumber}");
            }
        }

        int totalProcessedFlights = assignedCount;
        int totalProcessedGates =assignedCount;
        int totalFlights = assignedCount + initiallyAssignedFlights;
        int totalGates = assignedCount + initiallyAssignedGates;

        double percentageProcessedFlights = (double)assignedCount / totalFlights * 100;
        double percentageProcessedGates = (double)assignedCount / totalGates * 100;

        Console.WriteLine($"Total Flights Assigned: {assignedCount}");
        Console.WriteLine($"Remaining Unassigned Flights: {unassignedFlights.Count}");
        Console.WriteLine($"Total Flights and Boarding Gates Processed: {totalProcessedFlights}, {totalProcessedGates}");
        Console.WriteLine($"Percentage of Flights and Gates Processed Automatically: {percentageProcessedFlights:F2}% Flights, {percentageProcessedGates:F2}% Gates");
        Console.WriteLine();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred: {ex.Message}");
    }
}

//Advanced Feature (b)
void DisplayAirlineFees(Terminal terminal)
{
    bool allFlightsAssigned = true;

    foreach (Flight flight in terminal.Flights.Values)
    {
        bool isAssigned = false;
        foreach (BoardingGate gate in terminal.BoardingGates.Values)
        {
            if (gate.Flight == flight)
            {
                isAssigned = true;
                break;
            }
        }
        if (!isAssigned)
        {
            allFlightsAssigned = false;
            break;
        }
    }

    if (allFlightsAssigned)
    {
        terminal.PrintAirlineFees();
    }
    else
    {
        Console.WriteLine("Not all flights have been assigned a boarding gate. Please assign and try again");
        Console.WriteLine();
    }
}
