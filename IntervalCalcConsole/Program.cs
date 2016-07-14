using IntervalCalc;
using IntervalCalc.Solvers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntervalCalcConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var inv = new Interval(0, 1);

            Console.WriteLine(new MonteCarloSolver().Calc(() => inv.Any * (1 - inv.Any)));
            Console.WriteLine(new SASolver().Calc(() => inv.Any * (1 - inv.Any)));
            Console.ReadLine();
        }
    }
}
