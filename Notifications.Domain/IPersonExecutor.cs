namespace Notifications.Domain
{
    using System;
    using System.Linq;


    public interface IPersonExecutor
    {
        void Execute(string personalNumber, Action<Person> action, Guid commitId);
    }

 
}