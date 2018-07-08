using System;
using System.Linq;
using NUnit.Framework;
using WorldWideBank.Data;
using WorldWideBank.Repositories;
using WorldWideBank.Services;

namespace WorldWideBank.Tester
{
    /// <summary>
    /// A tester class to perform unit test.
    /// </summary>
    [TestFixture]
    public class Tester
    {
        #region Variables.

        // Account service required for tester to perform account related tasks.
        private readonly AccountService _accountService;
        // Customer service required for tester to perform customer related tasks.
        private readonly CustomerService _customerService;

        #endregion

        #region PositiveTesting.

        /// <summary>
        /// Positive test method to add and test customers.
        /// </summary>
        /// <param name="customerId">The identifier.</param>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        [TestCase("900", "Jhon", "Kelly")]
        [TestCase("901", "Greg", "Graham")]
        public void AddCustomer_Positive(string customerId, string firstName, string lastName)
        {
            _customerService.AddCustomer(customerId, firstName, lastName);
            Assert.IsTrue(Repository.CustomerRepository.Any(c => string.Equals(c.CustomerId, customerId, StringComparison.CurrentCultureIgnoreCase)));
        }

        /// <summary>
        /// Positive test method to create and add account.
        /// </summary>
        /// <param name="accountNumber">The account number.</param>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="currencyCode">The currency code.</param>
        /// <param name="initialBalance">The initial balance.</param>
        /// <param name="mimimumBalance">The mimimum balance.</param>
        [TestCase("9000", "900", Repository.CurrencyCodeCad, 9500, 0)]
        [TestCase("9001", "900", Repository.CurrencyCodeCad, 2500, 0)]
        [TestCase("9500", "901", Repository.CurrencyCodeCad, 50500, 0)]
        public void CreateAccount_Positive(string accountNumber, string customerId, string currencyCode, decimal initialBalance, decimal mimimumBalance)
        {
            _accountService.CreateGeneralAccount(accountNumber, customerId, currencyCode, initialBalance, mimimumBalance);
        }

        /// <summary>
        /// Test scenario 1 performs
        /// Stewie Griffin deposits $300.00 USD to account number 1234.
        /// </summary>
        [Test]
        public void TesteCase1()
        {
            PerformTransaction("777", null, "1234", Repository.CurrencyCodeUsd, 300, TransactionType.Deposite);
            _accountService.DisplayAccountCurrentBalance("1234");
        }


        /// <summary>
        /// Test scenario 2 performs
        /// Glenn Quagmire withdraws $5,000.00 MXN from account number 2001.
        /// Glenn Quagmire withdraws $12,500.00 USD from account number 2001.
        /// Glenn Quagmire deposits $300.00 CAD to account number 2001.
        /// </summary>
        [Test]
        public void TesteCase2()
        {
            PerformTransaction("504", "2001", null, Repository.CurrencyCodeMxn, 5000, TransactionType.Withdrawal);
            PerformTransaction("504", "2001", null, Repository.CurrencyCodeUsd, 12500, TransactionType.Withdrawal);
            PerformTransaction("504", null, "2001", Repository.CurrencyCodeCad, 300, TransactionType.Deposite);
            _accountService.DisplayAccountCurrentBalance("2001");
        }

        /// <summary>
        /// Test scenario 3 performs
        /// Joe Swanson withdraws $5,000.00 CAD from account number 5500.
        /// Joe Swanson transfers $7,300.00 CAD from account number 1010 to account number 5500.
        /// Joe Swanson deposits $13,726.00 MXN to account number 1010.
        /// </summary>
        [Test]
        public void TesteCase3()
        {
            PerformTransaction("002", "5500", null, Repository.CurrencyCodeCad, 5000, TransactionType.Withdrawal);
            PerformTransaction("002", "1010", "5500", Repository.CurrencyCodeCad, 7300, TransactionType.Transfer);
            PerformTransaction("002", null, "1010", Repository.CurrencyCodeMxn, 13726, TransactionType.Deposite);
            _accountService.DisplayAccountCurrentBalance("1010");
            _accountService.DisplayAccountCurrentBalance("5500");
        }

        /// <summary>
        /// Test scenario 4 performs
        /// Peter Griffin withdraws $70.00 USD from account number 0123.
        /// Lois Griffin deposits $23,789.00 USD to account number 0456.
        /// Lois Griffin transfers $23.75 CAD from account number 0456 to Peter Griffin(account number 0123).
        /// </summary>
        [Test]
        public void TesteCase4()
        {
            PerformTransaction("123", "0123", null, Repository.CurrencyCodeUsd, 70m, TransactionType.Withdrawal);
            PerformTransaction("456", null, "0456", Repository.CurrencyCodeUsd, 23789m, TransactionType.Deposite);
            PerformTransaction("456", "0456", "0123", Repository.CurrencyCodeCad, 23.75m, TransactionType.Transfer);
            _accountService.DisplayAccountCurrentBalance("0123");
            _accountService.DisplayAccountCurrentBalance("0456");
        }

