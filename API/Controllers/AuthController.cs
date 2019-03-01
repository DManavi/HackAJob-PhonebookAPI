using System.Linq;
using System.Web.Http;

namespace API.Controllers
{
    public class AuthController : ApiController
    {
        [HttpPost]
        public IHttpActionResult Login([FromBody] DTO.Auth.Login model)
        {
            bool isValid = Services.PhonebookContext.Instance.Users.Any(_ => _.Username.ToLower() == model.Username.ToLower() && _.Password == model.Password && !_.Disabled);

            if (!isValid)
            {
                return StatusCode(System.Net.HttpStatusCode.Forbidden);
            }

            var token = Services.Jwt.Create(model.Username.ToLower());

            return Ok(token);
        }
    }
}
