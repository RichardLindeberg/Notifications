namespace Notifications.WebApi.Controllers
{
    using System;
    using System.Net;
    using System.Web.Http;

    using Messages.Commands;

    public class PersonController : ApiController
    {
        [Route("Person")]
        [HttpGet]
        public IHttpActionResult Get()
        {
            return Content(HttpStatusCode.Found, FakedDi.PersonalNumberAndTokenReadModell.PeopleWithTokens);
        }

        [Route("Person/AddToken")]
        [HttpPost]
        public IHttpActionResult AddToken(AddFireBaseTokenCommand command)
        {
            if (command == null)
            {
                return BadRequest("No command");
            }

            try
            {
                FakedDi.GetPersonCommandHandler.Handle(command);
            }
            catch (Exception)
            {
                
                throw;
            }
            return Ok();
        }

        [Route("Person/RemoveToken")]
        [HttpPost]
        public IHttpActionResult RemoveToken(RemoveFireBaseTokenCommand command)
        {
            if (command == null)
            {
                return BadRequest("No command");
            }

            try
            {
                FakedDi.GetPersonCommandHandler.Handle(command);
            }
            catch (Exception)
            {

                throw;
            }

            return Ok();
        }
    }
}
