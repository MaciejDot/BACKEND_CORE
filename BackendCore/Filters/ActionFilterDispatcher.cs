
using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;

namespace BackendCore.Filters
{

    public sealed class ActionFilterDispatcher : IActionFilter
    {
        private readonly IServiceProvider _services;

        public ActionFilterDispatcher(IServiceProvider services)
        {
            _services = services;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            IEnumerable<object> attributes =
                context.Controller.GetType().GetTypeInfo()
                    .GetCustomAttributes(true);

            var descriptor = context.ActionDescriptor
                as ControllerActionDescriptor;

            if (descriptor != null)
            {
                attributes = attributes.Concat(
                    descriptor.MethodInfo.GetCustomAttributes(true));
            }

            foreach (var attribute in attributes)
            {
                Type filterType = typeof(Attributes.IActionFilter<>)
                    .MakeGenericType(attribute.GetType());
                var filters = _services.GetServices(filterType);
                foreach (dynamic actionFilter in filters)
                {
                    actionFilter.OnActionExecuting((dynamic)attribute, context);
                }
            }
        }

        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}
