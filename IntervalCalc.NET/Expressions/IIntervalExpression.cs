using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntervalCalc.Expressions
{
    public interface IIntervalExpression
    {
        IntervalParams Params { get; }
        double Calc(double[] Values);
    }
}
