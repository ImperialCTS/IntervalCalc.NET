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

            var result = Solver.Calc(() => inv.Any * (1 - inv.Any));

            Assert.IsTrue(result.A >= 0, "A < 0");
            Assert.IsTrue(result.B <= 0.25, "B > 0.25");

            Assert.IsTrue(result.A < epsilon, "A >> 0");
            Assert.IsTrue(0.25 - result.B < epsilon, "B << 0.25");
        }
    }
}
