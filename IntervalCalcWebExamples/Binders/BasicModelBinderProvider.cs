using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntervalCalcWebExamples.Binders
{
    public class BasicModelBinderProvider<BinderType, T> : IModelBinderProvider where BinderType : IModelBinder, new()
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (context.Metadata.ModelType == typeof(T))
                return new BinderType();

            return null;
        }
    }
}
