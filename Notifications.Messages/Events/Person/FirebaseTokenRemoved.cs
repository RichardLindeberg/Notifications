namespace Notifications.Messages.Events.Person
{
    public class FirebaseTokenRemoved : PersonEvent
    {
        public FirebaseTokenRemoved(string personalNumber, string firebaseToken)
            : base(personalNumber)
        {
            FirebaseToken = firebaseToken;
        }

        public string FirebaseToken { get; }
    }
}