using System;
using System.Collections.Generic;
using System.Linq;
using WorldWideBank.Models;
using WorldWideBank.Models.Interfaces;

namespace WorldWideBank.Repositories
{
    /// <summary>
    /// Static class used to simulate the data storage. 
    /// Usually data source is used in real life.
    /// </summary>
    public static class Repository
    {
        #region Constants

        /// <summary>
        /// Conversion rate from CAD to USD
        /// </summary>
        private const decimal CadToUsd = 0.50m;

        /// <summary>
        /// Conversion rate from CAD to MXN.
        /// </summary>
        private const decimal CadToMxn = 10.0m;

        /// <summary>
        /// The currency code for canadian dollar.
        /// </summary>
        public const string CurrencyCodeCad = "CAD";

        /// <summary>
        /// The currency code for us dollar.
        /// </summary>
        public const string CurrencyCodeUsd = "USD";

        /// <summary>
        /// The currency code for mexican pesos.
        /// </summary>
        public const string CurrencyCodeMxn = "MXN";

        /// <summary>
        /// The canadian dollar currency.
        /// </summary>
        private static readonly Currency Cad = new Currency(CurrencyCodeCad, "Canadian Dollar");

        /// <summary>
        /// The US dollar currency.
        /// </summary>
        private static readonly Currency Usd = new Currency(CurrencyCodeUsd, "US Dollar");

        /// <summary>
        /// The mexican pesos currency.
        /// </summary>
        public static readonly Currency Mxn = new Currency(CurrencyCodeMxn, "Mexican Pesos");

        #endregion

        #region Properties.

        /// <summary>
        /// Property used as repository to get or set the currencies.
        /// </summary>
        public static IList<Currency> CurrencyRepository
        {
            get;
            set;
        }
        = new List<Currency>()
            {
                Cad, Usd, Mxn
            };

        /// <summary>
        /// Property used as repository to get or set the transactions.
        /// </summary>
        public static IList<ITransaction> TransactionRepository
        {
            get;
            set;
        } = new List<ITransaction>();

        /// <summary>
        /// Property used as repository to get or set the currency exchanges.
        /// </summary>
        public static IList<CurrencyExchangeRepositoryModel> CurrencyExchangeRepository
        {
            get;
            set;
        } = new List<CurrencyExchangeRepositoryModel>()
                    {
                        new CurrencyExchangeRepositoryModel (Cad, Usd, CadToUsd),
                        new CurrencyExchangeRepositoryModel (Usd, Cad, 1 / CadToUsd),
                        new CurrencyExchangeRepositoryModel (Cad, Mxn, CadToMxn),
                        new CurrencyExchangeRepositoryModel (Mxn, Cad, 1 / CadToMxn)
                    };

        /// <summary>
        /// Property used as repository to get or set the customers.
        /// </summary>
        public static IList<ICustomer> CustomerRepository
        {
            get;
            set;
        } = new List<ICustomer>()
            {
                new Customer("777", "Stewie", "Griffin"),
                new Customer("504", "Glenn", "Quagmire"),
                new Customer("002", "Joe", "Swanson"),
                new Customer("123", "Peter", "Griffin"),
                new Customer("456", "Lois", "Griffin")
        };

        /// <summary>
        /// Property used as repository to get or set the general accounts.
        /// </summary>
        public static IList<IGeneralAccount> GeneralAccountRepository
        {
            get;
            set;
        } = new List<IGeneralAccount>()
        {
            new GeneralAccount("1234", CustomerRepository.FirstOrDefault(c => string.Equals(c.CustomerId, "777", StringComparison.CurrentCultureIgnoreCase)), CurrencyRepository.FirstOrDefault(c => string.Equals(c.Code, CurrencyCodeCad, StringComparison.CurrentCultureIgnoreCase)), 100.00m, 0),
            new GeneralAccount("2001", CustomerRepository.FirstOrDefault(c => string.Equals(c.CustomerId, "504", StringComparison.CurrentCultureIgnoreCase)), CurrencyRepository.FirstOrDefault(c => string.Equals(c.Code, CurrencyCodeCad, StringComparison.CurrentCultureIgnoreCase)), 35000.00m, 0),
            new GeneralAccount("1010", CustomerRepository.FirstOrDefault(c => string.Equals(c.CustomerId, "002", StringComparison.CurrentCultureIgnoreCase)), CurrencyRepository.FirstOrDefault(c => string.Equals(c.Code, CurrencyCodeCad, StringComparison.CurrentCultureIgnoreCase)), 7425.00m, 0),
            new GeneralAccount("5500", CustomerRepository.FirstOrDefault(c => string.Equals(c.CustomerId, "002", StringComparison.CurrentCultureIgnoreCase)), CurrencyRepository.FirstOrDefault(c => string.Equals(c.Code, CurrencyCodeCad, StringComparison.CurrentCultureIgnoreCase)), 15000.00m, 0),
            new GeneralAccount("0123", CustomerRepository.FirstOrDefault(c => string.Equals(c.CustomerId, "123", StringComparison.CurrentCultureIgnoreCase)), CurrencyRepository.FirstOrDefault(c => string.Equals(c.Code, CurrencyCodeCad, StringComparison.CurrentCultureIgnoreCase)), 150.00m, 0),
            new GeneralAccount("0456", CustomerRepository.FirstOrDefault(c => string.Equals(c.CustomerId, "456", StringComparison.CurrentCultureIgnoreCase)), CurrencyRepository.FirstOrDefault(c => string.Equals(c.Code, CurrencyCodeCad, StringComparison.CurrentCultureIgnoreCase)), 65000.00m, 0)
        };

        /// <summary>
        /// Property used as repository to get or set the credit accounts.
        /// We've added this property for demonstration of different types of account.
        /// No implementation exists for ICreditAccount.
        /// </summary>
        public static IList<ICreditAccount> CreditAccountRepository
        {
            get;
            set;
        }

        #endregion
    }
}
