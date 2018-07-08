using System;
using System.Data;
using System.Linq;
using WorldWideBank.Data;
using WorldWideBank.Models;
using WorldWideBank.Models.Interfaces;
using WorldWideBank.Repositories;

namespace WorldWideBank.Services
{
    /// <summary>
    /// Class to provide account related service.
    /// </summary>
    public class AccountService
    {
        #region Methods.

        /// <summary>
        /// Method to create a new account.
        /// </summary>
        /// <param name="accountNumber">The account number.</param>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="currencyCode">The currency code.</param>
        /// <param name="initialBalance">The initial balance.</param>
        /// <param name="mimimumBalance">The mimimum balance.</param>
        public void CreateGeneralAccount(string accountNumber, string customerId, string currencyCode, decimal? initialBalance, decimal? mimimumBalance)
        {
            try
            {
                if (Repository.GeneralAccountRepository.Any(a => string.Equals(a.AccountNumber, accountNumber, StringComparison.CurrentCultureIgnoreCase)))
                {
                    throw new DataException($"Unable to create account as account number {accountNumber} already exist !!");
                }
                if (initialBalance == null)
                {
                    throw new DataException("Unable to create account as provided initial balance is unknown !!");
                }
                if (mimimumBalance == null)
                {
                    throw new DataException("Unable to create account as provided minimum balance is unknown !!");
                }

                ICustomer accountHolder = Repository.CustomerRepository.FirstOrDefault(c => string.Equals(c.CustomerId, customerId, StringComparison.CurrentCultureIgnoreCase));
                if (accountHolder == null)
                {
                    throw new DataException($"Unable to create account for customer id {customerId} as customer not found !!");
                }

                Currency defaultCurrency = Repository.CurrencyRepository.FirstOrDefault(c =>
                    string.Equals(c.Code, currencyCode, StringComparison.CurrentCultureIgnoreCase));
                if (defaultCurrency == null)
                {
                    throw new DataException($"Unable to create account for customer as provided currency code {currencyCode} doesn't exist !!");
                }

                IGeneralAccount account = new GeneralAccount(accountNumber, accountHolder, defaultCurrency, initialBalance.Value, mimimumBalance.Value);
                Repository.GeneralAccountRepository.Add(account);
            }
            catch (DataException dataException)
            {
                Console.WriteLine($"ERROR - {dataException.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occured while creating account !! {ex.Message}");
            }
        }

        /// <summary>
        /// Method to delete an existing account.
        /// </summary>
        /// <param name="accountNumber">The account number.</param>
        /// <exception cref="DataException"></exception>
        public void DeleteAccount(string accountNumber)
        {
            try
            {
                IGeneralAccount account = Repository.GeneralAccountRepository.FirstOrDefault(a => string.Equals(a.AccountNumber, accountNumber, StringComparison.CurrentCultureIgnoreCase));
                if (account == null)
                {
                    throw new DataException($"Unable to delete account as account with number {accountNumber} doesn't exist !!");
                }

                Repository.GeneralAccountRepository.Remove(account);
            }
            catch (DataException dataException)
            {
                Console.WriteLine($"ERROR - {dataException.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occured while deleting account !!  {ex.Message}");
            }

        }

        /// <summary>
        /// Method to performa a transaction.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="fromAccountNumber">From account number.</param>
        /// <param name="toAccountNumber">To account number.</param>
        /// <param name="currencyCode">The currency code.</param>
        /// <param name="amount">The amount.</param>
        /// <param name="transactionType">Type of the transaction.</param>
        public void PerformTransaction(string customerId, string fromAccountNumber, string toAccountNumber, string currencyCode, decimal? amount, TransactionType transactionType)
        {
            try
            {
                if ((string.IsNullOrWhiteSpace(fromAccountNumber)) && (string.IsNullOrWhiteSpace(toAccountNumber)))
                {
                    throw new DataException("Unable to perform transaction as no account number provided !!");
                }
                if (amount == null)
                {
                    throw new DataException("Unable to perform transaction as transaction amount is unknown !!");
                }
                if (string.IsNullOrWhiteSpace(currencyCode))
                {
                    throw new DataException("Unable to perform transaction as no currency provided for the transaction !!");
                }

                ICustomer customer = Repository.CustomerRepository.FirstOrDefault(c => string.Equals(c.CustomerId, customerId, StringComparison.CurrentCultureIgnoreCase));
                if ((string.IsNullOrWhiteSpace(customerId))
                    || (customer == null))
                {
                    throw new DataException("Unable to perform transaction as no customer found for verification !!");
                }

                IAccount fromAccount =
                    Repository.GeneralAccountRepository.FirstOrDefault(a => (fromAccountNumber != null) && (string.Equals(a.AccountNumber, fromAccountNumber, StringComparison.CurrentCultureIgnoreCase)));
                IAccount toAccount =
                    Repository.GeneralAccountRepository.FirstOrDefault(a => (toAccountNumber != null) && (string.Equals(a.AccountNumber, toAccountNumber, StringComparison.CurrentCultureIgnoreCase)));
                Currency currency = Repository.CurrencyRepository.FirstOrDefault(c =>
                    string.Equals(c.Code, currencyCode, StringComparison.CurrentCultureIgnoreCase));

                switch (transactionType)
                {
                    case TransactionType.Deposite:
                        PerformDeposite(toAccount, currency, amount.Value);
                        break;

                    case TransactionType.Withdrawal:
                        if (!string.Equals(fromAccount?.AccountHolder?.CustomerId, customer.CustomerId,
                            StringComparison.CurrentCultureIgnoreCase))
                        {
                            throw new DataException("Unable to perform transaction as customer not the owner of the account !!");
                        }
                        PerformWithdrawal(fromAccount, currency, amount.Value);
                        break;

                    case TransactionType.Transfer:
                        if (!string.Equals(fromAccount?.AccountHolder?.CustomerId, customer.CustomerId,
                            StringComparison.CurrentCultureIgnoreCase))
                        {
                            throw new DataException("Unable to perform transaction as customer not the owner of the account !!");
                        }
                        PerformTransfer(fromAccount, toAccount, currency, amount.Value);
                        break;
                    default:
                        return;
                }
            }
            catch (DataException dataException)
            {
                Console.WriteLine($"ERROR - {dataException.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occured while performing transaction !!  {ex.Message}");
            }
        }

