using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc.ModelBinding;
using IntervalCalc;

namespace IntervalCalcWebExamples.Models
{
    public class IntervalModelBinder : IModelBinder
    {
        public Task<ModelBindingResult> BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext.ModelType == typeof(Interval))
            {
                var A = bindingContext.ValueProvider.GetValue(bindingContext.ModelName + ".A").FirstValue;
                var B = bindingContext.ValueProvider.GetValue(bindingContext.ModelName + ".B").FirstValue;             
                return ModelBindingResult.SuccessAsync(bindingContext.ModelName, new Interval(double.Parse(A), double.Parse(B)));
            }

            return ModelBindingResult.FailedAsync(bindingContext.ModelName);
        }
    }
}