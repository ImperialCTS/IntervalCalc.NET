using IntervalCalc;
using IntervalCalcWebExamples.Models;
using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntervalCalcWebExamples.ViewModels.Examples
{
    public class ParameterViewModel
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public string Unit { get; set; }
        [ModelBinder(BinderType = typeof(IntervalModelBinder))]
        public Interval Value { get; set; }
    }
}
