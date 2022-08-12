using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Rewrite;

namespace client
{
    public class FirstLoadRewriteRule : IRule
    {
        public void ApplyRule(RewriteContext context)
        {
            var culture = CultureInfo.CurrentCulture;
            var request = context.HttpContext.Request;
            if (request.Path == "/")
            {
                request.Path = new Microsoft.AspNetCore.Http.PathString($"/{culture.Name}");
            }
        }
    }
}