using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Diagnostics;
using ConsoleTable;

public class Race
{
    public Race(string raceId, string name, string date, string track)
    {
        Properties = new Dictionary<string, object>
        {
            { "ID", raceId },
            { "Name", name },
            { "Date", date },
            { "Track", track },
            { "Drivers", string.Empty }
        };
    }

    public Dictionary<string, object> Properties { get; set; }

    public static void AddDrivers(List<Race> races, List<Driver> allDrivers, List<Queue<Driver>> waitlists, List<string> tempDriversList)
    {
        var random = new Random();
        Driver? currentDriver = null;
        int currentDriverIndex = 0;

        foreach (var race in races)
        {
            var driverNames = new StringBuilder();

            for (int i = 0; i < 5; i++)
            {
                if (race.Properties["Drivers"] == currentDriver)
                {
                    i--;
                    continue;
                }
                currentDriver = allDrivers[random.Next(allDrivers.Count)];
                currentDriverIndex = allDrivers.IndexOf(currentDriver);
                tempDriversList.Add(currentDriver.Name);
                allDrivers.RemoveAt(currentDriverIndex);
                driverNames.Append(currentDriver.Name);
                driverNames.Append(", ");
            }

            race.Properties["Drivers"] = driverNames.ToString().TrimEnd(',', ' ');
        }  
    }

    public static void PopulateWaitlists(List<Driver> allDrivers, List<Queue<Driver>> waitlists, List<string> tempDriversList)
    {
        var raceNum = 0;
        foreach (var driver in allDrivers)
        {
            if (!tempDriversList.Contains(driver.Name))
            {
                if (waitlists[raceNum].Count < 5)
                {
                    waitlists[raceNum].Enqueue(driver);
                }
                else if (waitlists[raceNum + 1].Count < 5)
                {
                    waitlists[raceNum + 1].Enqueue(driver);
                }
                else
                {
                    waitlists[raceNum + 2].Enqueue(driver);
                }
            }
        }
    }

    public static void AddADriver()
    {

    }

    public static void RemoveADriver(List<Race> races, Spectre.Console.Table calTable)
    {
        Console.WriteLine("Enter the ID of the race you want to remove a driver from:");

        string[] raceIdOptions = { "1", "2", "3" };
        string? raceIdInput;
        do
        {
            raceIdInput = Console.ReadLine();
            if (!raceIdOptions.Contains(raceIdInput))
            {
                Console.WriteLine("That is not a valid option. You must enter 1, 2, or 3.");
            }
        } while (!raceIdOptions.Contains(raceIdInput));

        string? driverName;
        TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

        switch (raceIdInput)
        {
            case "1":
                Console.WriteLine("Enter the name of the driver you want to remove from Race 1");
                driverName = Console.ReadLine();
                driverName = driverName!.Trim();
                driverName = textInfo.ToTitleCase(driverName);
                if (races[0].Properties["Drivers"].ToString()!.Split(new char[] { ',' }, StringSplitOptions.None).Any(name => name.Trim().Equals(driverName, StringComparison.OrdinalIgnoreCase)))
                {
                    string driverNames = races[0].Properties["Drivers"].ToString();
                    races[0].Properties["Drivers"] = "";
                    Console.WriteLine($"{driverName} has been removed.");


                    
                    driverNames = races[0].Properties["Drivers"].ToString();

                    foreach (var property in races[0].Properties)
                    {
                        if (races[0].Properties["Drivers"].ToString() != driverName)
                        {
                            driverNames?.Append(driverName);
                            driverNames?.Append(", ");
                        }
                    }

                    races[0].Properties["Drivers"] = driverNames.ToString().TrimEnd(',', ' ');
                }
                else
                {
                    Console.WriteLine($"{driverName} is not listed in this race.");
                }
                break;
            case "2":
                
                break;
            case "3":
                
                break;
        }
    }

    
}


