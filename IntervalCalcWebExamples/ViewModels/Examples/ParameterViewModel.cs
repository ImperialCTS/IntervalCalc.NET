using IntervalCalc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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
        public Interval Value { get; set; }
        public Interval CustomRange { get; set; }
    }
}
