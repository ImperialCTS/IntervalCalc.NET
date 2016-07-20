using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IntervalCalcWebExamples.ViewModels.Examples;
using Microsoft.AspNetCore.Mvc;

namespace IntervalCalcWebExamples.Controllers
{
    public class ExamplesController : Controller
    {
        public IActionResult EOQ([FromForm]ParameterViewModel[] Parameters)
        {
            return View("Example", ExampleViewModel.GetEOQ(Parameters));
        }

        public IActionResult BasicNPV([FromForm]ParameterViewModel[] Parameters)
        {
            return View("Example", ExampleViewModel.GetBasicNPV(Parameters));
        }
    }
}
