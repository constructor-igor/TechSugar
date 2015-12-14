using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace DesignSamples
{
    [TestFixture]
    public class BankAccountTests
    {
        [Test]
        public void TestWithdrawFromAccount_EnoughMoney()
        {
            MockLogging mockLogging = new MockLogging();
            MockBank mockBank = new MockBank("123123", 600);
            BankAccount b = new BankAccount("123123", mockLogging, mockBank);
            b.Withdraw(500);

            Assert.That(mockBank.RequiredSumma, Is.EqualTo(500));
        }
        [Test]
        public void TestWithdrawFromAccount_NotEnoughMoney()
        {
            MockLogging mockLogging = new MockLogging();
            MockBank mockBank = new MockBank("123123", 400);
            BankAccount b = new BankAccount("123123", mockLogging, mockBank);
            Assert.That(()=>b.Withdraw(500), Throws.ArgumentException);
        }
        [Test]
        public void TestWithdrawFromAccount_InvalidAccount()
        {
            MockLogging mockLogging = new MockLogging();
            MockBank mockBank = new MockBank("123123", 600);
            BankAccount b = new BankAccount("123123-Invalid", mockLogging, mockBank);
            Assert.That(() => b.Withdraw(500), Throws.ArgumentException);
        }

        public interface IBank
        {
            double GetTotal(string accountId);
            void Withdraw(string accountId, int requiredSumma);
        }
        public class MockBank : IBank
        {
            private readonly string m_accountId;
            private readonly double m_total;

            public int RequiredSumma { get; set; }

            public MockBank(string accountId, double total)
            {
                m_accountId = accountId;
                m_total = total;
            }

            #region Implementation of IBank
            public double GetTotal(string accountId)
            {
                if (accountId == m_accountId)
                    return m_total;                
                throw new ArgumentException();
            }
            public void Withdraw(string accountId, int requiredSumma)
            {
                if (accountId != m_accountId)
                    throw new ArgumentException();
                RequiredSumma = requiredSumma;                
            }
            #endregion
        }

        public interface ILogging
        {
            void ToLog(string message);
        }
        public class MockLogging : ILogging
        {
            public List<string> MessagesList { get; private set; }
            public MockLogging()
            {
                MessagesList = new List<string>();
            }

            #region ILogging
            public void ToLog(string message)
            {
                MessagesList.Add(message);
            }
            #endregion
        }

        public class BankAccount
        {
            private readonly ILogging m_logging;
            private readonly IBank m_bank;
            public string AccountId { get; set; }
            public BankAccount(string accountId, ILogging logging, IBank bank)
            {
                m_logging = logging;
                m_bank = bank;
                AccountId = accountId;
            }

            public void Withdraw(int requiredSumma)
            {
                double total = m_bank.GetTotal(AccountId);
                if (total<requiredSumma)
                    throw new ArgumentException();
                total -= requiredSumma;
                m_bank.Withdraw(AccountId, requiredSumma);
            }
        }
    }
}