        /// <summary>
        /// Methods to perform a withdrawal from an account.
        /// </summary>
        /// <param name="account">The account.</param>
        /// <param name="currency">The currency.</param>
        /// <param name="amount">The amount.</param>
        /// <exception cref="ArgumentNullException"> Thrown if
        /// <param name="account"></param> or
        /// <param name="currency"></param> parameter is <b>null</b>.
        /// </exception>
        private void PerformWithdrawal(IAccount account, Currency currency, decimal amount)
        {
            if (account == null)
            {
                throw new ArgumentNullException(nameof(account));
            }
            if (currency == null)
            {
                throw new ArgumentNullException(nameof(currency));
            }
            try
            {
                decimal exchangeRate = GetCurrentExchangeRate(currency, account.DefaultCurrency);
                if (CanWithdrawAmount(account, currency, amount, exchangeRate))
                {
                    ITransaction transaction = new Transaction(account, currency, amount, exchangeRate, $"Amount {amount:C2} was withdrawn.", TransactionType.Withdrawal);
                    Repository.TransactionRepository.Add(transaction);
                }
            }
            catch (DataException dataException)
            {
                Console.WriteLine($"ERROR - {dataException.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR - An error occured while performing transaction. {ex.Message}");
            }
        }

        /// <summary>
        /// Methods to perform a deposite to an account.
        /// </summary>
        /// <param name="account">The account.</param>
        /// <param name="currency">The currency.</param>
        /// <param name="amount">The amount.</param>
        /// <exception cref="ArgumentNullException"> Thrown if
        /// <param name="account"></param> or
        /// <param name="currency"></param> parameter is <b>null</b>.
        /// </exception>
        private void PerformDeposite(IAccount account, Currency currency, decimal amount)
        {
            if (account == null)
            {
                throw new ArgumentNullException(nameof(account));
            }
            if (currency == null)
            {
                throw new ArgumentNullException(nameof(currency));
            }
            try
            {
                decimal exchangeRate = GetCurrentExchangeRate(currency, account.DefaultCurrency);
                if (CanDepositeAmount(account, currency, amount, exchangeRate))
                {
                    ITransaction transaction = new Transaction(account, currency, amount, exchangeRate, $"Amount {amount:C2} was withdrawn.", TransactionType.Deposite);
                    Repository.TransactionRepository.Add(transaction);
                }
            }
            catch (DataException dataException)
            {
                Console.WriteLine($"ERROR - {dataException.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR - An error occured while performing transaction. {ex.Message}");
            }
        }

        /// <summary>
        /// Methods to perform a transfer from one account to another.
        /// </summary>
        /// <param name="fromAccount">From account.</param>
        /// <param name="toAccount">To account.</param>
        /// <param name="currency">The currency.</param>
        /// <param name="amount">The amount.</param>
        /// <exception cref="ArgumentNullException"> Thrown if
        /// <param name="fromAccount"></param> or
        /// <param name="toAccount"></param> or
        /// <param name="currency"></param> parameter is <b>null</b>.
        /// </exception>
        private void PerformTransfer(IAccount fromAccount, IAccount toAccount, Currency currency, decimal amount)
        {
            if (fromAccount == null)
            {
                throw new ArgumentNullException(nameof(fromAccount));
            }
            if (toAccount == null)
            {
                throw new ArgumentNullException(nameof(toAccount));
            }
            if (currency == null)
            {
                throw new ArgumentNullException(nameof(currency));
            }
            try
            {
                decimal fromAccExchangeRate = GetCurrentExchangeRate(currency, fromAccount.DefaultCurrency);
                decimal toAccExchangeRate = GetCurrentExchangeRate(currency, toAccount.DefaultCurrency);

                if ((CanWithdrawAmount(fromAccount, currency, amount, fromAccExchangeRate)
                    && (CanDepositeAmount(toAccount, currency, amount, toAccExchangeRate))))
                {
                    ITransaction fromTransaction = new Transaction(fromAccount, currency, amount, fromAccExchangeRate, $"Amount {amount:C2} was transferred to account {toAccount.AccountNumber}.", TransactionType.Withdrawal);
                    ITransaction toTransaction = new Transaction(toAccount, currency, amount, fromAccExchangeRate, $"Amount {amount:C2} was transferred from account {fromAccount.AccountNumber}.", TransactionType.Deposite);
                    Repository.TransactionRepository.Add(fromTransaction);
                    Repository.TransactionRepository.Add(toTransaction);
                }
            }
            catch (DataException dataException)
            {
                Console.WriteLine($"ERROR - {dataException.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR - An error occured while performing transaction. {ex.Message}");
            }
        }

