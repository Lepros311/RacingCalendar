using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Diagnostics;
using ConsoleTable;
using System.Runtime.CompilerServices;

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
                if (race.Properties!["Drivers"] == currentDriver)
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

    public static void AddADriver(List<Race> races, List<Queue<Driver>> waitlists)
    {
        Console.WriteLine("\nEnter the ID of the race you want to add a driver to:");

        string[] raceIdOptions = { "1", "2", "3" };
        string? raceIdInput;
        do
        {
            raceIdInput = Console.ReadLine();
            if (!raceIdOptions.Contains(raceIdInput))
            {
                Console.WriteLine("\nThat is not a valid option. You must enter 1, 2, or 3.");
            }
        } while (!raceIdOptions.Contains(raceIdInput));

        switch (raceIdInput)
        {
            case "1":
                if (waitlists[0].ToList().All(x => x.Name == ""))
                {
                    Console.WriteLine("\nThere are no waitlisted drivers available for Race 1.");
                    AddADriver(races, waitlists);
                    break;
                }
                string driverNames1 = races[0].Properties!["Drivers"].ToString()!;
                List<string> driverNamesList1 = driverNames1.Split(',').Select(x => x.Trim()).ToList();
                if (driverNamesList1.Count == 5)
                {
                    Console.WriteLine("\nUnable to add driver. This race already has the maximum of 5 drivers.\n");
                }
                else
                {
                    Driver driverToAdd = waitlists[0].Dequeue();
                    waitlists[0].Enqueue(new Driver(""));
                    string driverNamesString = races[0].Properties!["Drivers"].ToString()!;
                    List<string> driverNamesToList = driverNames1.Split(',').Select(x => x.Trim()).ToList();
                    driverNamesList1.Add(driverToAdd.Name);

                    Console.WriteLine($"\n{driverToAdd.Name} has been added to Race 1.\n");

                    races[0].Properties!["Drivers"] = string.Join(", ", driverNamesList1);
                }
                break;
            case "2":
                if (waitlists[1].ToList().All(x => x.Name == ""))
                {
                    Console.WriteLine("\nThere are no waitlisted drivers available for Race 2.");
                    AddADriver(races, waitlists);
                    break;
                }
                string driverNames2 = races[1].Properties!["Drivers"].ToString()!;
                List<string> driverNamesList2 = driverNames2.Split(',').Select(x => x.Trim()).ToList();
                if (driverNamesList2.Count == 5)
                {
                    Console.WriteLine("\nUnable to add driver. This race already has the maximum of 5 drivers.\n");
                }
                else
                {
                    Driver driverToAdd = waitlists[1].Dequeue();
                    waitlists[1].Enqueue(new Driver(""));
                    string driverNamesString = races[1].Properties!["Drivers"].ToString()!;
                    List<string> driverNamesToList = driverNames2.Split(',').Select(x => x.Trim()).ToList();
                    driverNamesList2.Add(driverToAdd.Name);

                    Console.WriteLine($"\n{driverToAdd.Name} has been added to Race 2.\n");

                    races[1].Properties!["Drivers"] = string.Join(", ", driverNamesList2);
                }
                break;
            case "3":
                if (waitlists[2].ToList().All(x => x.Name == ""))
                {
                    Console.WriteLine("\nThere are no waitlisted drivers available for Race 3.");
                    AddADriver(races, waitlists);
                    break;
                }
                string driverNames3 = races[2].Properties!["Drivers"].ToString()!;
                List<string> driverNamesList3 = driverNames3.Split(',').Select(x => x.Trim()).ToList();
                if (driverNamesList3.Count == 5)
                {
                    Console.WriteLine("\nUnable to add driver. This race already has the maximum of 5 drivers.\n");
                }
                else
                {
                    Driver driverToAdd = waitlists[2].Dequeue();
                    waitlists[2].Enqueue(new Driver(""));
                    string driverNamesString = races[2].Properties!["Drivers"].ToString()!;
                    List<string> driverNamesToList = driverNames3.Split(',').Select(x => x.Trim()).ToList();
                    driverNamesList3.Add(driverToAdd.Name);

                    Console.WriteLine($"\n{driverToAdd.Name} has been added to Race 3.\n");

                    races[2].Properties!["Drivers"] = string.Join(", ", driverNamesList3);
                }
                break;
        }
    }

    public static void RemoveADriver(List<Race> races, Spectre.Console.Table calTable)
    {
        Console.WriteLine("\nEnter the ID of the race you want to remove a driver from:");

        string[] raceIdOptions = { "1", "2", "3" };
        string? raceIdInput;
        do
        {
            raceIdInput = Console.ReadLine();
            if (!raceIdOptions.Contains(raceIdInput))
            {
                Console.WriteLine("\nThat is not a valid option. You must enter 1, 2, or 3.\n");
            }
        } while (!raceIdOptions.Contains(raceIdInput));

        string? driverName;
        TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

        switch (raceIdInput)
        {
            case "1":
                Console.WriteLine("\nEnter the name of the driver you want to remove from Race 1:");
                driverName = Console.ReadLine();
                driverName = driverName!.Trim();
                driverName = textInfo.ToTitleCase(driverName);
                if (races[0].Properties!["Drivers"].ToString()!.Split(new char[] { ',' }, StringSplitOptions.None).Any(name => name.Trim().Equals(driverName, StringComparison.OrdinalIgnoreCase)))
                {
                    string driverNames = races[0].Properties!["Drivers"].ToString()!;
                    List<string> driverNamesList = driverNames.Split(',').Select(x => x.Trim()).ToList();
                    driverNamesList.Remove(driverName);
                    
                    Console.WriteLine($"\n{driverName} has been removed from Race 1.\n");

                    races[0].Properties!["Drivers"] = string.Join(", ", driverNamesList);
                }
                else
                {
                    Console.WriteLine($"\n{driverName} is not listed in this race.\n");
                    RemoveADriver(races, calTable);
                    break;
                }
                break;
            case "2":
                Console.WriteLine("\nEnter the name of the driver you want to remove from Race 2:");
                driverName = Console.ReadLine();
                driverName = driverName!.Trim();
                driverName = textInfo.ToTitleCase(driverName);
                if (races[1].Properties!["Drivers"].ToString()!.Split(new char[] { ',' }, StringSplitOptions.None).Any(name => name.Trim().Equals(driverName, StringComparison.OrdinalIgnoreCase)))
                {
                    string driverNames = races[1].Properties!["Drivers"].ToString()!;
                    List<string> driverNamesList = driverNames.Split(',').Select(x => x.Trim()).ToList();
                    driverNamesList.Remove(driverName);

                    Console.WriteLine($"\n{driverName} has been removed from Race 2.\n");

                    races[1].Properties!["Drivers"] = string.Join(", ", driverNamesList);
                }
                else
                {
                    Console.WriteLine($"\n{driverName} is not listed in this race.\n");
                    RemoveADriver(races, calTable);
                    break;
                }
                break;
            case "3":
                Console.WriteLine("\nEnter the name of the driver you want to remove from Race 3:");
                driverName = Console.ReadLine();
                driverName = driverName!.Trim();
                driverName = textInfo.ToTitleCase(driverName);
                if (races[2].Properties!["Drivers"].ToString()!.Split(new char[] { ',' }, StringSplitOptions.None).Any(name => name.Trim().Equals(driverName, StringComparison.OrdinalIgnoreCase)))
                {
                    string driverNames = races[2].Properties!["Drivers"].ToString()!;
                    List<string> driverNamesList = driverNames.Split(',').Select(x => x.Trim()).ToList();
                    driverNamesList.Remove(driverName);

                    Console.WriteLine($"\n{driverName} has been removed from Race 3.\n");

                    races[2].Properties!["Drivers"] = string.Join(", ", driverNamesList);
                }
                else
                {
                    Console.WriteLine($"\n{driverName} is not listed in this race.\n");
                    RemoveADriver(races, calTable);
                    break;
                }
                break;
        }
    }

    
}


