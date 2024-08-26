using GoalSeekNS;

class Program
{
    static void Main(string[] args)
    {
        int implementation = 1;
        ErrorHandler ErrHand = new ErrorHandler();
        if (args.Length == 0)
        {
            ErrHand.ErrorMsg(1, "");
            return ;
        }
        if (args.Length == 2 && args[1].Length == 1 && args[1] == "2")
            implementation = 2;

        string[] allLines;
        try
        {
            allLines = File.ReadAllLines(args[0]);
        }
        catch
        {
            ErrHand.ErrorMsg(2, args[0]);
            return ;
        }
        if (ErrHand.FileCheck(allLines, args[0]))
            return ;

        Initializer init = new();
        Variables vars = init.InitVars(allLines);
        if (ErrHand.ValueCheck(vars))
            return ;
        Calculator calc = new();
        double result;

        if (implementation == 2)
            result = calc.GoalSeeker2(vars);
        else
            result = calc.GoalSeeker1(vars);
        
        if (result == -1)
        {
            ErrHand.ErrorMsg(4, "");
            return ;
        }
        result -= result % 0.01; // removes excess decimals in final answer, delete this line for more precision
        Console.WriteLine("maximum budget for new ad would be " + result.ToString());
    }
}