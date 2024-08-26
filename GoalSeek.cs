namespace GoalSeekNS
{
    public class Calculator
    {
        int stepsTaken = 0;
        public double CombineXValues(double[] X)
        {
            double totalX = 0;
            for (int i = 0; i< X.Length; i++)
                totalX += X[i];
            return totalX;
        }
        public double Formula(Variables vars)
        {
            double allX = CombineXValues(vars.X);
            return allX + (vars.Y1*allX) + vars.Y2*(vars.X[0]+vars.X[1]+vars.X[3]) + vars.HOURS;
        }

        public double IncrementSteps(double budget, double stepSize, Variables vars)
        {
            while (budget < vars.Z)
            {
                stepsTaken += 1;
                vars.X[vars.newXPos] += stepSize;
                budget = Formula(vars);
            }
            return budget;
        }
        public double DecrementSteps(double budget, double stepSize, Variables vars)
        {
            while (budget > vars.Z)
            {
                stepsTaken += 1;
                vars.X[vars.newXPos] -= stepSize;
                budget = Formula(vars);
            }
            return budget;
        }
        public double GoalSeeker1(Variables vars)
        {
            // in the inital guess we take the diff of current budget and limit Z
            // whilst taking into account the effect Y1 and Y2 might have on growth
            double growthFactor = (1+vars.Y1)*(1+vars.Y2);
            double initialGuess = vars.Z - Formula(vars);
            if (growthFactor != 0) // dividing by zero could potentially open a black hole
                initialGuess = (vars.Z - Formula(vars)) / growthFactor;

            Console.WriteLine("started goalseeker1, initial guess is "+ initialGuess.ToString());
            double stepSize = initialGuess;
            double budget = Formula(vars);

            if (budget > vars.Z)
                return -1;

            // increment or decrement with 'stepSize' until limit Z is reached
            // repeat with increasingly smaller steps for precision
            while (stepSize > 0.01)
            {
                if (budget < vars.Z)
                    budget = IncrementSteps(budget, stepSize, vars);
                else if (budget > vars.Z)
                    budget = DecrementSteps(budget, stepSize, vars);
                stepSize /= 2;
            }
            Console.WriteLine("steps taken = " + stepsTaken.ToString());
            return vars.X[vars.newXPos];
        }

        public double GoalSeeker2(Variables vars)
        {
            double budget1 = Formula(vars);

             if (budget1 > vars.Z)
                return -1;

            vars.X[vars.newXPos] = 1;
            double budget2 = Formula(vars);

            double budgetDiff = budget2 - budget1;
            if (budgetDiff == 0) // this protection is probably not necesary
                budgetDiff = 1;
            // we take the difference in budgets after setting 'new X' to 1
            // then immediately calculate how big 'new X' has to be to reach 'Z'
            return (vars.Z - budget1)/budgetDiff;
        }
    }
}