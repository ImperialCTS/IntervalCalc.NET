using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IntervalCalc;
using IntervalCalc.Solvers;

namespace IntervalCalcTests
{
    [TestClass]
    public class MCUnitTest : SolverUnitTestBase
    {
        public MCUnitTest() : base(new MonteCarloSolver(), 0.001) { }
    }
}
