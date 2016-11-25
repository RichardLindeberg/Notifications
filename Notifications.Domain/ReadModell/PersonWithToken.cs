using System.Collections.Generic;

namespace Notifications.Domain.ReadModell
{
    public class PersonWithToken
    {
        public PersonWithToken(string personalNumber, string token, string notificationTypeId)
        {
            PersonalNumber = personalNumber;
            Token = token;
            NotificationTypeId = notificationTypeId;
        }

        public string PersonalNumber { get; }

        public string Token { get; }

        public string NotificationTypeId { get; }

        private sealed class PersonWithTokenEqualityComparer : IEqualityComparer<PersonWithToken>
        {
            public bool Equals(PersonWithToken x, PersonWithToken y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return string.Equals(x.PersonalNumber, y.PersonalNumber) && string.Equals(x.Token, y.Token) && string.Equals(x.NotificationTypeId, y.NotificationTypeId);
            }

            public int GetHashCode(PersonWithToken obj)
            {
                unchecked
                {
                    var hashCode = (obj.PersonalNumber != null ? obj.PersonalNumber.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (obj.Token != null ? obj.Token.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (obj.NotificationTypeId != null ? obj.NotificationTypeId.GetHashCode() : 0);
                    return hashCode;
                }
            }
        }

        private static readonly IEqualityComparer<PersonWithToken> PersonWithTokenComparerInstance = new PersonWithTokenEqualityComparer();

        public static IEqualityComparer<PersonWithToken> PersonWithTokenComparer
        {
            get
            {
                return PersonWithTokenComparerInstance;
            }
        }

        protected bool Equals(PersonWithToken other)
        {
            return string.Equals(PersonalNumber, other.PersonalNumber) && string.Equals(Token, other.Token) && string.Equals(NotificationTypeId, other.NotificationTypeId);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((PersonWithToken)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (PersonalNumber != null ? PersonalNumber.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Token != null ? Token.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (NotificationTypeId != null ? NotificationTypeId.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}