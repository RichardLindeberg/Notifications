namespace Notifications.Domain
{
    using System.Collections.Generic;

    public class FirebaseTokenAndNotificationTypeId
    {
        public FirebaseTokenAndNotificationTypeId(string firebaseToken, string notificationTypeId)
        {
            FirebaseToken = firebaseToken;
            NotificationTypeId = notificationTypeId;
        }

        public string FirebaseToken { get; }

        public string NotificationTypeId { get; }

        protected bool Equals(FirebaseTokenAndNotificationTypeId other)
        {
            return string.Equals(FirebaseToken, other.FirebaseToken) && string.Equals(NotificationTypeId, other.NotificationTypeId);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((FirebaseTokenAndNotificationTypeId)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((FirebaseToken != null ? FirebaseToken.GetHashCode() : 0) * 397) ^ (NotificationTypeId != null ? NotificationTypeId.GetHashCode() : 0);
            }
        }

        private sealed class FirebaseTokenAndNotificationEqualityComparer : IEqualityComparer<FirebaseTokenAndNotificationTypeId>
        {
            public bool Equals(FirebaseTokenAndNotificationTypeId x, FirebaseTokenAndNotificationTypeId y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return string.Equals(x.FirebaseToken, y.FirebaseToken) && string.Equals(x.NotificationTypeId, y.NotificationTypeId);
            }

            public int GetHashCode(FirebaseTokenAndNotificationTypeId obj)
            {
                unchecked
                {
                    return ((obj.FirebaseToken != null ? obj.FirebaseToken.GetHashCode() : 0) * 397) ^ (obj.NotificationTypeId != null ? obj.NotificationTypeId.GetHashCode() : 0);
                }
            }
        }

        private static readonly IEqualityComparer<FirebaseTokenAndNotificationTypeId> FirebaseTokenAndNotificationComparerInstance = new FirebaseTokenAndNotificationEqualityComparer();

        public static IEqualityComparer<FirebaseTokenAndNotificationTypeId> FirebaseTokenAndNotificationComparer
        {
            get
            {
                return FirebaseTokenAndNotificationComparerInstance;
            }
        }
    }
}