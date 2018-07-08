using System;
using WorldWideBank.Models.Interfaces;

namespace WorldWideBank.Models
{
    /// <summary>
    /// Class to define properties / methods for a customer.
    /// </summary>
    /// <seealso cref="Customer" />
    public class Customer : ICustomer
    {
        #region Properties.

        /// <summary>
        /// Property gets the customer identifier.
        /// </summary>
        public string CustomerId
        {
            get;
        }

        /// <summary>
        /// Property gets the customer first name.
        /// </summary>
        public string FirstName
        {
            get;
            private set;
        }

        /// <summary>
        /// Property gets the customer last name.
        /// </summary>
        public string LastName
        {
            get;
            private set;
        }

        #endregion

        #region Methods.

        /// <summary>
        /// Method to update the first name of a customer.
        /// </summary>
        /// <param name="firstName">The first name.</param>
        public void UpdateFirstName(string firstName)
        {
            FirstName = firstName;
        }

        /// <summary>
        /// Method to update the last name of a customer.
        /// </summary>
        /// <param name="lastName">The last name.</param>
        public void UpdateLastName(string lastName)
        {
            LastName = lastName;
        }

        /// <summary>
        /// Function to get the full name of a customer.
        /// </summary>
        /// <returns>
        /// A string value combined of first and last name seperated by a space.
        /// </returns>
        public string GetFullName()
        {
            return $"{FirstName} {LastName}";
        }

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="Customer" /> class.
        /// </summary>
        /// <param name="customerId">The identifier.</param>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <param name="firstName"></param> parameter is null or empty.
        /// </exception>
        public Customer(string customerId, string firstName, string lastName)
        {
            if (string.IsNullOrEmpty(firstName))
            {
                throw new ArgumentNullException(nameof(firstName));
            }

            CustomerId = customerId;
            FirstName = firstName;
            LastName = lastName;
        }

        #endregion
    }
}
