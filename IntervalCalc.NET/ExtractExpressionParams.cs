using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace IntervalCalc
{
    public class ExtractExpressionParams : ExpressionVisitor
    {
        public ExtractExpressionParams(Expression Exp)
        {
            Params = new IntervalParams();
            this.Func = ((Expression<Func<double>>)Visit(Exp)).Compile();
        }

        public IntervalParams Params { get; private set; }
        public Func<double> Func { get; set; }

        public double Calc(double[] Values)
        {
            for (int i = 0; i < Values.Length; i++)
                Params[i].CurrentValue = Values[i];
            var ret = Func();
            return ret;
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            if (node.NodeType == ExpressionType.MemberAccess && node.Expression.Type == typeof(Interval) && node.Member.Name == "Any")
            {
                var inv = Expression.Lambda<Func<Interval>>(node.Expression).Compile()();
                if (Params[inv] == null)
                {
                    Params.AddParam(inv);
                }
                var p = Params[inv];
                return Expression.MakeMemberAccess(Expression.Constant(p), typeof(IntervalParams.Param).GetTypeInfo()
                    .GetDeclaredProperty(nameof(IntervalParams.Param.CurrentValue)));
            }
            else
                return base.VisitMember(node);
        }
    }
}
