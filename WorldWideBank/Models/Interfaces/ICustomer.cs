namespace WorldWideBank.Models.Interfaces
{
    /// <summary>
    /// Interface to define properties / methods for a customer.
    /// </summary>
    public interface ICustomer
    {
        #region Properties.

        /// <summary>
        /// Property gets the customer identifier.
        /// </summary>
        string CustomerId
        {
            get;
        }

        /// <summary>
        /// Property gets the customer first name.
        /// </summary>
        string FirstName
        {
            get;
        }

        /// <summary>
        /// Property gets the customer last name.
        /// </summary>
        string LastName
        {
            get;
        }

        #endregion

        #region Methods.

        /// <summary>
        /// Method to update the first name of a customer.
        /// </summary>
        /// <param name="firstName">The first name.</param>
        void UpdateFirstName(string firstName);

        /// <summary>
        /// Method to update the last name of a customer.
        /// </summary>
        /// <param name="lastName">The last name.</param>
        void UpdateLastName(string lastName);

        /// <summary>
        /// Function to get the full name of a customer.
        /// </summary>
        /// <returns> A string value combined of first and last name seperated by a space. </returns>
        string GetFullName();


        #endregion

    }
}
