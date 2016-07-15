using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace IntervalCalc.Solvers
{
    /// <summary>
    /// Using Monte-Carlo method for Interval Calculus
    /// </summary>
    public class MonteCarloSolver : ISolver
    {
        public int NumIterations { get; set; } = 10000;

        public Interval Calc(Expression<Func<double>> Exp)
        {
            var eep = new ExtractExpressionParams(Exp);
            var pars = eep.Params;

            var min = double.MaxValue;
            var max = double.MinValue;

            var rand = new Random();

            for (int i = 0; i < NumIterations; i++)
            {
                foreach (var p in pars)
                {
                    p.CurrentValue = rand.NextDouble() * p.Value.Range + p.Value.A;
                }

                var next = eep.Func();

                if (next < min) min = next;
                if (next > max) max = next;
            }

            return new Interval(min, max);
        }
    }
}
