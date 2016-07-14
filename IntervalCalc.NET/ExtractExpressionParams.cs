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
        public ExtractExpressionParams()
        {
            Params = new IntervalParams();
        }

        public IntervalParams Params { get; private set; }

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
