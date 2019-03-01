using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Entity;
using System.Web.Http;
using ExpressMapper;

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
            var query = Services.PhonebookContext.Instance.Contacts.AsQueryable()
                .Where(_ => _.Owner.Username.ToLower() == Username.ToLower())
                .Include(_ => _.Fields); // create query

            var contacts = Mapper.Map<IQueryable<Models.Contact>, List<DTO.Contact.Read>>(query);

            return Ok(contacts);
        }

        [Route("{id}")]
        public IHttpActionResult Get([FromUri] Guid id)
        {
            var contactModel = Services.PhonebookContext.Instance.Contacts.AsQueryable()
                .Where(_ => _.Owner.Username.ToLower() == Username.ToLower() && _.Id == id)
                .FirstOrDefault();

            if (contactModel == null)
            {
                return NotFound();
            }

            var dto = Mapper.Map<Models.Contact, DTO.Contact.Read>(contactModel);

            return Ok(dto);
        }
    }
}
