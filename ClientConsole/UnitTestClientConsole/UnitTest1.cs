using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClientConsole;

namespace UnitTestClientConsole
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestRegistration()
        {
            Registration reg = new Registration();
            reg.User = new USER() { Login = "test", Password = "test", Nick = "test" };
            reg.CreateUser().Wait();

            Assert.AreEqual(true, reg.IsCreated);
        }

        [TestMethod]
        public void TestLogIn()
        {
            Program.LoggedInUser.Login = "admin";
            Program.LoggedInUser.Password = "admin";
            
            LogIn log = new LogIn();
            log.IsLoginValid = false;
            log.CheckLogin().Wait();

            Assert.AreEqual(true, log.IsLoginValid);
        }
    }
}
