using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using IssueTracker.Controllers;
using Assert = NUnit.Framework.Assert;

namespace IssueTracker.Tests
{
    [TestClass]
    public class AccountControllerTests
    {
        [TestMethod]
        public void TestLogOff()
        {
            IAuth auth = new StubbyAuthWrapper();
            var accountController = new AccountController(auth);
            var redirectToRouteResult = accountController.LogOff() as RedirectToRouteResult;

            Assert.NotNull(redirectToRouteResult);
            Assert.AreEqual("Index", redirectToRouteResult.RouteValues["Action"]);
            Assert.AreEqual("Home", redirectToRouteResult.RouteValues["controller"]);
        }
    }
}
