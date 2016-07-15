using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace IntervalCalc.Solvers
{
    /// <summary>
    /// Using General Transformation method for Interval Calculus
    /// </summary>
    public class GeneralTransformationSolver : ISolver
    {
        public int DiscretizationFactor { get; set; } = 10;

        public Interval Calc(Expression<Func<double>> Exp)
        {
            var eep = new ExtractExpressionParams(Exp);
            var pars = eep.Params;

            var min = double.MaxValue;
            var max = double.MinValue;

            var rand = new Random();

            foreach (var v in GetAllValues(eep.Params))
            {
                var next = eep.Calc(v);

                if (next < min) min = next;
                if (next > max) max = next;
            }

            return new Interval(min, max);
        }

        IEnumerable<double[]> GetAllValues(IntervalParams Params)
        {
            var result = Params.Select(v => v.Value.A).ToArray();

            var steps = Params.Select(v => v.Value.Range / DiscretizationFactor).ToArray();

            bool finished = false;

            while (!finished)
            {
                yield return result;

                for (int i = 0; i < result.Length; i++)
                {
                    if (result[i] == Params[i].Value.B)
                    {
                        if (i == result.Length - 1) finished = true;
                        result[i] = Params[i].Value.A;
                    }
                    else
                    {
                        result[i] += steps[i];
                        if (result[i] > Params[i].Value.B)
                            result[i] = Params[i].Value.B;
                        else
                            break;
                    }
                }
            }
        }
    }
}
