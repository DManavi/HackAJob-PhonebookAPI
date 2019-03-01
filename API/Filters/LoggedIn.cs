using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace API.Filters
{
    public class LoggedIn : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            object username = null;

            if (!actionContext.Request.Properties.TryGetValue("username", out username))
            {

                actionContext.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);

                return;
            }

            base.OnAuthorization(actionContext);
        }
    }
}