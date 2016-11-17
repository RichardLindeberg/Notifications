namespace Notifications.Messages
{
    public class PersonRemoved : PersonEvent
    {
        public PersonRemoved(string personalNumber)
            : base(personalNumber)
        {
        }
    }
}