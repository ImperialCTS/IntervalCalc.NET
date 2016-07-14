using IntervalCalc;
using IntervalCalc.Solvers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
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

        [TestMethod]
        public void BasicTest()
        {
            var inv = new Interval(0, 1);

            CheckWithin(Solver.Calc(() => inv.Any * (1 - inv.Any)), new Interval(0, 0.25));
        }

        [TestMethod]
        public void TwoVariableMult()
        {
            var x = new Interval(0, 1);
            var y = new Interval(3, 4);

            CheckWithin(Solver.Calc(() => x.Any * y.Any), new Interval(0, 4));
        }

        [TestMethod]
        public void TwoVariableDiv()
        {
            var x = new Interval(0, 1);
            var y = new Interval(3, 4);

            CheckWithin(Solver.Calc(() => x.Any / y.Any), new Interval(0, 1D / 3));
        }

        public void CheckWithin(Interval result, Interval CorrectResult)
        {
            var name = Solver.GetType().Name + ":";
            Assert.IsTrue(result.A >= CorrectResult.A, name + "A < " + CorrectResult.A);
            Assert.IsTrue(result.B <= CorrectResult.B, name + "B > " + CorrectResult.B);

            Assert.IsTrue(result.A - CorrectResult.A < epsilon, name + "A >> " + CorrectResult.A);
            Assert.IsTrue(CorrectResult.B - result.B < epsilon, name + "B << " + CorrectResult.B);
        }
    }
}
