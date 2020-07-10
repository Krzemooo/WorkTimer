using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WorkTimer.Model;

namespace UnitTests
{
    [TestClass]
    public class MethodTest
    {
        readonly UserModel _userModel = new UserModel()
        {
            Name = "Unit",
            Surname = "Test"
        };
        [TestMethod()]
        public void Test1()
        {
            WorkTimer.Core.DataCore dataCore = new WorkTimer.Core.DataCore();
            bool testResult = dataCore.GenerateNewFile(DateTime.Now, _userModel);

            Assert.IsTrue(testResult);
        }
        [TestMethod()]
        public void Test2()
        {
            WorkTimer.Core.DataCore dataCore = new WorkTimer.Core.DataCore();
            dataCore.AppendNewLine(DateTime.Now.AddSeconds(2), 2, _userModel);
            List<UserWorkTime> testResult = dataCore.GetUserWorkTime(_userModel);
            UserWorkTime singleElement = testResult.Find(s => s.DayStamp == DateTime.Now.Date);
            Assert.AreEqual(singleElement.DataWork, TimeSpan.FromSeconds(2));
        }

        [TestMethod()]
        public void Test3()
        {
            WorkTimer.Core.DataCore dataCore = new WorkTimer.Core.DataCore();
            bool testResult = dataCore.AppendNewLine(DateTime.Now.AddSeconds(2), 2, _userModel);

            Assert.IsTrue(testResult);
        }
    }
}
