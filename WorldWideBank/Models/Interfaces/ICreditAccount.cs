namespace WorldWideBank.Models.Interfaces
{
    /// <summary>
    /// Interface to define properties / methods for a credit account.
    /// </summary>
    /// <seealso cref="IAccount" />
    public interface ICreditAccount : IAccount
    {
        #region Properties.

        /// <summary>
        /// Property to get the maximum credit limit for the account.
        /// Used this property to demonstrate property in a more specific type of account.
        /// </summary>
        decimal MaximumCreditLimit
        {
            get;
        }

        #endregion

        #region Methods.

        /// <summary>
        /// Method updates the maximum credit limit for the account.
        /// </summary>
        /// <param name="amount">The amount.</param>
        void UpdateMaximumCreditLimit(decimal amount);

        #endregion
    }
}
