using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IntervalCalc;
using IntervalCalc.Solvers;

namespace IntervalCalcTests
{
    [TestClass]
    public class SAUnitTest : SolverUnitTestBase
    {
        public SAUnitTest() : base(new SASolver(), 0.1) { }
    }
}
