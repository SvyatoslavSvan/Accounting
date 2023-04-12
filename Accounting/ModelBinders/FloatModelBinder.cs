using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Globalization;

namespace Accounting.ModelBinders
{
    public class FloatModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            if (valueProviderResult != ValueProviderResult.None && float.TryParse(valueProviderResult.FirstValue, NumberStyles.Any, CultureInfo.InvariantCulture, out var result))
            {
                bindingContext.Result = ModelBindingResult.Success(result);
            }
            else
            {
                bindingContext.Result = ModelBindingResult.Failed();
            }

            return Task.CompletedTask;
        }
    }
}
