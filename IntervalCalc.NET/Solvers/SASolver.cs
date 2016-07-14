using IntervalCalc.Algorithms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace IntervalCalc.Solvers
{
    /// <summary>
    /// Using Simulated Annealing method for Interval Calculus
    /// </summary>
    public class SASolver : ISolver
    {
        public Interval Calc(Expression<Func<double>> Exp)
        {
            var method = new ThreadLocal<ExtractExpressionParams>(() => new ExtractExpressionParams(Exp));

            var myval = method.Value;

            var sa = new SA();
            var r = new Random();
            var max = sa.Run(r, myval.Params.Select(v => v.Value.Middle).ToArray(), (v) => method.Value.Calc(v), (v, r2) => method.Value.GetRandomNeighbour(r2, v), IsMin: false).Item2;
            var min = sa.Run(r, myval.Params.Select(v => v.Value.Middle).ToArray(), (v) => method.Value.Calc(v), (v, r2) => method.Value.GetRandomNeighbour(r2, v)).Item2;

            return new Interval(min, max);
        }
    }
}
