namespace Notifications.Domain
{
    using System.Collections.Generic;

    public class PersonWithToken
    {
        public PersonWithToken(string personalNumber, string token)
        {
            PersonalNumber = personalNumber;
            Token = token;
        }
        

        private sealed class PersonalNumberTokenEqualityComparer : IEqualityComparer<PersonWithToken>
        {
            public bool Equals(PersonWithToken x, PersonWithToken y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return string.Equals(x.PersonalNumber, y.PersonalNumber) && string.Equals(x.Token, y.Token);
            }

            public int GetHashCode(PersonWithToken obj)
            {
                unchecked
                {
                    return ((obj.PersonalNumber != null ? obj.PersonalNumber.GetHashCode() : 0) * 397) ^ (obj.Token != null ? obj.Token.GetHashCode() : 0);
                }
            }
        }


        private static readonly IEqualityComparer<PersonWithToken> PersonalNumberTokenComparerInstance = new PersonalNumberTokenEqualityComparer();

        public static IEqualityComparer<PersonWithToken> PersonalNumberTokenComparer
        {
            get
            {
                return PersonalNumberTokenComparerInstance;
            }
        }

        protected bool Equals(PersonWithToken other)
        {
            return string.Equals(PersonalNumber, other.PersonalNumber) && string.Equals(Token, other.Token);
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
                return ((PersonalNumber != null ? PersonalNumber.GetHashCode() : 0) * 397) ^ (Token != null ? Token.GetHashCode() : 0);
            }
        }

        public string PersonalNumber { get; }

        public string Token { get; }
    }
}