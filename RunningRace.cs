using System;
using System.Collections.Generic;
using System.Linq;

public class Course
{
    // Data about a particular course
    public string Title;

    public int ObstacleCount;

    public Course(string courseTitle, int obstacles)
    {
        Title = courseTitle;
        ObstacleCount = obstacles;
    }

    public override bool Equals(object obj)
    {
        if (obj is not Course c)
        {
            return false;
        }

        return c.Title == this.Title &&
               c.ObstacleCount == this.ObstacleCount;
    }

    public override int GetHashCode()
    {
        return (Title == null ? 0 : Title.GetHashCode()) * ObstacleCount;
    }
}

public class Run
{
    // Data and methods about a single run

    public Course Course;

    public bool Complete;

    public List<int> ObstacleTimes;

    public Run(Course runCourse)
    {
        Course = runCourse;

        Complete = false;

        ObstacleTimes = new List<int>();
    }

    public void AddObstacleTime(int obstacleTime)
    {
        if (Complete)
        {
            throw new InvalidOperationException(
                "Cannot add obstacle to complete run");
        }

        ObstacleTimes.Add(obstacleTime);

        if (ObstacleTimes.Count == Course.ObstacleCount)
        {
            Complete = true;
        }
    }

    public int GetRunTime()
    {
        return ObstacleTimes.Sum();
    }
}

public class RunCollection
{
    public Course Course;

    public List<Run> Runs;

    public RunCollection(Course collectionCourse)
    {
        Course = collectionCourse;

        Runs = new List<Run>();
    }

    public int GetNumRuns()
    {
        return Runs.Count;
    }

    public void AddRun(Run run)
    {
        if (!run.Course.Equals(Course))
        {
            throw new ArgumentException(
                "Run course does not match collection course");
        }

        Runs.Add(run);
    }

    public int PersonalBest()
    {
        return Runs
            .Where(run => run.Complete)
            .Select(run => run.GetRunTime())
            .DefaultIfEmpty(int.MaxValue)
            .Min();
    }

    public int BestOfBests()
    {
        if(Course==null)
        {
            return 0;
        }
        int ObstacleCount = Course.ObstacleCount;
        int[] bestofTimes=new int[ObstacleCount];

        for(int i=0; i< bestofTimes.Length; i++)
        {
            bestofTimes[i] = int.MaxValue;
        }

        foreach(var run in Runs)
        {
            int obstacleCount = Course.ObstacleCount;

            for(int i = 0; i < run.ObstacleTimes.Count; i++)
            {
                bestofTimes[i] = Math.Min(run.ObstacleTimes[i], bestofTimes[i]);
            }
            
        }
        int sum = 0;
        foreach (var best in bestofTimes)
        {
            sum=sum+best;
        }
        return sum;
    }
}

public class Program
{
    static void Main(string[] args)
    {
        //TestRun();

        TestRunCollection();
    }

    public static void TestRun()
    {
        Console.WriteLine("Running TestRun");

        Course testCourse =
            new Course("Test course", 2);

        Run testRun =
            new Run(testCourse);

        testRun.AddObstacleTime(3);

        Console.WriteLine(
            "Run Complete: " + testRun.Complete);

        testRun.AddObstacleTime(5);

        Console.WriteLine(
            "Run Complete: " + testRun.Complete);

        Console.WriteLine(
            "Run Time: " + testRun.GetRunTime());

        try
        {
            testRun.AddObstacleTime(4);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public static RunCollection MakeRunCollection(
        Course course,
        int[][] obstacleData)
    {
        RunCollection runCollection =
            new RunCollection(course);

        foreach (int[] runData in obstacleData)
        {
            Run run = new Run(course);

            foreach (int obstacleTime in runData)
            {
                run.AddObstacleTime(obstacleTime);
            }

            runCollection.AddRun(run);
        }

        
        return runCollection;
    }

    public static void TestRunCollection()
    {
        //Console.WriteLine(
        //    "Running TestRunCollection");

        int[][] obstacleData =
        {
            new int[] {3,4,5,6},
            new int[] {4,4,4,5},
            new int[] {4,5,4,6},
            new int[] {5,5,3}
        };

        Course testCourse =
            new Course("Test course", 4);

        RunCollection runCollection =
            MakeRunCollection(
                testCourse,
                obstacleData);

        Console.WriteLine(
            "Number of Runs: " +
            runCollection.GetNumRuns());

        Console.WriteLine(
            "Personal Best: " +
            runCollection.PersonalBest());

        Console.WriteLine(
            "Best Of Bests: " +
            runCollection.BestOfBests());
    }
}
