using System;
using WorldWideBank.Data;

namespace WorldWideBank.Models.Interfaces
{
    /// <summary>
    /// Interface defines properties/methods for a transaction
    /// </summary>
    public interface ITransaction
    {
        #region Properties.

        /// <summary>
        /// Property to get the transaction number.
        /// GUID is used for the demonstration.
        /// System/Database generated number can be used when working with database.
        /// </summary>
        Guid TransactionNumber
        {
            get;
        }

        /// <summary>
        /// Property to get the transaction type.
        /// </summary>
        TransactionType TransactionType
        {
            get;
        }

        /// <summary>
        /// Property to get the account where transaction is being performed.
        /// </summary>
        IAccount TransactionAccount
        {
            get;
        }

        /// <summary>
        /// Property to get the currency used for the transaction.
        /// </summary>
        Currency TransactionCurrency
        {
            get;
        }

        /// <summary>
        /// Property to get the transaction amount.
        /// </summary>
        decimal TransactionAmount
        {
            get;
        }

        /// <summary>
        /// Property to get the exchange rate for the transaction.
        /// </summary>
        decimal ExchangeRate
        {
            get;
        }

        /// <summary>
        /// Property to get the description for the transaction.
        /// </summary>
        string Description
        {
            get;
        }

        #endregion
    }
}
