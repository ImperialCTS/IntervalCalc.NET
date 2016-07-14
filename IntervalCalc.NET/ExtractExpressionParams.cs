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

        public IEnumerable<double[]> GetAllValues(int DiscretizationFactor = 10)
        {
            var result = Params.Select(v => v.Value.A).ToArray();

            var steps = Params.Select(v => v.Value.Range / DiscretizationFactor).ToArray();

            bool finished = false;

            while (!finished)
            {
                yield return result;

                for (int i = 0; i < result.Length; i++)
                {
                    if (result[i] == Params[i].Value.B)
                    {
                        if (i == result.Length - 1) finished = true;
                        result[i] = Params[i].Value.A;
                    }
                    else
                    {
                        result[i] += steps[i];
                        if (result[i] > Params[i].Value.B)
                            result[i] = Params[i].Value.B;
                        else
                            break;
                    }
                }
            }
        }

        public double[] GetRandomNeighbour(Random r, double[] Values)
        {
            var newarr = new double[Values.Length];
            for (int i = 0; i < Values.Length; i++)
            {
                var inv = Params[i].Value;
                var min = Math.Max(inv.A, Values[i] - inv.Range * 0.05);
                var max = Math.Min(inv.B, Values[i] + inv.Range * 0.05);
                newarr[i] = r.NextDouble() * (max - min) + min;
            }
            return newarr;
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
