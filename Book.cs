//for story choices problem
using System;
using System.Collections.Generic;
 
public class Choice
{
    public int Page { get; set; }
    public int Option1 { get; set; }
    public int Option2 { get; set; }
 
    public Choice(int page, int option1, int option2)
    {
        Page = page;
        Option1 = option1;
        Option2 = option2;
    }
}
 
public class StorySolver
{
    public static int Stories(List<int> endings, List<Choice> choices, int option)
    {
        // Store endings for fast lookup
        HashSet<int> endingSet = new HashSet<int>(endings);
 
        // Store choices in dictionary for easy access
        Dictionary<int, Choice> choiceMap = new Dictionary<int, Choice>();
 
        foreach (var choice in choices)
        {
            choiceMap[choice.Page] = choice;
        }
 
        // Track visited pages to detect loops
        HashSet<int> visited = new HashSet<int>();
 
        int currentPage = 1;
 
        while (true)
        {
            // ✅ If ending page → return it
            if (endingSet.Contains(currentPage))
            {
                return currentPage;
            }
 
            // ✅ If already visited → loop
            if (visited.Contains(currentPage))
            {
                return -1;
            }
 
            visited.Add(currentPage);
 
            // ✅ If page has a choice
            if (choiceMap.ContainsKey(currentPage))
            {
                Choice ch = choiceMap[currentPage];
 
                // Choose option
                if (option == 1)
                {
                    currentPage = ch.Option1;
                }
                else
                {
                    currentPage = ch.Option2;
                }
            }
            else
            {
                // Normal page → go next
                currentPage++;
            }
        }
    }
 
    public static void Main()
    {
        var endings = new List<int> { 6, 15, 21, 30 };
 
        var choices = new List<Choice>
        {
            new Choice(3, 7, 8),
            new Choice(9, 4, 2)
        };
 
        Console.WriteLine(Stories(endings, choices, 1)); // 6
        Console.WriteLine(Stories(endings, choices, 2)); // -1
    }
}
