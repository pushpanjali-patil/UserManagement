
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

public enum MembershipStatus
{
    /*
        Membership Status is of three types: BRONZE, SILVER and GOLD.
        BRONZE is the default membership a new member gets.
        SILVER and GOLD are paid memberships for the gym.
    */
    BRONZE = 1,
    SILVER = 2,
    GOLD = 3
}

public class Member
{
    /* Data about a gym member. */
    public int MemberId { get; set; }
    public string Name { get; set; }

    public List<WorkOut> WorkOuts { get; set; }
    public MembershipStatus MembershipStatus { get; set; }

    public Member(int memberId, string name, MembershipStatus membershipStatus)
    {
        MemberId = memberId;
        Name = name;
        MembershipStatus = membershipStatus;

        WorkOuts = new List<WorkOut>();
    }

    public override string ToString()
    {
        return $"Member ID: {MemberId}, Name: {Name}, Membership Status: {MembershipStatus}";
    }
}

public class Membership
{
    /*
        Data for managing a gym membership, and methods which staff can
        use to perform any queries or updates.
    */
    private List<Member> members;

    public Membership()
    {
        members = new List<Member>();
    }

    public void AddMember(Member member)
    {
        members.Add(member);
    }

    public void UpdateMembership(int memberId, MembershipStatus membershipStatus)
    {
        Member memberToUpdate =
            members.Find(member => member.MemberId == memberId) ?? throw new ArgumentException();
        if (memberToUpdate != null)
        {
            memberToUpdate.MembershipStatus = membershipStatus;
        }
    }

    public Dictionary<string, double> GetMembershipStatistics()
    {
        int totalMembers = members.Count;
        int totalPaidMembers = members.Count(member =>
        member.MembershipStatus == MembershipStatus.GOLD
        || member.MembershipStatus == MembershipStatus.SILVER);
        double conversionRate = (double)totalPaidMembers / totalMembers * 100;



        return new Dictionary<string, double>
        {
            { "total_members", totalMembers },
            { "total_paid_members", totalPaidMembers },
            { "conversion_rate", conversionRate }
        };
    }

    public void AddWorkOut(int memberId, WorkOut workOut)
    {
        Member memberToUpdate = members.Find(member => member.MemberId == memberId);

        Console.WriteLine(
    "memberToUpdate = "
    + memberToUpdate);

        Console.WriteLine(
    "memberToUpdate = "
    + memberToUpdate?.WorkOuts);

        //Console.ReadLine();

        if (memberToUpdate != null)
        {
            memberToUpdate.WorkOuts.Add(workOut);
        }

    }

    public Dictionary<int, double> getAverageWorkoutDurations()
    {
        Dictionary<int ,double> dict = new Dictionary<int ,double>();

        foreach (var member in members)
        {

            int TotalWorkOutCount = member.WorkOuts.Count;
            double TotalWorkOutHours = 0;

            foreach (var workout in member.WorkOuts)
            {
                if (TotalWorkOutCount > 0)
                {
                    TotalWorkOutHours+= workout.EndTime-workout.StartTime;
            }
            }
          double avgWorkOutHours= TotalWorkOutHours / TotalWorkOutCount;

            dict[member.MemberId] = avgWorkOutHours;

        }
        return dict;

    }
}

public class WorkOut
{
    public int UniqueId { get; set; }
    public int StartTime { get; set; }

    public int EndTime { get; set; }

    public WorkOut(int UniqueId,int StartTime,int EndTime)
    {
        this.UniqueId= UniqueId;
        this.StartTime= StartTime;
        this.EndTime = EndTime;
    }


}

public class TestSuite
{
    /*
        This is not a complete test suite, but tests some basic functionality of
        the code and shows how to use it.
    */
    public static void Main()
    {
        TestMember();
        TestMembership();

        TestGetAverageWorkoutDurations();
    }

    public static void TestMember()
    {
        Console.WriteLine("Running TestMember");
        Member testMember = new Member(1, "John Doe", MembershipStatus.BRONZE);
        Debug.Assert(testMember.MemberId == 1);
        Debug.Assert(testMember.Name == "John Doe");
        Debug.Assert(testMember.MembershipStatus == MembershipStatus.BRONZE);
    }

