using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

public class Stock
{
    // Data about a particular stock
    public string Symbol;
    public string Name;

    public Stock(string symbol, string name)
    {
        Symbol = symbol;
        Name = name;
    }


    public override bool Equals(object other)
    {
        if (this == other)
            return true;

        if (other == null ||
            GetType() != other.GetType())
            return false;

        Stock stock = (Stock)other;

        return Symbol == stock.Symbol
               && Name == stock.Name;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(
            Symbol,
            Name);
    }
}

public class Result
{
    public int Difference { get; set; }

    public string StartDate { get; set; }
    public string EndDate { get; set; }

    
}

    public class PriceRecord
{
    // Single price record
    public Stock Stock;
    public int Price;
    public string Date;

    public PriceRecord(
        Stock stock,
        int price,
        string date)
    {
        Stock = stock;
        Price = price;
        Date = date;
    }
}

public class StockCollection
{
    public List<PriceRecord>
        PriceRecords =
        new List<PriceRecord>();

    public Stock Stock;

    public StockCollection(
        Stock stock)
    {
        Stock = stock;
    }

    public int GetNumPriceRecords()
    {
        Console.WriteLine(
            "PriceRecords.Count >> "
            + PriceRecords.Count);

        return PriceRecords.Count;
    }

    public void AddPriceRecord(
        PriceRecord priceRecord)
    {
        if (!priceRecord.Stock
            .Equals(this.Stock))
        {
            throw new Exception(
              "PriceRecord Stock mismatch");
        }

        PriceRecords.Add(
            priceRecord);
    }

    public int GetMaxPrice()
    {
        if (!PriceRecords.Any())
        {
            return -1;
        }

        return PriceRecords
            .Max(record =>
                 record.Price);
    }

    public int GetMinPrice()
    {
        if (!PriceRecords.Any())
        {
            return -1;
        }

        return PriceRecords
            .Min(record =>
                 record.Price);
    }

    public double GetAvgPrice()
    {
        if (!PriceRecords.Any())
        {
            return -1;
        }

        double total =
            PriceRecords
            .Sum(record =>
                 record.Price);

        return total /
               PriceRecords.Count;
    }

    public Result GetBiggestChange()
    {

        var result = PriceRecords.OrderBy(x => x.Date).ToList();
        

        if (result.Count > 1)
        {
            Result obj = new Result();
            int biggestChage = int.MinValue;


            for (var i = 1; i < result.Count; i++)
            {
                var current = result[i].Price;

                var previous = result[i - 1].Price;

                int difference =Math.Abs(current - previous);

                if (difference > biggestChage)
                {
                    biggestChage = difference;

                    obj.Difference = current - previous;
                    obj.StartDate = result[i - 1].Date;
                    obj.EndDate = result[i].Date;
                }

            }
            return obj;
        }
        else
        {
            return null;
        }
        
    }
}
    

public class Program
{
    static void Main()
    {
        TestPriceRecord();

        TestStockCollection();
        TestGetBiggestChange();
    }

    static void TestPriceRecord()
    {
        Console.WriteLine(
        "Running TestPriceRecord");

        Stock testStock =
            new Stock(
                "AAPL",
                "Apple Inc.");

        PriceRecord testRecord =
            new PriceRecord(
                testStock,
                100,
                "2023-07-01");

        Debug.Assert(
            testRecord.Stock
            .Equals(testStock));

        Debug.Assert(
            testRecord.Price
            == 100);

        Debug.Assert(
            testRecord.Date
            == "2023-07-01");
    }


    static StockCollection
    MakeStockCollection(
    Stock stock,
    object[][] priceData)
    {
        StockCollection
            stockCollection =
            new StockCollection(
                stock);

        foreach (var data
                in priceData)
        {
            PriceRecord
                priceRecord =
                new PriceRecord(
                    stock,
                    (int)data[0],
                    (string)data[1]);

            stockCollection
                .AddPriceRecord(
                    priceRecord);
        }

        return stockCollection;
    }


    static void TestStockCollection()
    {
        Console.WriteLine(
        "Running TestStockCollection");

        Stock testStock =
            new Stock(
                "AAPL",
                "Apple Inc.");

        StockCollection
        stockCollection =
        new StockCollection(
            testStock);

        Debug.Assert(
            stockCollection
            .GetNumPriceRecords()
            == 0);

        Debug.Assert(
            stockCollection
            .GetMaxPrice()
            == -1);

        Debug.Assert(
            stockCollection
            .GetMinPrice()
            == -1);

        Debug.Assert(
            stockCollection
            .GetAvgPrice()
            == -1);


        object[][] priceData =
        {
            new object[]
            {
                110,
                "2023-06-29"
            },

            new object[]
            {
                112,
                "2023-07-01"
            },

            new object[]
            {
                90,
                "2023-06-28"
            },

            new object[]
            {
                105,
                "2023-07-06"
            }
        };

        testStock =
            new Stock(
                "AAPL",
                "Apple Inc.");

        stockCollection =
            MakeStockCollection(
                testStock,
                priceData);

        Debug.Assert(
            priceData.Length
            ==
            stockCollection
            .GetNumPriceRecords());

        Debug.Assert(
            stockCollection
            .GetMaxPrice()
            == 112);

        Debug.Assert(
            stockCollection
            .GetMinPrice()
            == 90);

        Debug.Assert(
            Math.Abs(
            stockCollection
            .GetAvgPrice()
            - 104.25)
            < 0.1);

        
    }

    public static void TestGetBiggestChange()
    {
        Console.WriteLine(
            "Running TestGetBiggestChange");

        // Test the GetBiggestChange method

        Console.WriteLine(
            "Running TestGetBiggestChange");

        

        Stock testStock =
            new Stock(
                "AAPL",
                "Apple Inc.");

        StockCollection stockCollection =
            new StockCollection(
                testStock);



        Debug.Assert(
            stockCollection
            .GetBiggestChange()
            == null);

        //    Debug.Assert(
        //        stockCollection
        //        .GetBiggestChange()
        //        == null);


        //    object[][] priceData =
        //    {
        //    new object[]
        //    {
        //        110,
        //        "2023-06-29"
        //    },

        //    new object[]
        //    {
        //        112,
        //        "2023-07-01"
        //    },

        //    new object[]
        //    {
        //        90,
        //        "2023-06-25"
        //    },

        //    new object[]
        //    {
        //        105,
        //        "2023-07-06"
        //    }
        //};

        //    stockCollection =
        //        MakeStockCollection(
        //            testStock,
        //            priceData);


        //    Result result =
        //        stockCollection
        //        .GetBiggestChange();

        //    Debug.Assert(
        //        result.Difference == 20);

        //    Debug.Assert(
        //        result.StartDate
        //        == "2023-06-25");

        //    Debug.Assert(
        //        result.EndDate
        //        == "2023-06-29");



        object[][] priceData2 =
        {
        new object[]
        {
            200,
            "2000-01-04"
        },

        new object[]
        {
            210,
            "1999-12-30"
        },

        new object[]
        {
            190,
            "2000-01-03"
        },

        new object[]
        {
            180,
            "2000-01-01"
        }
    };


        stockCollection =
            MakeStockCollection(
                testStock,
                priceData2);


        Result result2 =
            stockCollection
            .GetBiggestChange();



        Debug.Assert(
            result2.Difference
            == -30);

        Debug.Assert(
            result2.StartDate
            == "1999-12-30");

        Debug.Assert(
            result2.EndDate
            == "2000-01-01");
    }
}