        #endregion

        #region NegativeTesting

        /// <summary>
        /// Negative test method to add and test customers.
        /// </summary>
        /// <param name="customerId">The identifier.</param>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        [TestCase(null, null, null)]
        [TestCase("504", null, null)]
        [TestCase("504", "Joe", null)]
        [TestCase("504", "Joe", null)]
        [TestCase("", "", "Griffin")]
        public void AddCustomer_Negative(string customerId, string firstName, string lastName)
        {
            _customerService.AddCustomer(customerId, firstName, lastName);
        }

        /// <summary>
        /// Negative test method to create and add account.
        /// </summary>
        /// <param name="accountNumber">The account number.</param>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="currencyCode">The currency code.</param>
        /// <param name="initialBalance">The initial balance.</param>
        /// <param name="mimimumBalance">The mimimum balance.</param>
        [TestCase(null, null, null, null, null)]
        [TestCase("", "504", Repository.CurrencyCodeCad, 35000.00, 0)]
        [TestCase("1010", "002", Repository.CurrencyCodeCad, null, 0)]
        [TestCase("1010", "002", Repository.CurrencyCodeCad, 15000.00, null)]
        [TestCase("0123", "123", null, 150.00, 0)]
        [TestCase("0456", "456", Repository.CurrencyCodeCad, -65000.00, 0)]
        [TestCase("0456", "456", Repository.CurrencyCodeCad, 6500, -1)]
        public void CreateAccount_Negative(string accountNumber, string customerId, string currencyCode, decimal? initialBalance, decimal? mimimumBalance)
        {
            _accountService.CreateGeneralAccount(accountNumber, customerId, currencyCode, initialBalance, mimimumBalance);
        }

        [TestCase(null, null, null, null, null, null)]
        [TestCase("", "0123", null, Repository.CurrencyCodeUsd, 70, TransactionType.Withdrawal)]
        [TestCase("123", null, null, Repository.CurrencyCodeCad, 70, TransactionType.Withdrawal)]
        [TestCase("123", "", null, Repository.CurrencyCodeUsd, 70, TransactionType.Withdrawal)]
        [TestCase("123", "0123", null, null, 70, TransactionType.Withdrawal)]
        [TestCase("123", "0123", null, Repository.CurrencyCodeUsd, 600000000, TransactionType.Withdrawal)]
        [TestCase("123", "0123", null, Repository.CurrencyCodeUsd, 70, TransactionType.Transfer)]
        [TestCase("123", "0123", "0123", Repository.CurrencyCodeMxn, 70, TransactionType.Deposite)]
        [TestCase("123", "0123", null, Repository.CurrencyCodeCad, -500, TransactionType.Withdrawal)]
        [TestCase("123", "0123", null, Repository.CurrencyCodeUsd, 0.00000001, TransactionType.Deposite)]
        [TestCase("4564654654", "0123", null, Repository.CurrencyCodeMxn, 70, TransactionType.Deposite)]
        [TestCase("123", "6546546544", null, Repository.CurrencyCodeUsd, 70, TransactionType.Withdrawal)]
        [TestCase("123", null, null, Repository.CurrencyCodeUsd, 70, TransactionType.Transfer)]
        public void Transaction_Negative(string customerId, string fromAccountNumber, string toAccountNumber, string currencyCode, decimal? amount, TransactionType transactionType)
        {
            _accountService.PerformTransaction(customerId, fromAccountNumber, toAccountNumber, currencyCode, amount, transactionType);
        }

        #endregion

        #region Methods.


        /// <summary>
        /// Function performs the transaction.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="fromAccountNumber">From account number.</param>
        /// <param name="toAccountNumber">To account number.</param>
        /// <param name="currencyCode">The currency code.</param>
        /// <param name="amount">The amount.</param>
        /// <param name="transactionType">Type of the transaction.</param>
        private void PerformTransaction(string customerId, string fromAccountNumber, string toAccountNumber, string currencyCode, decimal? amount, TransactionType transactionType)
        {
            _accountService.PerformTransaction(customerId, fromAccountNumber, toAccountNumber, currencyCode, amount, transactionType);
        }

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="Tester" /> class.
        /// </summary>
        public Tester()
        {
            _accountService = new AccountService();
            _customerService = new CustomerService();
        }

        #endregion
    }
}
