namespace Notifications.Messages
{
    public class FirebaseTokenAdded : PersonEvent
    {
        public FirebaseTokenAdded(string personalNumber, string firebaseToken)
            : base(personalNumber)
        {
            FirebaseToken = firebaseToken;
        }

        public string FirebaseToken { get; }
    }
}