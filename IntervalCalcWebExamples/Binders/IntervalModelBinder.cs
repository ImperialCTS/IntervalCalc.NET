using System;
using System.Threading.Tasks;
using IntervalCalc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;

namespace IntervalCalcWebExamples.Binders
{
    public class IntervalModelBinder : IModelBinder
    {
        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext.ModelType == typeof(Interval))
            {
                var AStr = bindingContext.ValueProvider.GetValue(bindingContext.ModelName + ".A").FirstValue;                
                var BStr = bindingContext.ValueProvider.GetValue(bindingContext.ModelName + ".B").FirstValue;
                double A, B;
                if (double.TryParse(AStr, out A) && double.TryParse(BStr, out B) && B >= A)
                    bindingContext.Result = ModelBindingResult.Success(new Interval(A, B));

                var key = ModelMetadataIdentity.ForType(typeof(Interval));
                var cache = new DefaultMetadataDetails(key, new ModelAttributes(new object[0]));
                cache.ValidationMetadata = new ValidationMetadata()
                {
                    ValidateChildren = false,
                };

                return;
            }

            bindingContext.Result = ModelBindingResult.Failed();
            return;
        }
    }
}