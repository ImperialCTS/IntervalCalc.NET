using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntervalCalc.Algorithms
{
    /// <summary>
    /// Implementation of Simmulated Annealing
    /// Loosly based on http://www.codeproject.com/Articles/26758/Simulated-Annealing-Solving-the-Travelling-Salesma
    /// </summary>
    public class SA
    {
        public double Temperature { get; set; } = 10000.0;
        public double CoolingRate { get; set; } = 0.9995;
        public double AbsoluteTemperature { get; set; } = 0.001;

        public Tuple<T, double> Run<T>(Random generalr, T InitSolution, Func<T, double> CalcCost, Func<T, Random, T> GetNext,
            Action<Tuple<T, double>> OnNewSolution = null, bool IsMin = true, int Repeat = 4)
        {
            var mylock = new object();
            Tuple<T, double> best = null;

            Action<Tuple<T, double>> SetBest = (t) =>
            {
                lock (mylock)
                {
                    best = t;
                    OnNewSolution?.Invoke(best);
                }
            };

            var randoms = Enumerable.Range(0, Repeat).Select(rep => new Random(generalr.Next(int.MaxValue))).ToArray();
            var dir = IsMin ? 1 : -1;
            Parallel.For(0, Repeat, (rep) =>
            {
                var r = randoms[rep];
                T solution = InitSolution;
                int iteration = -1;
                double cost = dir * CalcCost(solution);

                lock (mylock)
                {
                    if (best == null)
                    {
                        SetBest(Tuple.Create(solution, dir * cost));
                    }
                }

                var temp = Temperature;
                while (temp > AbsoluteTemperature)
                {
                    var newrotations = GetNext(solution, r);

                    var newcost = dir * CalcCost(newrotations);
                    var DeltaDistance = newcost - cost;

                    lock (mylock)
                    {
                        if (newcost < best.Item2)
                        {
                            var mybest = Tuple.Create(newrotations, dir * newcost);
                            SetBest(mybest);
                        }
                    }

                    if ((DeltaDistance < 0) || (cost > 0 &&
                         Math.Exp(-DeltaDistance / temp) > r.NextDouble()))
                    {
                        solution = newrotations;

                        cost = DeltaDistance + cost;
                    }

                    temp *= CoolingRate;

                    iteration++;
                }
            });

            return best;
        }
    }
}
