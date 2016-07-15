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

            Console.WriteLine();

            Console.WriteLine("y/e^x where x in [0,1] and y in [3,4]:");
            PrintResults(() => y.Any / Math.Exp(x.Any));

            Console.WriteLine();

            var height = new Interval(1.795, 1.805);
            var weight = new Interval(79.5, 80.5);
            Console.WriteLine($"(weight/(height^2) where weight in {weight} and height in {height}:");
            PrintResults(() => weight.Any / (height.Any * height.Any));

            Console.WriteLine();

            var z = new Interval(-1, 1);
            Console.WriteLine($"z^2 + z where z in {z}:");
            PrintResults(() => z.Any * z.Any + z.Any);

            Console.ReadLine();
        }

        private static void PrintResults(Expression<Func<double>> exp)
        {
            Console.WriteLine($"Monte Carlo: {new MonteCarloSolver { NumIterations = 1000000 }.Calc(exp)}");
            Console.WriteLine($"SA: {new SASolver().Calc(exp)}");
            Console.WriteLine($"General Transformation: {new GeneralTransformationSolver { DiscretizationFactor = 1000 }.Calc(exp)}");
            Console.WriteLine($"Mod. General Transformation: {new ModifiedGASolver().Calc(exp)}");
        }
    }
}
