using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IntervalCalc;
using IntervalCalc.Solvers;

namespace IntervalCalcTests
{
    [TestClass]
    public class GTUnitTest : SolverUnitTestBase
    {
        public GTUnitTest() : base(new GeneralTransformationSolver(), 0.0001) { }
    }
}
