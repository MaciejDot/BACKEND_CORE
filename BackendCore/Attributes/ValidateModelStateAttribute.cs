
using Microsoft.AspNetCore.Mvc.Filters;

namespace BackendCore.Attributes
{
    public class ValidateModelStateAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.ModelState.IsValid)
            {
                return;
            }
            //filterContext.Result = ((BaseController)filterContext.Controller).BadRequest("Model is not valid");
            return;
        }
    }
}
