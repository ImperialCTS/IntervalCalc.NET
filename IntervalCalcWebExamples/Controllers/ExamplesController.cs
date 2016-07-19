using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using IntervalCalcWebExamples.ViewModels.Examples;

namespace IntervalCalcWebExamples.Controllers
{
    public class ExamplesController : Controller
    {
        // GET: /<controller>/
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
