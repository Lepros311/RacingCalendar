public class Race
{
    public Race(string name, string date, string track, string driver)
    {
        Properties = new Dictionary<string, object>
        {
            { "Name", name },
            { "Date", date },
            { "Track", track },
            { "Driver", driver }
        };
    }

    public Dictionary<string, object> Properties { get; set; }

    public void AddDriver()
    {

    }
}
