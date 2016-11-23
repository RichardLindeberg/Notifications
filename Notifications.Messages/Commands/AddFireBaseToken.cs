using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Messages.Commands
{
    public class AddFireBaseTokenCommand : IPersonCommand
    {
        public AddFireBaseTokenCommand(string personalNumber, string firebaseToken, Guid commandId, string notificationTypeId)
        {
            PersonalNumber = personalNumber;
            FirebaseToken = firebaseToken;
            CommandId = commandId;
            NotificationTypeId = notificationTypeId;
        }

        public Guid CommandId { get; set; }

        public string PersonalNumber { get; set; }

        public string FirebaseToken { get; set; }

        public string NotificationTypeId { get; }
    }
}
