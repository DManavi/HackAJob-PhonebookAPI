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

        private Services.PhonebookContext _DatabaseContext;

        private Services.PhonebookContext DatabaseContext
        {
            get
            {
                if(_DatabaseContext == null)
                {
                    _DatabaseContext = Services.PhonebookContext.Instance;
                }

                return _DatabaseContext;
            }
        }

        [Route("")]
        public IHttpActionResult Get()
        {
            var query = DatabaseContext.Contacts.AsQueryable()
                .Where(_ => _.Owner.Username.ToLower() == Username.ToLower())
                .Include(_ => _.Fields); // create query

            var contacts = Mapper.Map<IQueryable<Models.Contact>, List<DTO.Contact.Read>>(query);

            return Ok(contacts);
        }

        [Route("{id}")]
        public IHttpActionResult Get([FromUri] Guid id)
        {
            var contactModel = DatabaseContext.Contacts.AsQueryable()
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


            domain.Owner = DatabaseContext.Users.FirstOrDefault(_ => _.Username.ToLower() == Username.ToLower());

            DatabaseContext.Users.Attach(domain.Owner);

            DatabaseContext.Contacts.Add(domain);

            var result = DatabaseContext.SaveChanges();

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
        public IHttpActionResult Put([FromUri] Guid id, [FromBody] DTO.Contact.Update model)
        {
            var contactQuery = DatabaseContext.Contacts.AsQueryable().Where(_ => _.Id == id && _.Owner.Username.ToLower() == Username.ToLower());

            if (!contactQuery.Any())
            {
                return NotFound();
            }

            using (var transaction = DatabaseContext.Database.BeginTransaction())
            {
                // mark current fields as deleted
                var currentFields = DatabaseContext.Fields.Where(_ => _.Contact.Id == id);

                DatabaseContext.Fields.RemoveRange(currentFields);

                DatabaseContext.SaveChanges();

                // add new fields

                var domain = DatabaseContext.Contacts.SingleOrDefault(_ => _.Id == id);

                domain.Fields = Mapper.Map<IEnumerable<DTO.Field.Read>, List<Models.Field>>(model.Fields);

                DatabaseContext.SaveChanges();


                var dto = Mapper.Map<Models.Contact, DTO.Contact.Read>(domain);


                transaction.Commit();

                return Ok(dto);
            }
        }

        [Route("{id}")]
        public IHttpActionResult Delete([FromUri] Guid id)
        {
            var contactQuery = DatabaseContext.Contacts.AsQueryable().Where(_ => _.Id == id && _.Owner.Username.ToLower() == Username.ToLower());

            if (!contactQuery.Any())
            {
                return NotFound();
            }

            var contact = contactQuery.Include(_ => _.Fields).SingleOrDefault();


            DatabaseContext.Fields.RemoveRange(contact.Fields);

            DatabaseContext.Contacts.Remove(contact);


            var result = DatabaseContext.SaveChanges();

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
