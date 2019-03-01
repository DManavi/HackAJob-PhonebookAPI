using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;

namespace API.Filters
{
    public class LoggedIn : AuthorizationFilterAttribute
    {
    }
}