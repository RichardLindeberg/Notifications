namespace Notifications.WebApi.Controllers
{
    using System.Net;
    using System.Web.Http;

    using Notifications.Domain.ReadModell;
    using Notifications.Storage;

    public class PersonController : ApiController
    {
        [Route("Person")]
        [HttpGet]
        public IHttpActionResult Get()
        {
            return Content(HttpStatusCode.Found, FakedDi.PersonalNumberAndTokenReadModell.GetAll());
        }

        [Route("Person/{personalNumber}")]
        [HttpGet]
        public IHttpActionResult Get(string personalNumber)
        {
            return Content(HttpStatusCode.Found, FakedDi.PersonalNumberAndTokenReadModell.GetPerson(personalNumber));
        }



    }
}
