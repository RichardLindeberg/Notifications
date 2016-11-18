using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Notifications.WebApi.Controllers
{
    using Notifications.Messages.Commands;

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
