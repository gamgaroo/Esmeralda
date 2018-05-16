using System;
using Gamgaroo.Esmeralda.App.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Gamgaroo.Esmeralda.App.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public sealed class BasicAuthorizationAttribute : TypeFilterAttribute
    {
        public BasicAuthorizationAttribute() : base(typeof(BasicAuthorizationFilter))
        {
        }
    }
}