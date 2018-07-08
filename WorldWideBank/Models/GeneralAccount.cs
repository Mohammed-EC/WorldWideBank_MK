using System;
using System.Data;
using System.Linq;
using WorldWideBank.Data;
using WorldWideBank.Models.Interfaces;
using WorldWideBank.Repositories;

namespace WorldWideBank.Models
{
    /// <summary>
    /// Class to define properties / methods for a general account.
    /// </summary>
    public class GeneralAccount : IGeneralAccount
    {
        #region Properties.

        /// <summary>
        /// Property to get the account number.
        /// </summary>
        public string AccountNumber
        {
            get;
        }

        /// <summary>
        /// Property to get the account holder.
        /// </summary>
        public ICustomer AccountHolder
        {
            get;
        }

        /// <summary>
        /// Property to get the initial balance for the account.
        /// </summary>
        public decimal InitialBalance
        {
            get;
        }

        /// <summary>
        /// Property to get the minimum balance for the account.
        /// </summary>
        public decimal MinimumAccountBalance
        {
            get;
            private set;
        }

        /// <summary>
        /// Property to get the monthly fee for the account.
        /// Used this property to demonstrate property in a more specific type of account.
        /// </summary>
        public decimal MonthlyFee
        {
            get;
            private set;
        }

        /// <summary>
        /// Property to get the default currency for the account.
        /// </summary>
        public Currency DefaultCurrency
        {
            get;
            private set;
        }


        #endregion

        /// <summary>
        /// Function to get the current balance of the account.
        /// </summary>
        /// <returns>
        /// A decimal value represents the current account balance using default currency.
        /// </returns>
        public decimal GetCurrentBalance()
        {
            decimal calculatedBalance = InitialBalance;

            foreach (ITransaction transaction in Repository.TransactionRepository.Where(tr =>
                string.Equals(tr.TransactionAccount.AccountNumber, AccountNumber, StringComparison.CurrentCultureIgnoreCase)))
            {
                switch (transaction.TransactionType)
                {
                    case TransactionType.Deposite:
                        calculatedBalance = calculatedBalance + (transaction.TransactionAmount * transaction.ExchangeRate);
                        break;

                    case TransactionType.Withdrawal:
                        calculatedBalance = calculatedBalance - (transaction.TransactionAmount * transaction.ExchangeRate);
                        break;
                }
            }

            return calculatedBalance;
        }

        /// <summary>
        /// Method used to update the minimum account balance.
        /// </summary>
        /// <param name="amount">The amount.</param>
        public void UpdateMinimumAccountBalance(decimal amount)
        {
            try
            {
                if (amount < 0)
                {
                    throw new DataException("Minimum account balance cannot be set to negative value.");
                }

                if (amount > GetCurrentBalance())
                {
                    throw new DataException("Minimum amount cannot be set as current balance is less than minimum balance.");
                }

                MinimumAccountBalance = amount;
            }
            catch (DataException dataException)
            {
                Console.WriteLine($"ERROR - {dataException.Message}");
            }
            catch (Exception)
            {
                Console.WriteLine("ERROR - An error occured while updating minimum account balance.");
            }
        }

        /// <summary>
        /// Method used to update the default currency.
        /// </summary>
        /// <param name="currency">The currency.</param>
        /// <exception cref="ArgumentNullException"> Thrown if
        /// <param name="currency"></param> parameter is null.
        /// </exception>
        public void UpdateDefaultCurrency(Currency currency)
        {
            if (currency == null)
            {
                throw new ArgumentNullException(nameof(currency));
            }

            try
            {
                DefaultCurrency = currency;
            }
            catch (Exception)
            {
                Console.WriteLine("ERROR - An error occured while setting default currency.");
            }
        }

        /// <summary>
        /// Updates the monthly fee.
        /// </summary>
        /// <param name="amount">The amount.</param>
        /// <exception cref="DataException">Monthly fee amount cannot be set to a negative value.</exception>
        public void UpdateMonthlyFee(decimal amount)
        {
            try
            {
                if (amount < 0)
                {
                    throw new DataException("Monthly fee amount cannot be set to a negative value.");
                }

                MonthlyFee = amount;
            }
            catch (DataException dataException)
            {
                Console.WriteLine($"ERROR - {dataException.Message}");
            }
            catch (Exception)
            {
                Console.WriteLine("ERROR - An error occured while updating monthly account fee.");
            }
        }

        #region Methods.


        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="GeneralAccount" /> class.
        /// </summary>
        /// <param name="accountNumber">The account number.</param>
        /// <param name="accountHolder">The account holder.</param>
        /// <param name="defaultCurrency">The default currency.</param>
        /// <param name="initialBalance">The initial balance.</param>
        /// <param name="minimumAccountBalance">The minimum account balance.</param>
        /// <exception cref="ArgumentNullException"> Thrown if
        /// <param name="accountNumber"></param> or
        /// <param name="accountHolder"></param> or
        /// <param name="defaultCurrency"></param> or
        /// </exception>
        public GeneralAccount(string accountNumber, ICustomer accountHolder, Currency defaultCurrency, decimal initialBalance, decimal minimumAccountBalance)
        {
            if (string.IsNullOrWhiteSpace(accountNumber))
            {
                throw new ArgumentNullException(nameof(accountNumber));
            }
            if (accountHolder == null)
            {
                throw new ArgumentNullException(nameof(accountHolder));
            }
            if (defaultCurrency == null)
            {
                throw new ArgumentNullException(nameof(defaultCurrency));
            }
            if (initialBalance < 1)
            {
                throw new DataException("Initial banance cannot be set to negative value");
            }
            if (minimumAccountBalance < 0)
            {
                throw new DataException("Minimum account balance cannot be set to negative value");
            }
            AccountNumber = accountNumber;
            AccountHolder = accountHolder;
            DefaultCurrency = defaultCurrency;
            InitialBalance = initialBalance;
            MinimumAccountBalance = minimumAccountBalance;
        }

        #endregion

    }
}
