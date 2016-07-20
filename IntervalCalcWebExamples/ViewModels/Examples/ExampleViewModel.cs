using IntervalCalc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntervalCalcWebExamples.ViewModels.Examples
{
    public class ExampleViewModel
    {
        public ParameterViewModel[] Parameters { get; private set; }
        public ResultViewModel[] Results { get; private set; }
        public string Title { get; private set; }
        //public Interval Output { get; private set; }

        public static ExampleViewModel GetEOQ(ParameterViewModel[] Values)
        {
            var D = CreateOrUpdateParameter(Values, "D", "Demand", "units", new Interval(9000, 11000), new Interval(5000, 15000));
            var K = CreateOrUpdateParameter(Values, "K", "Cost per order", "$", new Interval(1.5, 2.5), new Interval(0, 10));
            var h = CreateOrUpdateParameter(Values, "P", "Annual holidng cost", "$/unit", new Interval(0.14, 0.18), new Interval(0.1, 2));

            var Q = new ResultViewModel
            {
                Title = "Optimal order quantity",
                Unit = "units",
                Range = new Interval(0, 1000),
                Value = IntervalArithmetic.Calc(() => Math.Sqrt(2 * D.Value.Any * K.Value.Any / h.Value.Any))
            };

            return new ExampleViewModel { Title = "Economic Order Quantity (EOQ)", Parameters = new[] { D, K, h }, Results = new[] { Q } };
        }

        public static ExampleViewModel GetBasicNPV(ParameterViewModel[] Values)
        {
            var I = CreateOrUpdateParameter(Values, "I", "Initial investment", "$", new Interval(90000, 110000), new Interval(50000, 150000), 1000);
            var R = CreateOrUpdateParameter(Values, "R", "Annual return", "$", new Interval(5000, 10000), new Interval(0, 20000), 1000);
            var T = CreateOrUpdateParameter(Values, "T", "Number of years", "years", new Interval(5, 10), new Interval(0, 20), 1);
            var i = CreateOrUpdateParameter(Values, "i", "Discount rate", "%", new Interval(1, 3), new Interval(0, 100), 1);

            var NPV = new ResultViewModel
            {
                Title = "NPV",
                Unit = "$",
                Range = new Interval(-100000, 100000),
                Value = IntervalArithmetic.Calc(() => - I.Value.Any + Enumerable.Range(1, (int)Math.Floor(T.Value.Any)).Sum(y => R.Value.Any / Math.Pow(1 + i.Value.Any / 100, y)) )
            };

            return new ExampleViewModel { Title = "Net Present Value (NPV) - Basic", Parameters = new[] { I, R, T, i }, Results = new[] { NPV } };
        }

        public static ParameterViewModel CreateOrUpdateParameter(ParameterViewModel[] Values, string Name, string Title, string Unit, Interval DefaultValue, Interval CustomRange = null, double Step = 0.01)
        {
            var val = Values?.FirstOrDefault(v => v.Name == Name)?.Value ?? DefaultValue;
            return new ParameterViewModel { Name = Name, Title = Title, Unit = Unit, Value = val, CustomRange = CustomRange, Step = Step };
        }
    }
}
