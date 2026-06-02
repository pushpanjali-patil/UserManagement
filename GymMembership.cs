// See https://aka.ms/new-console-template for more information

using System.Collections;
using System.Diagnostics.Contracts;
using System.Linq;

public class CastleRooms
{
    public static List<String> filterRooms((string Source,string Dest)[] instructions, List<String> treasureRooms)
    {



        Dictionary<string, int> dict = new Dictionary<string, int>();

        Dictionary<string, string> nextRooms = new Dictionary<string, string>();

      
        foreach (var item in instructions)
        {
            string Source = item.Source;
            string Destination = item.Dest;
            if (Source != Destination)
            {
                if (dict.ContainsKey(Destination))
                {

                    dict[Destination]++;


                }
                else
                {
                    dict[Destination] = 1;
                }
            }
            if (!dict.ContainsKey(Source))
            {
                dict[Source] = 0;
            }
            nextRooms[Source] = Destination;

        }
        List<string> result = new List<string>();

        foreach (var item in dict)
        {



            if (item.Value >= 2 && treasureRooms.Contains(nextRooms[item.Key]))
            {
                result.Add(item.Key);
            }
        }

        return result;

    }

    public static void Main(string[] args)
    {

        (string Source, string Destination)[] instructions_1 =
                    {
                    ("jasmin", "tulip"),
                    ("lily", "tulip"),
                    ("tulip", "tulip"),
                    ("rose", "rose"),
                    ("violet", "rose"),
                    ("sunflower", "violet"),
                    ("daisy", "violet"),
                    ("iris", "violet")

                    };


        //List<string> treasureRoom1 = new()
        //{
        //    "lily",
        //    "tulip",
        //    "violet",
        //    "rose"
        //};

        List<string> treasureRoom2 = new()
        {
            "lily",
            "jasmin",
            "violet"

        };

        //var results1 = filterRooms(instructions_1, treasureRoom1);

        //foreach (var res in results1)
        //{
        //    Console.WriteLine(res);
        //}


        var results2 = filterRooms(instructions_1, treasureRoom2);

        foreach (var res in results2)
        {
            Console.WriteLine(res);
        }



        //(string Source, string Destination)[] instructions_2 =
        //            {
        //            ("jasmin", "tulip"),
        //            ("lily", "tulip"),
        //            ("tulip", "violet"),
        //            ("violet", "violet"),
                   

        //            };

        //List<string> treasureRoom3 = new()
        //{
        //    "violet"
        //};

        //var results3 = filterRooms(instructions_2, treasureRoom3);

        //foreach (var res in results3)
        //{
        //    Console.WriteLine(res);
        //}



        Console.ReadLine();
    }
}




