namespace WorldWideBank.Models.Interfaces
{
    /// <summary>
    /// Interface to define base properties / methods for an account.
    /// </summary>
    public interface IAccount
    {
        #region Properties.

        /// <summary>
        /// Property to get the account number.
        /// </summary>
        string AccountNumber
        {
            get;
        }

        /// <summary>
        /// Property to get the account holder.
        /// </summary>
        ICustomer AccountHolder
        {
            get;
        }

        /// <summary>
        /// Property to get the initial balance for the account.
        /// </summary>
        decimal InitialBalance
        {
            get;
        }

        /// <summary>
        /// Property to get the minimum balance for the account.
        /// </summary>
        decimal MinimumAccountBalance
        {
            get;
        }

        /// <summary>
        /// Property to get the default currency for the account.
        /// </summary>
        Currency DefaultCurrency
        {
            get;
        }

        #endregion

        #region Methods.

        /// <summary>
        /// Method used to update the minimum account balance.
        /// </summary>
        /// <param name="amount">The amount.</param>
        void UpdateMinimumAccountBalance(decimal amount);

        /// <summary>
        /// Method used to update the default currency.
        /// </summary>
        /// <param name="currency">The currency.</param>
        void UpdateDefaultCurrency(Currency currency);

        /// <summary>
        /// Function to get the current balance of the account.
        /// </summary>
        /// <returns> A decimal value represents the current account balance using default currency. </returns>
        decimal GetCurrentBalance();

        #endregion

    }
}
