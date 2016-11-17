namespace Notifications.Domain.UnitTests.Person
{
    using System.Collections.Generic;

    using Domain;

    using Messages;

    public abstract class PersonTestBase
    {
        protected Person Sut { get; set; }

        public void CreateSut(string pno)
        {
            Sut = new Person(pno, null);
            Sut.Apply(GetEvents());
        }

        public abstract IEnumerable<PersonEvent> GetEvents();
    }
}
