// See https://aka.ms/new-console-template for more information

using System;



public class Result
{
    public List<int> _rows { get; set; }

    public List<int> _cols { get; set; }

    public Result(List<int> rows, List<int> cols)
    {
        _rows = rows;
        _cols = cols;
    }
}
public class SnakeLanes
{

    static void Main()
    {
        char[][] board1 =
        {
            new char[] { '+', '+', '+', '0', '+', '0', '0' },
             new char[] {'0', '0', '+', '0', '0', '0', '0' },
              new char[] { '0', '0', '0', '0', '+', '0', '0' },
               new char[] { '+', '+', '+', '0', '0', '+', '0' },
                new char[] { '0', '0', '0', '0', '0', '0', '0' },

        };


        Result result1=FindPassable(board1 );
        
        Console.WriteLine( "Passable rows position of board1:" + string.Join(",",result1._rows) );

        Console.WriteLine("Passable columns position  of board1:" + string.Join(",", result1._cols));

        char[][] board2 =
        {
            new char[] {'+', '+', '+', '0', '+', '0', '0'},
            new char[] { '0', '0', '0', '0', '0', '+', '0' },
            new char[] { '0', '0', '+', '0', '0', '0', '0' },
            new char[] { '0', '0', '0', '0', '+', '0', '0' },
            new char[] { '+', '+', '+', '0', '0', '0', '+' },
        };


        Result result2 = FindPassable(board2);

        Console.WriteLine("Passable rows position of board2:" + string.Join(",", result2._rows));

        Console.WriteLine("Passable columns position  of board2:" + string.Join(",", result2._cols));



        char[][] board3 =
        {
            new char[] {'+', '+', '+', '0', '+', '0', '0'},
            new char[] { '0', '0', '0', '0', '0', '0', '0' },
            new char[] { '0', '0', '+', '+', '0', '+', '0'},
            new char[] { '0', '0', '0', '0', '+', '0', '0'},
            new char[] {'+', '+', '+', '0', '0', '0', '+' },
        };


        Result result3 = FindPassable(board3);

        Console.WriteLine("Passable rows position of board3:" + string.Join(",", result3._rows));

        Console.WriteLine("Passable columns position  of board3:" + string.Join(",", result3._cols));




        char[][] board4 =
        {
            new char[] {'+' }
        };


        Result result4 = FindPassable(board4);

        Console.WriteLine("Passable rows position of board4:" + string.Join(",", result4._rows));

        Console.WriteLine("Passable columns position  of board4:" + string.Join(",", result4._cols));




        char[][] board5 =
        {
            new char[] {'0' }
        };


        Result result5 = FindPassable(board5);

        Console.WriteLine("Passable rows position of board5:" + string.Join(",", result5._rows));

        Console.WriteLine("Passable columns position  of board5:" + string.Join(",", result5._cols));





        char[][] board6 =
        {
            new char[] { '0', '0' },
            new char[] { '0', '0' },
            new char[] { '0', '0' },
            new char[] { '0', '0' }
        };


        Result result6 = FindPassable(board6);

        Console.WriteLine("Passable rows position of board6:" + string.Join(",", result6._rows));

        Console.WriteLine("Passable columns position  of board6:" + string.Join(",", result6._cols));


        Console.ReadLine();




    }
    public static Result FindPassable(char[][] board)
    {
        Result result;

        List<int> rows = new List<int>();

        List<int> cols = new List<int>();

        int rowLength = board.Length;

        int columnLength = board[0].Length;


        for (int row = 0; row < rowLength; row++)
        {
            bool passable = true;
            //row checking
            for (int col = 0; col < columnLength; col++)
            {
                if (board[row][col] == '+')
                {
                    passable = false;
                    break;
                }

            }
            if (passable)
            {
                rows.Add(row);
            }


        }

        //column checking



        for (int col = 0; col < columnLength; col++)
        {
            bool passable = true;
            for (int row = 0; row < rowLength; row++)
            {
                if (board[row][col] == '+')
                {
                    passable = false;
                    break;
                }
            }
            if (passable)
            {
                cols.Add(col);
            }
        }

        return new Result(rows, cols);
    }
}



