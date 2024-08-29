using Spectre.Console;

var race1 = new Race("Indianapolis 500", "May 25, 2025", "Indianapolis Motor Speedway", "");
var race2 = new Race("24 Hours of Le Mans", "June 11, 2025", "Circuit de la Sarthe", "");
var race3 = new Race("Daytona 500", "February 16, 2025", "Indianapolis Motor Speedway", "");

var driver1 = new Driver("Al Unser Jr.");
var driver2 = new Driver("Ricardo Patrese");
var driver3 = new Driver("Erik Carlsson");
var driver4 = new Driver("Craig Breedlove");
var driver5 = new Driver("Louis Meyer");
var driver6 = new Driver("Rauno Aaltonen");
var driver7 = new Driver("Giuseppe Farina");
var driver8 = new Driver("Richard Burns");
var driver9 = new Driver("David Pearson");
var driver10 = new Driver("Jean Behra");
var driver11 = new Driver("Wolfgang von Trips");
var driver12 = new Driver("Henry Segrave");
var driver13 = new Driver("Gerhard Berger");
var driver14 = new Driver("David Coulthard");
var driver15 = new Driver("Carlos Reutemann");
var driver16 = new Driver("Didier Pironi");
var driver17 = new Driver("Robert Benoist");
var driver18 = new Driver("Parnelli Jones");
var driver19 = new Driver("Stefan Bellof");
var driver20 = new Driver("Keke Rosberg");
var driver21 = new Driver("Olivier Gendebien");
var driver22 = new Driver("Nico Rosberg");
var driver23 = new Driver("Luigi Fagioli");
var driver24 = new Driver("Denny Hulme");
var driver25 = new Driver("Bill Vukovich");
var driver26 = new Driver("Jo Siffert");
var driver27 = new Driver("Malcolm Campbell");
var driver28 = new Driver("Louis Chiron");
var driver29 = new Driver("Felice Nazzaro");
var driver30 = new Driver("Hermann Lang");

var drivers = new List<Driver>();

drivers.Add(driver1);
drivers.Add(driver2);
drivers.Add(driver3);
drivers.Add(driver4);
drivers.Add(driver5);
drivers.Add(driver6);
drivers.Add(driver7);
drivers.Add(driver8);
drivers.Add(driver9);
drivers.Add(driver10);
drivers.Add(driver11);
drivers.Add(driver12);
drivers.Add(driver13);
drivers.Add(driver14);
drivers.Add(driver15);
drivers.Add(driver16);
drivers.Add(driver17);
drivers.Add(driver18);
drivers.Add(driver19);
drivers.Add(driver20);
drivers.Add(driver21);
drivers.Add(driver22);
drivers.Add(driver23);
drivers.Add(driver24);
drivers.Add(driver25);
drivers.Add(driver26);
drivers.Add(driver27);
drivers.Add(driver28); 
drivers.Add(driver29);
drivers.Add(driver30);


var races = new List<Race>();

races.Add(race1);
races.Add(race2);
races.Add(race3);

var calTable = new Table();

calTable.AddColumns("Race", "Date", "Track", "Driver");

foreach (var race in races)
{
    if (race.Properties != null)
    {
        string? name = race.Properties["Name"]?.ToString();
        string? date = race.Properties["Date"]?.ToString();
        string? track = race.Properties["Track"]?.ToString();
        string? driver = race.Properties["Driver"]?.ToString();

        if (name != null && date != null && track != null && driver != null)
        {
            calTable.AddRow(name, date, track, driver);
        }
    }
}

Console.WriteLine("RACING CALENDAR:");
AnsiConsole.Write(calTable);

Table driverTable = new Table();
var id = 1;

driverTable.AddColumns("ID", "Name");

foreach (var driver in drivers)
{
        driverTable.AddRow(id, driver.Name);
        id++;
}

Console.WriteLine("DRIVERS:");
AnsiConsole.Write(driverTable);