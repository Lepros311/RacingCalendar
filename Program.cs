using Spectre.Console;
using System;
using System.Text;
using System.Collections.Generic;

var allDrivers = new List<Driver>();
var races = new List<Race>();
var waitlists = new List<Queue<Driver>>();
List<string> tempDriversList = new List<string>();
string? userMenuChoice;
bool exit = false;
var calTable = new Table();

void CreateDrivers()
{
    allDrivers.Add(new Driver("Richard Petty"));
    allDrivers.Add(new Driver("Ricardo Patrese"));
    allDrivers.Add(new Driver("Erik Carlsson"));
    allDrivers.Add(new Driver("Craig Breedlove"));
    allDrivers.Add(new Driver("Louis Meyer"));
    allDrivers.Add(new Driver("Rauno Aaltonen"));
    allDrivers.Add(new Driver("Giuseppe Farina"));
    allDrivers.Add(new Driver("Richard Burns"));
    allDrivers.Add(new Driver("David Pearson"));
    allDrivers.Add(new Driver("Jean Behra"));
    allDrivers.Add(new Driver("Dale Earnhardt"));
    allDrivers.Add(new Driver("Henry Segrave"));
    allDrivers.Add(new Driver("Gerhard Berger"));
    allDrivers.Add(new Driver("David Coulthard"));
    allDrivers.Add(new Driver("Carlos Reutemann"));
    allDrivers.Add(new Driver("Didier Pironi"));
    allDrivers.Add(new Driver("Robert Benoist"));
    allDrivers.Add(new Driver("Parnelli Jones"));
    allDrivers.Add(new Driver("Stefan Bellof"));
    allDrivers.Add(new Driver("Keke Rosberg"));
    allDrivers.Add(new Driver("Olivier Gendebien"));
    allDrivers.Add(new Driver("Nico Rosberg"));
    allDrivers.Add(new Driver("Luigi Fagioli"));
    allDrivers.Add(new Driver("Denny Hulme"));
    allDrivers.Add(new Driver("Bill Vukovich"));
    allDrivers.Add(new Driver("Jo Siffert"));
    allDrivers.Add(new Driver("Malcolm Campbell"));
    allDrivers.Add(new Driver("Louis Chiron"));
    allDrivers.Add(new Driver("Felice Nazzaro"));
    allDrivers.Add(new Driver("Hermann Lang"));
}

void CreateRaces()
{
    races.Add(new Race("1", "Indianapolis 500", "May 25, 2025", "Indianapolis Motor Speedway"));
    races.Add(new Race("2", "24 Hours of Le Mans", "June 11, 2025", "Circuit de la Sarthe"));
    races.Add(new Race("3", "Daytona 500", "February 16, 2025", "Indianapolis Motor Speedway"));
}

void CreateWaitlists()
{
    waitlists.Add(new Queue<Driver>());
    waitlists.Add(new Queue<Driver>());
    waitlists.Add(new Queue<Driver>());
}

void CreateCalTable()
{
    calTable.Rows.Clear();

    if (calTable.Columns.Count <= 0)
    {
        calTable.AddColumns("ID", "Race", "Date", "Track", "Drivers (Max 5 per race)");
    }

    foreach (var race in races)
    {
        if (race.Properties != null)
        {
            string? raceId = race.Properties["ID"]?.ToString();
            string? name = race.Properties["Name"]?.ToString();
            string? date = race.Properties["Date"]?.ToString();
            string? track = race.Properties["Track"]?.ToString();
            string? drivers = race.Properties["Drivers"]?.ToString();

            if (raceId != null && name != null && date != null && track != null && drivers != null)
            {
                calTable.AddRow(raceId, name, date, track, drivers);
            }
        }
    }

    Console.WriteLine("RACING CALENDAR:");
    AnsiConsole.Write(calTable);
}

void CreateDriverTable()
{
    Table driverTable = new Table();
    var driverId = 1;

    driverTable.AddColumns("ID", "Name");

    foreach (var driver in allDrivers)
    {
        driverTable.AddRow(driverId.ToString(), driver.Name);
        driverId++;
    }

    Console.WriteLine("AVAILABLE DRIVERS:");
    AnsiConsole.Write(driverTable);
}

CreateDrivers();
CreateRaces();
CreateCalTable();
CreateDriverTable();
CreateWaitlists();

Console.WriteLine("Press any key to randomly assign drivers to each race...");
Console.ReadKey();
Console.Clear();

Race.AddDrivers(races, allDrivers, waitlists, tempDriversList);

CreateCalTable();

Race.PopulateWaitlists(allDrivers, waitlists, tempDriversList);


void CreateWaitlistTable()
{
    var waitlistTable = new Table();
    var raceNum = 0;

    foreach (var race in races)
    {
        waitlistTable.AddColumns($"Race {raceNum + 1} Waitlisted Drivers");
        raceNum++;
    }

    raceNum = 0;
    for (int i = 0; i < 5; i++) 
    {
        waitlistTable.AddRow(waitlists[raceNum].ElementAt(i).Name, waitlists[raceNum+1].ElementAt(i).Name, waitlists[raceNum+2].ElementAt(i).Name);
        
    }
    

    Console.WriteLine("WAITLIST DRIVERS:");
    AnsiConsole.Write(waitlistTable);
}

CreateWaitlistTable();

void PrintMenu()
{
    Console.WriteLine("\nMAIN MENU");
    Console.WriteLine("1) Add a Driver to a Race");
    Console.WriteLine("2) Remove a Driver from a Race");
    Console.WriteLine("3) Exit program");
}

void GetUserMenuSelection()
{
    string[] menuOptions = { "1", "2", "3" };
    do
    {
        Console.WriteLine("\nWhat would you like to do? Enter 1, 2, or 3:");
        userMenuChoice = Console.ReadLine();
        if (!menuOptions.Contains(userMenuChoice))
        {
            Console.WriteLine("\nThat is not a valid option. You must enter 1, 2, or 3.");
        }
    } while (!menuOptions.Contains(userMenuChoice));
}

void MenuSelectionControl(string userMenuChoice)
{
    switch (userMenuChoice)
    {
        case "1":
            Console.Clear();
            CreateCalTable();
            CreateWaitlistTable();
            Race.AddADriver(races, waitlists);
            CreateCalTable();
            break;
        case "2":
            Console.Clear();
            CreateCalTable();
            Race.RemoveADriver(races, calTable);
            CreateCalTable();
            break;
        case "3":
            Console.WriteLine("\nOK, bye!");
            exit = true;
            Environment.Exit(0);
            break;
    }
}

do
{
    PrintMenu();
    GetUserMenuSelection();
    MenuSelectionControl(userMenuChoice!);
} while (exit == false);

