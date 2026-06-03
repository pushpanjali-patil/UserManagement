using System;
using System.Collections.Generic;
using System.IO;

public class LogEntry
{
    private readonly float timestamp;

    private readonly string licensePlate;

    private readonly string boothType;

    private readonly int location;

    private readonly string direction;

    public LogEntry(string logLine)
    {
        string[] tokens = logLine.Split(' ');
        
        this.timestamp = float.Parse(tokens[0]);

        this.licensePlate = tokens[1];

        this.boothType = tokens[3];

        this.location =
            int.Parse(
                tokens[2].Substring(
                    0,
                    tokens[2].Length - 1));

        string directionLetter =
            tokens[2].Substring(tokens[2].Length - 1);

        if (directionLetter == "E")
        {
            this.direction = "EAST";
        }
        else if (directionLetter == "W")
        {
            this.direction = "WEST";
        }
        else
        {
            throw new ArgumentException();
        }
    }

    public float GetTimestamp()
    {
        return timestamp;
    }

    public string GetLicensePlate()
    {
        return licensePlate;
    }

    public string GetBoothType()
    {
        return boothType;
    }

    public int GetLocation()
    {
        return location;
    }

    public string GetDirection()
    {
        return direction;
    }

    public override string ToString()
    {
        return string.Format(
            "<LogEntry timestamp: {0} license: {1} location: {2} direction: {3} booth type: {4}>",
            timestamp,
            licensePlate,
            location,
            direction,
            boothType
        );
    }

}

public class LogFile
{
    public List<LogEntry> logEntries;

    public LogFile(StreamReader reader)
    {
        this.logEntries = new List<LogEntry>();

        string line = reader.ReadLine();

        while (!string.IsNullOrEmpty(line))
        {
            LogEntry logEntry =
                new LogEntry(line.Trim());

            this.logEntries.Add(logEntry);

            line = reader.ReadLine();
        }
    }

    public LogEntry Get(int index)
    {
        return this.logEntries[index];
    }

    public int Size()
    {
        return this.logEntries.Count;
    }


    public int CountJourney()
    {
        HashSet<string> hashSet = new HashSet<string>();

        foreach(var item in logEntries)
        {

            var licensePlate=item.GetLicensePlate();

           
        }

        return 0;

    }
}

public class Program
{
    static void Main(string[] args)
    {
        //TestLogEntry();

        //TestCountJourneys();

        StreamReader reader =
        new StreamReader(@"C:\\Angular\\Wecp\\ConsoleApp1\\TollBoothProject\\tollbooth_small.log");
        

        LogFile logfile = new LogFile(reader);

        Dictionary<string, LogEntry> dict = new Dictionary<string, LogEntry>();

        foreach(var  item in logfile.logEntries)
        {
           var licensePlate= item.GetLicensePlate();


            if (dict.ContainsKey(licensePlate))
            {
                LogEntry previousLog= dict[licensePlate];
                var distance = item.GetLocation()- previousLog.GetLocation();
                var time = item.GetTimestamp()-previousLog.GetTimestamp();

                var hour = time / 3600.0;

                var speed=distance / hour;

                dict[licensePlate] = item;
            }
            else
            {
                dict[licensePlate] = item;
            }

        }

        
    }

    public static void TestLogEntry()
    {
        Console.WriteLine("Running TestLogEntry");

        string logLine =
            "44776.619 KTB918 310E MAINROAD";

        LogEntry logEntry =
            new LogEntry(logLine);

        Console.WriteLine(
            logEntry.GetTimestamp());

        Console.WriteLine(
            logEntry.GetLicensePlate());

        Console.WriteLine(
            logEntry.GetLocation());

        Console.WriteLine(
            logEntry.GetDirection());

        Console.WriteLine(
            logEntry.GetBoothType());
    }

    public static void TestCountJourneys()
    {
        string path =
            "tollbooth_small.log";

        StreamReader reader =
            new StreamReader(path);

        LogFile logFile =
            new LogFile(reader);

        //logFile.CountJourneys();

        reader.Close();
    }


   





}
