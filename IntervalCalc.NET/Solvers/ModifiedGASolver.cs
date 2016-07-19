using IntervalCalc.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace IntervalCalc.Solvers
{
    /// <summary>
    /// Using Modified General Transformation method for Interval Calculus
    /// </summary>
    public class ModifiedGASolver : ISolver
    {
        public int DiscretizationFactor { get; set; } = 10;

        public Interval Calc(Func<IIntervalExpression> GetExp)
        {
            var eep = GetExp();
            var pars = eep.Params;

            var min = double.MaxValue;
            var max = double.MinValue;

            var steps = eep.Params.Select(v => v.Value.Range / (DiscretizationFactor * 2)).ToArray();

            var options = eep.Params.Select(v => new double[] { v.Value.Middle, v.Value.Middle, v.Value.Middle, v.Value.Middle }).ToArray();

            var prevmin = eep.Params.Select(v => v.Value.Middle).ToArray();
            var prevmax = eep.Params.Select(v => v.Value.Middle).ToArray();

            var rand = new Random();
            for (int i = 1; i <= DiscretizationFactor; i++)
            {
                // Updating the Min and Max at current disc. level
                for (int j = 0; j < options.Length; j++)
                {
                    options[j][0] -= steps[j]; // Min
                    options[j][1] += steps[j]; // Max
                }

                // Checking the possible combinations
                foreach (var v in GetAllValues(options))
                {
                    var next = eep.Calc(v);

                    if (next < min) { min = next; Array.Copy(v, prevmin, v.Length); }
                    if (next > max) { max = next; Array.Copy(v, prevmax, v.Length); }
                }

                // Updating PrevMin and PrevMax in Options
                for (int j = 0; j < options.Length; j++)
                {
                    options[j][2] = prevmin[j]; // Min
                    options[j][3] = prevmax[j]; // Max
                }
            }
            return new Interval(min, max);
        }

        static IEnumerable<double[]> GetAllValues(double[][] Options)
        {
            var result = Options.Select(v => v[0]).ToArray();
            var pos = Options.Select(v => 0).ToArray();

            while (true)
            {
                yield return result;

                for (int i = 0; i < result.Length; i++)
                {
                    pos[i]++;

                    if (pos[i] == Options[i].Length)
                    {
                        pos[i] = 0;
                        result[i] = Options[i][pos[i]];
                        if (i == Options.Length - 1) yield break;
                    }
                    else
                    {
                        result[i] = Options[i][pos[i]];
                        break;
                    }
                }
            }
        }
    }
}
