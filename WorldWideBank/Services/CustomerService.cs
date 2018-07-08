using System;
using System.Data;
using System.Linq;
using WorldWideBank.Models;
using WorldWideBank.Models.Interfaces;
using WorldWideBank.Repositories;

namespace WorldWideBank.Services
{
    /// <summary>
    /// Class to provide customer related service.
    /// </summary>
    public class CustomerService
    {
        #region Methods.

        /// <summary>
        /// Function adds a new customer based on given parameters.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        public void AddCustomer(string customerId, string firstName, string lastName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(customerId))
                {
                    throw new DataException("Customer ID is required to add new customer.");
                }
                if (string.IsNullOrWhiteSpace(firstName))
                {
                    throw new DataException("First name is required to add new customer.");
                }
                if (Repository.CustomerRepository.Any(c => string.Equals(c.CustomerId, customerId, StringComparison.CurrentCultureIgnoreCase)))
                {
                    throw new DataException($"Unable to add customer as customer with Id {customerId} already exist !!");
                }
                ICustomer customer = new Customer(customerId, firstName, lastName);
                Repository.CustomerRepository.Add(customer);
            }
            catch (DataException dataException)
            {
                Console.WriteLine($"ERROR - {dataException.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occured while adding customer !! {ex.Message}");
            }

        }

        /// <summary>
        /// Function to delete an existing the customer.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        public void DeleteCustomer(string customerId)
        {
            try
            {
                ICustomer customer = Repository.CustomerRepository.FirstOrDefault(c => string.Equals(c.CustomerId, customerId, StringComparison.CurrentCultureIgnoreCase));
                if (customer == null)
                {
                    throw new DataException($"Unable to delete customer, customer not found with id {customerId}");
                }

                if (Repository.GeneralAccountRepository.Any(a => string.Equals(a.AccountHolder.CustomerId, customerId, StringComparison.CurrentCultureIgnoreCase)))
                {
                    throw new DataException(
                        $"Unable to delete customer with customer id {customerId} while customer have an active account.");
                }

                Repository.CustomerRepository.Remove(customer);
            }
            catch (DataException dataException)
            {
                Console.WriteLine($"ERROR - {dataException.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occured while adding customer !! {ex.Message}");
            }
        }

        #endregion
    }
}
