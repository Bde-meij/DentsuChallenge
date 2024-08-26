namespace GoalSeekNS
{
    public class Variables()
    {
        public double Z;
        public double[] X = [];
        public double Y1;
        public double Y2;
        public double HOURS;
        public int newXPos;
    }
    public class Initializer()
    {
        public Variables InitVars(string[] allLines)
        {
            // convert parsed strings into 'double' values
            Variables vars = new()
            {
                Z = double.Parse(allLines[0].Split(" ")[1]),
                Y1 = double.Parse(allLines[2].Split(" ")[1].TrimEnd('%')) / 100,
                Y2 = double.Parse(allLines[3].Split(" ")[1].TrimEnd('%')) / 100,
                HOURS = double.Parse(allLines[4].Split(" ")[1])
            };

            string[] xLine = allLines[1].Split(" ");

            // the Formula() assumes at least 4 X's are given, 
            // set first 4 X's to 0 in case input file has less than 4
            if (xLine.Length >= 4)
                vars.X = new double[xLine.Length];
            else
            {
                vars.X = new double[4];
                for (int i = 0; i < 4; i++)  
                    vars.X[i] = 0;
            }

            // fills X[], newXPos is the position which is the new 'ad budget' 
            // we are trying to calculate with GoalSeek 
            vars.newXPos = xLine.Length -1;
            for (int i = 1; i <= vars.newXPos; i++)
                vars.X[i-1] = double.Parse(xLine[i]);
            vars.X[vars.newXPos] = 0;
            
            return vars;
        }
    }
}