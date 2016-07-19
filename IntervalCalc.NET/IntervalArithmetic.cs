using IntervalCalc.Expressions;
using IntervalCalc.Solvers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IntervalCalc
{
    public enum SolverType { ModifiedTransformation = 1, GeneralTransformation = 2, MonteCarlo = 3 }
    public static class IntervalArithmetic
    {
        public static Interval Calc(Expression<Func<double>> Expression, SolverType Solver = SolverType.ModifiedTransformation)
        {
            return GetSolver(Solver).Calc(() => new IntervalExpression(Expression));
        }

        public static ISolver GetSolver(SolverType Solver = SolverType.ModifiedTransformation)
        {
            switch (Solver)
            {
                case SolverType.ModifiedTransformation: return new ModifiedGASolver();
                case SolverType.GeneralTransformation: return new GeneralTransformationSolver();
                case SolverType.MonteCarlo: return new MonteCarloSolver();
                default: throw new ArgumentException(nameof(Solver));
            }
        }
    }
}
