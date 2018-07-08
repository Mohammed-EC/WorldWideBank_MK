using System;
using WorldWideBank.Data;
using WorldWideBank.Models.Interfaces;

namespace WorldWideBank.Models
{
    /// <summary>
    /// Class defines properties/methods for a transaction.
    /// </summary>
    /// <seealso cref="ITransaction" />
    public class Transaction : ITransaction
    {
        #region Properties.

        /// <summary>
        /// Property to get the transaction number.
        /// GUID is used for the demonstration.
        /// System/Database generated number can be used when working with database.
        /// </summary>
        public Guid TransactionNumber
        {
            get;
        }

        /// <summary>
        /// Property to get the transaction type.
        /// </summary>
        public TransactionType TransactionType
        {
            get;
        }

        /// <summary>
        /// Property to get the account where transaction is being performed.
        /// </summary>
        public IAccount TransactionAccount
        {
            get;
        }

        /// <summary>
        /// Property to get the currency used for the transaction.
        /// </summary>
        public Currency TransactionCurrency
        {
            get;
        }

        /// <summary>
        /// Property to get the transaction amount.
        /// </summary>
        public decimal TransactionAmount
        {
            get;
        }

        /// <summary>
        /// Property to get the exchange rate for the transaction.
        /// </summary>
        public decimal ExchangeRate
        {
            get;
        }

        /// <summary>
        /// Property to get the description for the transaction.
        /// </summary>
        public string Description
        {
            get;
        }

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="Transaction" /> class.
        /// </summary>
        /// <param name="transactionAccount">The transaction account.</param>
        /// <param name="transactionCurrency">The transaction currency.</param>
        /// <param name="transactionAmount">The transaction amount.</param>
        /// <param name="exchangeRate">The exchange rate.</param>
        /// <param name="description">The description.</param>
        /// <param name="transactionType">Type of the transaction.</param>
        /// <exception cref="ArgumentNullException"> Thrown if
        /// <param name="transactionAccount"></param> or
        /// <param name="transactionCurrency"></param> or
        /// <param name="transactionType"></param> parameter is <b>null</b>.
        /// </exception>
        public Transaction(IAccount transactionAccount, Currency transactionCurrency, decimal transactionAmount, decimal exchangeRate, string description, TransactionType transactionType)
        {
            if (transactionAccount == null)
            {
                throw new ArgumentNullException(nameof(transactionAccount));
            }
            if (transactionCurrency == null)
            {
                throw new ArgumentNullException(nameof(transactionCurrency));
            }
            if (transactionType == TransactionType.Unknown)
            {
                throw new ArgumentNullException(nameof(transactionType));
            }

            TransactionNumber = new Guid();
            TransactionAccount = transactionAccount;
            TransactionCurrency = transactionCurrency;
            TransactionAmount = transactionAmount;
            ExchangeRate = exchangeRate;
            Description = description;
            TransactionType = transactionType;
        }


        #endregion
    }
}
