namespace Notifications.Messages
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