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
        public Interval Calc(Expression<Func<double>> Exp)
        {
            return Calc(Exp, 10000);
        }
        public Interval Calc(Expression<Func<double>> Exp, int NumIterations)
        {
            var visitor = new ExtractExpressionParams();
            var newexp = (Expression<Func<double>>)visitor.Visit(Exp);
            var method = newexp.Compile();
            var pars = visitor.Params;

            var min = double.MaxValue;
            var max = double.MinValue;

            var rand = new Random();

            for (int i = 0; i < NumIterations; i++)
            {
                foreach (var p in pars)
                {
                    p.CurrentValue = rand.NextDouble() * p.Value.Range + p.Value.A;
                }

                var next = method();

                if (next < min) min = next;
                if (next > max) max = next;
            }

            return new Interval(min, max);
        }
    }
}
