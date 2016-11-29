using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Notifications.Domain.ReadModell;

namespace Notifications.Domain.UnitTests
{
    using Messages.Events.Person;

    using NUnit.Framework;

    using Should;

    //[TestFixture]
    //public class PersonalNumberAndTokenReadModellTests
    //{
    //    [Test]
    //    public void AddingOneShouldHaveOne()
    //    {
    //        var pno = "800412xxxx";
    //        var token = "abcde";
    //        var sut = new PersonalNumberAndTokenReadModell();
    //        var addEvt = new FirebaseTokenAdded(pno, token, "not1");
    //        sut.Handle(addEvt);

    //        var actual = sut.PersonalNumberWithThisToken(token);
    //        actual.Count.ShouldEqual(1);
    //        actual.ShouldContain(pno);
    //    }

    //    [Test]
    //    public void AddingSameTwiceShouldHaveOne()
    //    {
    //        var pno = "800412xxxx";
    //        var token = "abcde";
    //        var sut = new PersonalNumberAndTokenReadModell();
    //        var addEvt = new FirebaseTokenAdded(pno, token, "not1");
    //        sut.Handle(addEvt);
    //        sut.Handle(addEvt);

    //        var actual = sut.PersonalNumberWithThisToken(token);
    //        actual.Count.ShouldEqual(1);
    //        actual.ShouldContain(pno);
    //    }

    //    [Test]
    //    public void AddingTwoSameTokenShouldHaveTwoPeople()
    //    {
    //        var pno = "800412xxxx";
    //        var pno2 = "810412YYYY";
    //        var token = "abcde";
    //        var sut = new PersonalNumberAndTokenReadModell();
    //        var addEvt = new FirebaseTokenAdded(pno, token, "not1");
    //        var addEvt2 = new FirebaseTokenAdded(pno2, token, "not1");
    //        sut.Handle(addEvt);
    //        sut.Handle(addEvt2);

    //        var actual = sut.PersonalNumberWithThisToken(token);
    //        actual.Count.ShouldEqual(2);
    //        actual.ShouldContain(pno);
    //        actual.ShouldContain(pno2);
    //    }

    //    [Test]
    //    public void AddingTwoSameTokenRemoveOneShouldHaveOnePerson()
    //    {
    //        var pno = "800412xxxx";
    //        var pno2 = "810412YYYY";
    //        var token = "abcde";
    //        var sut = new PersonalNumberAndTokenReadModell();
    //        var addEvt = new FirebaseTokenAdded(pno, token, "not1");
    //        var addEvt2 = new FirebaseTokenAdded(pno2, token, "not1");
    //        var removeEvt = new FirebaseTokenRemoved(pno, token, "not1");
    //        sut.Handle(addEvt);
    //        sut.Handle(addEvt2);
    //        sut.Handle(removeEvt);

    //        var actual = sut.PersonalNumberWithThisToken(token);
    //        actual.Count.ShouldEqual(1);
    //        actual.ShouldContain(pno2);
    //    }

    //    [Test]
    //    public void AddingTwoSameTokenRemoveOneShouldHaveOnePerson_2()
    //    {
    //        var pno = "800412xxxx";
    //        var pno2 = "810412YYYY";
    //        var token = "abcde";
    //        var sut = new PersonalNumberAndTokenReadModell();
    //        var addEvt = new FirebaseTokenAdded(pno, token, "not1");
    //        var addEvt2 = new FirebaseTokenAdded(pno2, token, "not1");
    //        var removeEvt = new FirebaseTokenRemoved(pno2, token, "not1");
    //        sut.Handle(addEvt);
    //        sut.Handle(addEvt2);
    //        sut.Handle(removeEvt);

    //        var actual = sut.PersonalNumberWithThisToken(token);
    //        actual.Count.ShouldEqual(1);
    //        actual.ShouldContain(pno);
    //    }

    //    [Test]
    //    public void AddingTwoDifferentTokenShouldHaveOnePerson()
    //    {
    //        var pno = "800412xxxx";
    //        var pno2 = "810412YYYY";
    //        var token = "abcde";
    //        var token2 = "YXYXY";
    //        var sut = new PersonalNumberAndTokenReadModell();
    //        var addEvt = new FirebaseTokenAdded(pno, token, "not1");
    //        var addEvt2 = new FirebaseTokenAdded(pno2, token2, "not1");
    //        sut.Handle(addEvt);
    //        sut.Handle(addEvt2);

    //        var actual = sut.PersonalNumberWithThisToken(token);
    //        actual.Count.ShouldEqual(1);
    //        actual.ShouldContain(pno);
    //        actual.ShouldNotContain(pno2);
    //    }

    //    [Test]
    //    public void AddingTwoDifferentTokenShouldHaveOnePersonAlsoForSecondToken()
    //    {
    //        var pno = "800412xxxx";
    //        var pno2 = "810412YYYY";
    //        var token = "abcde";
    //        var token2 = "YXYXY";
    //        var sut = new PersonalNumberAndTokenReadModell();
    //        var addEvt = new FirebaseTokenAdded(pno, token, "not1");
    //        var addEvt2 = new FirebaseTokenAdded(pno2, token2, "not1");
    //        sut.Handle(addEvt);
    //        sut.Handle(addEvt2);

    //        var actual = sut.PersonalNumberWithThisToken(token2);
    //        actual.Count.ShouldEqual(1);
    //        actual.ShouldNotContain(pno);
    //        actual.ShouldContain(pno2);
    //    }

    //}
}
