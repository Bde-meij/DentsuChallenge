using System.Security.Cryptography.X509Certificates;

namespace GoalSeekNS
{
    public class ErrorHandler()
    {
        public void ErrorMsg(int msgNum, string fileName)
        {
            if (msgNum == 1)
                Console.WriteLine("No file was given, try: dotnet run \'filename\' ");
            if (msgNum == 2)
                Console.WriteLine("File " + fileName + " could not be read, please make sure path and access rights are correct");
            if (msgNum == 3)
                Console.WriteLine("File " + fileName + " is incorrectly formatted, example format:\nZ: 5000\nX: 100 240 305.6 4123\nY1: 35%\nY2: 12%\nHOURS: 400");
            if (msgNum == 4)
                Console.WriteLine("Current budget already exceeds limit 'Z', cannot calculate for new X");
            if (msgNum == 5)
                Console.WriteLine("Negative value detected, this would make no sense for limits, budgets or agency fees");
        }

        public bool FileCheck(string[] allLines, string fileName)
        {
            if (allLines.Length < 5)
            {
                ErrorMsg(3, fileName);
                return true;
            }
            string[] zLine = allLines[0].Split(" ");
            string[] xLine = allLines[1].Split(" ");
            string[] y1Line = allLines[2].Split(" ");
            string[] y2Line = allLines[3].Split(" ");
            string[] hourLine = allLines[4].Split(" ");
            if (zLine[0] != "Z:" || xLine[0] != "X:" || y1Line[0] != "Y1:" || y2Line[0] != "Y2:" || hourLine[0] != "HOURS:")
            {
                ErrorMsg(3, fileName);
                return true;
            }
            try
            {
                double.Parse(zLine[1]);
                for (int i = 1; i < xLine.Length; i++)
                    double.Parse(xLine[i]);
                double.Parse(y1Line[1].TrimEnd('%'));
                double.Parse(y2Line[1].TrimEnd('%'));
                double.Parse(hourLine[1]);
            }
            catch
            {
                ErrorMsg(3, fileName);
                return true;
            }
            return false;
        }

        public bool ValueCheck(Variables vars)
        {
            if (vars.Z < 0 || vars.Y1 < 0 || vars.Y2 < 0 || vars.HOURS < 0)
            {
                ErrorMsg(5, "");
                return true;
            }
            for (int i = 0; i < vars.X.Length; i++)
            {
                if (vars.X[i] < 0)
                {
                    ErrorMsg(5, "");
                    return true;
                }
            }
            return false;
        }
    }
}
