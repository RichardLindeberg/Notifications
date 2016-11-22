namespace Notifications.Messages.Events.Person
{
    public class PersonRemoved : PersonEvent
    {
        public PersonRemoved(string personalNumber)
            : base(personalNumber)
        {
        }
    }
}