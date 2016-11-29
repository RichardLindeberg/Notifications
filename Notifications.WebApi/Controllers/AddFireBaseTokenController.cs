namespace Notifications.WebApi.Controllers
{
    using System;
    using System.Web.Http;

    using Notifications.Messages.Commands;
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
}