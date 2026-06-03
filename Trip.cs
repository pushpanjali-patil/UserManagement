using System;
using System.Collections.Generic;
using System.Linq;
 
public class Solution
{
    public static int OptimizeShoppingTrip(
        List<List<string>> products,
        List<string> shoppingList)
    {
        // Product -> Department mapping
        Dictionary<string, string> productDepartment = new Dictionary<string, string>();
 
        foreach (var product in products)
        {
            productDepartment[product[0]] = product[1];
        }
 
        int originalVisits = 0;
        string previousDepartment = null;
 
        HashSet<string> uniqueDepartments = new HashSet<string>();
 
        foreach (string item in shoppingList)
        {
            string department = productDepartment[item];
 
            uniqueDepartments.Add(department);
 
            if (previousDepartment == null || previousDepartment != department)
            {
                originalVisits++;
            }
 
            previousDepartment = department;
        }
 
        int optimizedVisits = uniqueDepartments.Count;
 
        return originalVisits - optimizedVisits;
    }
 
    public static void Main()
    {
        var products = new List<List<string>>
        {
            new List<string> { "Cheese", "Dairy" },
            new List<string> { "Carrots", "Produce" },
            new List<string> { "Potatoes", "Produce" },
            new List<string> { "Canned Tuna", "Pantry" },
            new List<string> { "Romaine Lettuce", "Produce" },
            new List<string> { "Chocolate Milk", "Dairy" },
            new List<string> { "Flour", "Pantry" },
            new List<string> { "Iceberg Lettuce", "Produce" },
            new List<string> { "Coffee", "Pantry" },
            new List<string> { "Pasta", "Pantry" },
            new List<string> { "Milk", "Dairy" },
            new List<string> { "Blueberries", "Produce" },
            new List<string> { "Pasta Sauce", "Pantry" }
        };
 
        var shoppingList = new List<string>
        {
            "Blueberries",
            "Milk",
            "Coffee",
            "Flour",
            "Cheese",
            "Carrots"
        };
 
        Console.WriteLine(OptimizeShoppingTrip(products, shoppingList));
    }
}
 
