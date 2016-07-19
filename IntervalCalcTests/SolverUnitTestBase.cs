using IntervalCalc;
using IntervalCalc.Expressions;
using IntervalCalc.Solvers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IntervalCalcTests
{
    public abstract class SolverUnitTestBase
    {
        public SolverUnitTestBase(ISolver Solver, double epsilon = 0.0001D)
        {
            this.Solver = Solver;
            this.epsilon = epsilon;
        }

        double epsilon;

        ISolver Solver;

        private Interval Calc(Expression<Func<double>> Exp)
        {
            return Solver.Calc(() => new IntervalExpression(Exp));
        }

        [TestMethod]
        public void BasicTest()
        {
            var inv = new Interval(0, 1);

            CheckWithin(Calc(() => inv.Any * (1 - inv.Any)), new Interval(0, 0.25));
        }

        [TestMethod]
        public void TwoVariableMult()
        {
            var x = new Interval(0, 1);
            var y = new Interval(3, 4);

            CheckWithin(Calc(() => x.Any * y.Any), new Interval(0, 4));
        }

        [TestMethod]
        public void TwoVariableDiv()
        {
            var x = new Interval(0, 1);
            var y = new Interval(3, 4);

            CheckWithin(Calc(() => x.Any / y.Any), new Interval(0, 1D / 3));
        }

        [TestMethod]
        public void WikipediaExp()
        {
            var height = new Interval(1.795, 1.805);
            var weight = new Interval(79.5, 80.5);
            CheckWithin(Calc(() => weight.Any / (height.Any * height.Any)), new Interval(24.4, 25.0), 0.1);
        }

        [TestMethod]
        public void WikipediaExp2()
        {
            var x = new Interval(-1, 1);
            CheckWithin(Calc(() => x.Any * x.Any + x.Any), new Interval(-0.25, 2));
        }

        public void CheckWithin(Interval result, Interval CorrectResult, double? overrideepsilon = null)
        {
            overrideepsilon = overrideepsilon ?? epsilon;
            var name = Solver.GetType().Name + ":";
            Assert.IsTrue(result.A >= CorrectResult.A, name + "A < " + CorrectResult.A);
            Assert.IsTrue(result.B <= CorrectResult.B, name + "B > " + CorrectResult.B);

            Assert.IsTrue(result.A - CorrectResult.A < overrideepsilon, name + "A >> " + CorrectResult.A);
            Assert.IsTrue(CorrectResult.B - result.B < overrideepsilon, name + "B << " + CorrectResult.B);
        }
    }
}
