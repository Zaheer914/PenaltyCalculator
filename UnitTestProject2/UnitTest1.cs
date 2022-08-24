using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PenaltyCalculator;
using PenaltyCalculator.Controllers;

namespace UnitTestProject2
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void WorkingDaysofYears()
        {
          Countries country = new Countries();
          DateTime issueDate = new DateTime(2022, 1, 1);
          DateTime returnDate = new DateTime(2022, 12, 31);
          List<string> weekholiday = new List<string>();
          weekholiday.Add("Saturday");
          weekholiday.Add("Sunday");
          var data = country.WorkingDays(weekholiday, issueDate.ToString(), returnDate.ToString());
          Assert.AreEqual(260,data);
        }
        [TestMethod]
        public void WorkingDayswithFriday()
        {
          Countries country = new Countries();
          DateTime issueDate = new DateTime(2022, 1, 1);
          DateTime returnDate = new DateTime(2022, 1, 1);
          List<string> weekholiday = new List<string>();
          weekholiday.Add("Saturday");
          weekholiday.Add("Friday");
          var data = country.WorkingDays(weekholiday, issueDate.ToString(), returnDate.ToString());
          Assert.AreEqual(0, data);
        }
         
        
  }
}
