using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace API.Filters
{
    public class Jwt : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            IEnumerable<string> headerValue = null;

            if (actionContext.Request.Headers.TryGetValues("Authorization", out headerValue) && headerValue.Count() > 0)
            {
                var parts = headerValue.ElementAt(0).Split(' ');

                if (parts.Length == 2 && parts[0].ToLower() == "bearer")
                {
                    var username = string.Empty;

                    var payload = Services.Jwt.Check(parts[1], out username);

                    if (username.Length > 0)
                    {
                        actionContext.Request.Properties.Add("username", username);
                    }
                }
            }

            base.OnAuthorization(actionContext);
        }
    }
}