    public static void TestMembership()
    {
        Console.WriteLine("Running TestMembership");
        Membership testMembership = new Membership();
        Member testMember = new Member(1, "John Doe", MembershipStatus.BRONZE);
        testMembership.AddMember(testMember);
        Debug.Assert(testMembership.GetMembershipStatistics()["total_members"] == 1);

        testMembership.UpdateMembership(1, MembershipStatus.SILVER);
        Debug.Assert(testMembership.GetMembershipStatistics()["total_paid_members"] == 1);



        Member testMember2 = new Member(2, "Alex C", MembershipStatus.BRONZE);
        testMembership.AddMember(testMember2);

        Member testMember3 = new Member(3, "Marie C", MembershipStatus.GOLD);
        testMembership.AddMember(testMember3);

        Member testMember4 = new Member(4, "Joe D", MembershipStatus.SILVER);
        testMembership.AddMember(testMember4);

        Member testMember5 = new Member(5, "June R", MembershipStatus.BRONZE);
        testMembership.AddMember(testMember5);

        Member testMember6 = new Member(6, "Westley D", MembershipStatus.SILVER);
        testMembership.AddMember(testMember6);

        Dictionary<string, double> attendanceStats = testMembership.GetMembershipStatistics();
        Debug.Assert(attendanceStats["total_members"] == 6);
        Debug.Assert(attendanceStats["total_paid_members"] == 4);
        Debug.Assert(Math.Abs(attendanceStats["conversion_rate"] - 66.67) < 0.1);
    }

    public static void TestGetAverageWorkoutDurations()
    {
        Console.WriteLine(
            "Running TestGetAverageWorkoutDurations");

        Membership testMembership =
            new Membership();

        Member testMember1 =
            new Member(
                12,
                "John Doe",
                MembershipStatus.SILVER);

        testMembership.AddMember(
            testMember1);


        Member testMember2 =
            new Member(
                22,
                "Alex Cleeve",
                MembershipStatus.BRONZE);

        testMembership.AddMember(
            testMember2);


        Member testMember3 =
            new Member(
                31,
                "Marie Cardiff",
                MembershipStatus.GOLD);

        testMembership.AddMember(
            testMember3);


        Member testMember4 =
            new Member(
                37,
                "George Costanza",
                MembershipStatus.SILVER);

        testMembership.AddMember(
            testMember4);


        WorkOut testWorkout1 =
            new WorkOut(11, 10, 20);

        WorkOut testWorkout2 =
            new WorkOut(24, 15, 35);

        WorkOut testWorkOut3 =
            new WorkOut(32, 45, 90);

        WorkOut testWorkOut4 =
            new WorkOut(47, 100, 155);

        WorkOut testWorkOut5 =
            new WorkOut(56, 120, 200);

        WorkOut testWorkOut6 =
            new WorkOut(62, 300, 400);

        WorkOut testWorkOut7 =
            new WorkOut(78, 1000, 1010);

        WorkOut testWorkOut8 =
            new WorkOut(80, 1010, 1045);


        testMembership.AddWorkOut(
            12, testWorkout1);

        testMembership.AddWorkOut(
            22, testWorkout2);

        testMembership.AddWorkOut(
            31, testWorkOut3);

        testMembership.AddWorkOut(
            12, testWorkOut4);

        testMembership.AddWorkOut(
            22, testWorkOut5);

        testMembership.AddWorkOut(
            31, testWorkOut6);

        testMembership.AddWorkOut(
            12, testWorkOut7);

        testMembership.AddWorkOut(
            4, testWorkOut8);


        Dictionary<int, double>
            averageDurations =
            testMembership
            .getAverageWorkoutDurations();


        Debug.Assert(
            Math.Abs(
            averageDurations[12]
            - 25.0) < 0.1,
            "Average duration for member 12 should be 25");


        Debug.Assert(
            Math.Abs(
            averageDurations[22]
            - 50.0) < 0.1,
            "Average duration for member 22 should be 50");


        Debug.Assert(
            Math.Abs(
            averageDurations[31]
            - 72.5) < 0.1,
            "Average duration for member 31 should be 72.5");


        Debug.Assert(
            !averageDurations.ContainsKey(4));
    }
}
