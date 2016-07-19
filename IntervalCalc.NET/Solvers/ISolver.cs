using IntervalCalc.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IntervalCalc.Solvers
{
    public interface ISolver
    {
        Interval Calc(Func<IIntervalExpression> GetExp);
    }
}
