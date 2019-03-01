using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Entity;
using System.Web.Http;
using ExpressMapper;

namespace API.Controllers
{
    [RoutePrefix("api/phonebook")]
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

        [Route("")]
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

        [Route("")]
        public IHttpActionResult Post([FromBody] DTO.Contact.Create model)
        {
            var domain = Mapper.Map<DTO.Contact.Create, Models.Contact>(model);


            domain.Owner = Services.PhonebookContext.Instance.Users.FirstOrDefault(_ => _.Username.ToLower() == Username.ToLower());

            Services.PhonebookContext.Instance.Users.Attach(domain.Owner);

            Services.PhonebookContext.Instance.Contacts.Add(domain);

            var result = Services.PhonebookContext.Instance.SaveChanges();

            if (result > 0)
            {
                var dto = Mapper.Map<Models.Contact, DTO.Contact.Read>(domain);

                return Ok(dto);
            }
            else
            {

                return StatusCode(System.Net.HttpStatusCode.ExpectationFailed);
            }
        }

        [Route("{id}")]
        public IHttpActionResult Delete([FromUri] Guid id)
        {
            var contactQuery = Services.PhonebookContext.Instance.Contacts.AsQueryable().Where(_ => _.Id == id && _.Owner.Username.ToLower() == Username.ToLower());

            if (!contactQuery.Any())
            {
                return NotFound();
            }

            var contact = contactQuery.Include(_ => _.Fields).SingleOrDefault();


            Services.PhonebookContext.Instance.Fields.RemoveRange(contact.Fields);

            Services.PhonebookContext.Instance.Contacts.Remove(contact);


            var result = Services.PhonebookContext.Instance.SaveChanges();

            if (result > 0)
            {
                return Ok();
            }
            else
            {

                return StatusCode(System.Net.HttpStatusCode.ExpectationFailed);
            }
        }
    }
}
