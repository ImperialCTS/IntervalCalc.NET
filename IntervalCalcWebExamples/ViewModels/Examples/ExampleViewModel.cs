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

        public Interval Output { get; private set; }

        public static ExampleViewModel GetEOQ(ParameterViewModel[] Values)
        {
            var D = CreateOrUpdateParameter(Values, "D", "Demand", "units", new Interval(9000, 11000));
            var K = CreateOrUpdateParameter(Values, "K", "Cost per order", "$", new Interval(1.5, 2.5));
            var h = CreateOrUpdateParameter(Values, "P", "Annual holidng cost", "$/unit", new Interval(0.14, 0.18));

            return new ExampleViewModel {
                Parameters = new[] { D, K, h },
                Output = IntervalArithmetic.Calc(() => Math.Sqrt(2 * D.Value.Any * K.Value.Any / h.Value.Any))
            };
        }

        public static ParameterViewModel CreateOrUpdateParameter(ParameterViewModel[] Values, string Name, string Title, string Unit, Interval DefaultValue)
        {
            var val = Values?.FirstOrDefault(v => v.Name == Name)?.Value ?? DefaultValue;
            return new ParameterViewModel { Name = Name, Title = Title, Unit = Unit, Value = val };
        }
    }
}
