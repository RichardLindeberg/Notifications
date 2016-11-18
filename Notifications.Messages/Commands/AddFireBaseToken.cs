using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Messages.Commands
{
    public class AddFireBaseTokenCommand : IPersonCommand
    {
        public AddFireBaseTokenCommand(string personalNumber, string firebaseToken, Guid commandId)
        {
            PersonalNumber = personalNumber;
            FirebaseToken = firebaseToken;
            CommandId = commandId;
        }

        public Guid CommandId { get; set; }

        public string PersonalNumber { get; set; }

        public string FirebaseToken { get; set; }
    }
}
