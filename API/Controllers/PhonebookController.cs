using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Entity;
using System.Web.Http;

namespace API.Controllers
{
    [Filters.Jwt]
    [Filters.LoggedIn]
    public class PhonebookController : ApiController
    {
        private string Username
        {
            get
            {
                return Request.Properties["username"].ToString();
            }
        }

        public IHttpActionResult Get()
        {
            var contacts = Services.PhonebookContext.Instance.Contacts.AsQueryable()
                .Where(_ => _.Owner.Username.ToLower() == Username.ToLower())
                .ToList(); // execute query here

            return Ok(contacts);
        }

        [Route("{id}")]
        public IHttpActionResult Get([FromUri] Guid id)
        {
            var contacts = Services.PhonebookContext.Instance.Contacts.AsQueryable()
                .Where(_ => _.Owner.Username.ToLower() == Username.ToLower() && _.Id == id)
                .ToList(); // execute query here

            return Ok(contacts);
        }
    }
}
