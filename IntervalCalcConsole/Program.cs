using IntervalCalc;
using IntervalCalc.Solvers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IntervalCalcConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var x = new Interval(0, 1);
            var y = new Interval(3, 4);

            Console.WriteLine("x * (1 - x) where x in [0,1]:");
            PrintResults(() => x.Any * (1 - x.Any));

            Console.WriteLine();

            Console.WriteLine("x * y where x in [0,1] and y in [3,4]:");
            PrintResults(() => x.Any * y.Any);

            Console.WriteLine();

            Console.WriteLine("x / y where x in [0,1] and y in [3,4]:");
            PrintResults(() => x.Any / y.Any);

            Console.ReadLine();
        }

        private static void PrintResults(Expression<Func<double>> exp)
        {
            Console.WriteLine($"Monte Carlo: {new MonteCarloSolver().Calc(exp)}");
            Console.WriteLine($"SA: {new SASolver().Calc(exp)}");
            Console.WriteLine($"General Transformation: {new GeneralTransformationSolver().Calc(exp)}");
        }
    }
}
