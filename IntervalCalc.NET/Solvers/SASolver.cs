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
        class CalcPars
        {
            public Func<double> Exp { get; set; }
            public IntervalParams Params { get; set; }

            public double Calc(double[] Values)
            {
                for (int i = 0; i < Values.Length; i++)
                    Params[i].CurrentValue = Values[i];
                var ret = Exp();
                return ret;
            }

            public double[] Next(Random r, double[] Values)
            {
                var newarr = new double[Values.Length];
                for (int i = 0; i < Values.Length; i++)
                {
                    var inv = Params[i].Value;
                    var min = Math.Max(inv.A, Values[i] - inv.Range * 0.05);
                    var max = Math.Min(inv.B, Values[i] + inv.Range * 0.05);
                    newarr[i] = r.NextDouble() * (max - min) + min;
                }
                return newarr;
            }
        }

        public Interval Calc(Expression<Func<double>> Exp)
        {
            var method = new ThreadLocal<CalcPars>(() => 
            {
                var visitor = new ExtractExpressionParams();
                var newexp = (Expression<Func<double>>)visitor.Visit(Exp);
                return new CalcPars { Exp = newexp.Compile(), Params = visitor.Params };
            });

            var myval = method.Value;

            var sa = new SA();
            var r = new Random();
            var max = sa.Run(r, myval.Params.Select(v => v.Value.Middle).ToArray(), (v) => method.Value.Calc(v), (v, r2) => method.Value.Next(r2, v), IsMin: false).Item2;
            var min = sa.Run(r, myval.Params.Select(v => v.Value.Middle).ToArray(), (v) => method.Value.Calc(v), (v, r2) => method.Value.Next(r2, v)).Item2;

            return new Interval(min, max);
        }
    }
}
