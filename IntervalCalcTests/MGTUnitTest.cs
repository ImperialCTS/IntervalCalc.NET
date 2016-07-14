using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IntervalCalc;
using IntervalCalc.Solvers;

namespace IntervalCalcTests
{
    [TestClass]
    public class MGTUnitTest : SolverUnitTestBase
    {
        public MGTUnitTest() : base(new ModifiedGASolver()) { }
    }
}
