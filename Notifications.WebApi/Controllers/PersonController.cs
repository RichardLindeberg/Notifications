namespace Notifications.WebApi.Controllers
{
    using System;
    using System.Net;
    using System.Web.Http;

    using Messages.Commands;

    using Notifications.Domain.ReadModell;
    using Notifications.Storage;

    public class AddFireBaseTokenController : ApiController
    {
        [Route("AddFireBaseToken")]
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
    }

    public class RemoveFireBaseTokenController : ApiController
    {
        [Route("AddFireBaseToken")]
        [HttpPost]
        public IHttpActionResult AddToken(RemoveFireBaseTokenCommand command)
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
