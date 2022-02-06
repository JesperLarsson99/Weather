using Microsoft.AspNetCore.Mvc.Filters;
using System.IO;
using System.IO.Pipelines;
using WeatherTest.Models;

namespace WeatherTest.Attributes
{
    public class LimitAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if(context.ActionArguments.TryGetValue("request", out var value))
            {
                if ((value as WeatherRequest).positionList.Count >= 20)
                {
                    (value as WeatherRequest).IsValid = false;
                }
                else
                {
                    (value as WeatherRequest).IsValid = true;
                }
            }
        }
    }
}