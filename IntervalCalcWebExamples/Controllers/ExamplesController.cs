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
        public IActionResult EOQ()
        {
            return View(ExampleViewModel.GetEOQ(null));
        }

        [HttpPost]
        public IActionResult EOQ(ParameterViewModel[] Parameters)
        {
            return View(ExampleViewModel.GetEOQ(Parameters));
        }
    }
}
