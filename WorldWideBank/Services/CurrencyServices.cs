using System.Linq;
using WorldWideBank.Models;
using WorldWideBank.Repositories;

namespace WorldWideBank.Services
{
    /// <summary>
    /// Static class to provide currency related service.
    /// </summary>
    public static class CurrencyServices
    {
        #region  Methods.

        /// <summary>
        /// Function to get the exchange rate of provided currencies.
        /// </summary>
        /// <param name="fromCurrency">From currency.</param>
        /// <param name="toCurrency">To currency.</param>
        /// <returns> A decimal value represents the exchange rate of the provided currency. 
        /// Null if unable to get the exchange rate. </returns>
        public static decimal? GetExchangeRate(Currency fromCurrency, Currency toCurrency)
        {
            if (fromCurrency == toCurrency)
            {
                return 1;
            }

            CurrencyExchangeRepositoryModel currencyExchangeModel =
                Repository.CurrencyExchangeRepository.FirstOrDefault(cr =>
                                                                            (cr.FromCurrency == fromCurrency)
                                                                         && (cr.ToCurrency == toCurrency)
                                                                     );
            return currencyExchangeModel?.UnitExchangeRate;
        }

        /// <summary>
        /// Function to convert a given amount to an amount of requested currency.
        /// </summary>
        /// <param name="fromCurrency">From currency.</param>
        /// <param name="toCurrency">To currency.</param>
        /// <param name="amountToConvert">The amount to convert.</param>
        /// <returns> A decimal value represents the converted amount as per provided currencies. 
        /// Null if unable to convert. </returns>
        public static decimal? Convert(Currency fromCurrency, Currency toCurrency, decimal amountToConvert)
        {
            decimal? convertedCurrency = amountToConvert * GetExchangeRate(fromCurrency, toCurrency);
            return convertedCurrency;
        }

        #endregion  
    }
}