        /// <summary>
        /// Function to get the current exchange rate.
        /// </summary>
        /// <param name="fromCurrency">From currency.</param>
        /// <param name="toCurrency">To currency.</param>
        /// <returns> A decimal value represents the exchange rate for the requested currencies. </returns>
        /// <exception cref="DataException">Unable to perform transaction due to conversion error !!</exception>
        private decimal GetCurrentExchangeRate(Currency fromCurrency, Currency toCurrency)
        {
            decimal? currentExchangeRate = CurrencyServices.GetExchangeRate(fromCurrency, toCurrency);
            if (currentExchangeRate == null)
            {
                throw new DataException("Unable to perform transaction due to conversion error !!");
            }

            return currentExchangeRate.Value;
        }

        /// <summary>
        /// Function to determine whether an amount can be withdrawn from the specified account.
        /// </summary>
        /// <param name="account">The account.</param>
        /// <param name="currency">The currency.</param>
        /// <param name="amount">The amount.</param>
        /// <param name="exchangeRate">The exchange rate.</param>
        /// <returns>
        /// True if amount can be withdrawn from the account, false otherwise.
        /// </returns>
        /// <exception cref="ArgumentNullException"> Thrown if 
        /// <param name="account"></param> or
        /// <param name="currency"></param> parameter is <b>null</b>.
        /// </exception>
        private bool CanWithdrawAmount(IAccount account, Currency currency, decimal amount, decimal exchangeRate)
        {
            if (account == null)
            {
                throw new ArgumentNullException(nameof(account));
            }
            if (currency == null)
            {
                throw new ArgumentNullException(nameof(currency));
            }

            decimal withdrawalAmount = amount * exchangeRate;
            if (withdrawalAmount < 0)
            {
                throw new DataException("Unable to perform transaction for a negative amount !!");
            }

            if (withdrawalAmount > (account.InitialBalance - account.MinimumAccountBalance))
            {
                throw new DataException("Unable to perform transaction due to insufficiant balance !!");
            }

            return true;
        }

        /// <summary>
        /// Function to determine whether an amount can be deposited to the specified account.
        /// </summary>
        /// <param name="account">The account.</param>
        /// <param name="currency">The currency.</param>
        /// <param name="amount">The amount.</param>
        /// <param name="exchangeRate">The exchange rate.</param>
        /// <returns>
        /// True if amount can be deposited to the account, false otherwise.
        /// </returns>
        /// <exception cref="ArgumentNullException"> Thrown if 
        /// <param name="account"></param> or
        /// <param name="currency"></param> parameter is <b>null</b>.
        /// </exception>
        public bool CanDepositeAmount(IAccount account, Currency currency, decimal amount, decimal exchangeRate)
        {
            decimal depositeAmount = amount * exchangeRate;
            if (depositeAmount < 0)
            {
                throw new DataException("Unable to perform transaction for a negative amount !!");
            }

            return true;
        }

        /// <summary>
        /// Function returns the account balance of the account number provided.
        /// </summary>
        /// <param name="accountNumber">The account number.</param>
        /// <returns> A decimal value represents the current balance in the account. Null if no account found. </returns>
        public decimal? GetAccountBalance(string accountNumber)
        {
            IAccount account =
            Repository.GeneralAccountRepository.FirstOrDefault(a => string.Equals(a.AccountNumber, accountNumber, StringComparison.CurrentCultureIgnoreCase));
            decimal? accountBalance = account?.GetCurrentBalance();
            return accountBalance;
        }

        /// <summary>
        /// Method to display the account current balance.
        /// </summary>
        /// <param name="accountNumber">The account number.</param>
        /// <exception cref="DataException"></exception>
        public void DisplayAccountCurrentBalance(string accountNumber)
        {
            try
            {
                IAccount account =
                    Repository.GeneralAccountRepository.FirstOrDefault(a => string.Equals(a.AccountNumber, accountNumber, StringComparison.CurrentCultureIgnoreCase));
                if (account == null)
                {
                    throw new DataException($"Unable to display balance as account number{accountNumber} not found.!!");
                }

                Console.WriteLine($"Account Number: {accountNumber} Balance: {account.GetCurrentBalance():C2} {account.DefaultCurrency.Code}");
            }
            catch (DataException dataException)
            {
                Console.WriteLine($"ERROR - {dataException.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR - An error occured while performing transaction. {ex.Message}");
            }
        }

        #endregion
    }
}
