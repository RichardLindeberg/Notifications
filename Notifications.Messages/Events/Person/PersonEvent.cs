namespace Notifications.Messages.Events.Person
{
    public class PersonEvent : Event
    {
        public PersonEvent(string personalNumber)
        {
            PersonalNumber = personalNumber;
        }

        public string PersonalNumber { get; }
    }
}