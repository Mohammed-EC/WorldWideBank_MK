namespace WorldWideBank.Models.Interfaces
{
    /// <summary>
    /// Interface to define properties / methods for a general account.
    /// </summary>
    /// <seealso cref="IAccount" />
    public interface IGeneralAccount : IAccount
    {
        #region Properties.

        /// <summary>
        /// Property to get the monthly fee for the account.
        /// Used this property to demonstrate property in a more specific type of account.
        /// </summary>
        decimal MonthlyFee
        {
            get;
        }

        #endregion

        #region Methods.

        /// <summary>
        /// Method updates the monthly account fee.
        /// </summary>
        /// <param name="feeAmount">The fee amount.</param>
        void UpdateMonthlyFee(decimal feeAmount);

        #endregion


    }
}
