using Microsoft.VisualStudio.TestTools.UnitTesting;
using XMLParserV1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLParserV1.Tests
{
    [TestClass()]
    public class MemberOfParliamentTests
    {
        [TestMethod()]
        public void CalculateFullAmmount_PaymentSum_Pass()
        {
            // Arrange

            List<string> date = new List<string>();
            date.Add("test date 15/02/2023");

            List<int> payments = new List<int>();
            payments.Add(25);
            payments.Add(100);
            payments.Add(100);

            List<string> donnorNames = new List<string>();
            donnorNames.Add("Steve");
            donnorNames.Add("Bob");
            donnorNames.Add("Puskin");

            // Act
            MemberOfParliament testMember = new MemberOfParliament("Harun", "32123", date, payments, donnorNames);

            // Assert
            Assert.AreEqual(225, testMember.FullAmountReceived);

        }

        [TestMethod()]
        public void MemberOfParliament_NullCheck_Pass()
        {
            List<string> date = new List<string>();
            date.Add("test date 15/02/2023");

            List<int> payments = new List<int>();
            payments.Add(25);
            payments.Add(100);
            payments.Add(100);

            List<string> donnorNames = new List<string>();
            donnorNames.Add("Steve");
            donnorNames.Add("Bob");
            donnorNames.Add("Puskin");

            // Act
            MemberOfParliament testMember = new MemberOfParliament("Harun", "32123", date, payments, donnorNames);

            // Assert
            Assert.IsNotNull(testMember);
        }
        [TestMethod()]
        public void MemberOfParliament_TypeCheck_Pass()
        {
            List<string> date = new List<string>();
            date.Add("test date 15/02/2023");

            List<int> payments = new List<int>();
            payments.Add(25);
            payments.Add(100);
            payments.Add(100);

            List<string> donnorNames = new List<string>();
            donnorNames.Add("Steve");
            donnorNames.Add("Bob");
            donnorNames.Add("Puskin");

            // Act
            MemberOfParliament testMember = new MemberOfParliament("Harun", "32123", date, payments, donnorNames);

            // Assert
            Assert.IsInstanceOfType(testMember, typeof(MemberOfParliament));
        }
        public void MemberOfParliament_valueCheck_Pass()
        {
            List<string> date = new List<string>();
            date.Add("test date 15/02/2023");

            List<int> payments = new List<int>();
            payments.Add(25);
            payments.Add(100);
            payments.Add(100);

            List<string> donnorNames = new List<string>();
            donnorNames.Add("Steve");
            donnorNames.Add("Bob");
            donnorNames.Add("Puskin");

            // Act
            MemberOfParliament testMember = new MemberOfParliament("Harun", "32123", date, payments, donnorNames);

            // Assert
            Assert.AreEqual(testMember.Name, "Harun");
            Assert.AreEqual(testMember.Id, "32123");
            Assert.AreEqual(testMember.PaymentDateReceived, date);
            Assert.AreEqual(testMember.PaymentsReceived, payments);
            Assert.AreEqual(testMember.DonnorNames, donnorNames);
        }
    }
}