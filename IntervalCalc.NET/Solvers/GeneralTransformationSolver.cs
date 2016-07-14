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
        public Interval Calc(Expression<Func<double>> Exp)
        {
            return Calc(Exp, 10);
        }
        public Interval Calc(Expression<Func<double>> Exp, int DiscretizationFactor)
        {
            var eep = new ExtractExpressionParams(Exp);
            var pars = eep.Params;

            var min = double.MaxValue;
            var max = double.MinValue;

            var rand = new Random();

            foreach (var v in eep.GetAllValues(DiscretizationFactor))
            {
                var next = eep.Calc(v);

                if (next < min) min = next;
                if (next > max) max = next;
            }

            return new Interval(min, max);
        }
    }
}
