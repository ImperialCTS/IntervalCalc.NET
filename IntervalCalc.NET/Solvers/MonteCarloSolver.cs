using IntervalCalc.Expressions;
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

        public Interval Calc(Func<IIntervalExpression> GetExp)
        {
            var eep = GetExp();
            var pars = eep.Params;

            var min = double.MaxValue;
            var max = double.MinValue;

            var rand = new Random();

            var values = pars.Select(p => 0D).ToArray();

            for (int i = 0; i < NumIterations; i++)
            {
                for (int j = 0; j < values.Length; j++)
                    values[j] = rand.NextDouble() * pars[j].Value.Range + pars[j].Value.A;

                var next = eep.Calc(values);

                if (next < min) min = next;
                if (next > max) max = next;
            }

            return new Interval(min, max);
        }
    }
}
