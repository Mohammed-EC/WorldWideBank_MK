namespace WorldWideBank.Models
{
    /// <summary>
    /// Class used as a model to store currency exchange rates.
    /// Currency exchange rates can be retreived from datasource in real life.
    /// </summary>
    public class CurrencyExchangeRepositoryModel
    {
        #region  Properties.

        /// <summary>
        /// Property to get currency from which exchange rate is required.
        /// </summary>
        public Currency FromCurrency
        {
            get;
        }

        /// <summary>
        /// Property to get currency to which exchange rate is required.
        /// </summary>
        public Currency ToCurrency
        {
            get;
        }

        /// <summary>
        /// Property to get exchange rate.
        /// </summary>
        public decimal UnitExchangeRate
        {
            get;
        }

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="CurrencyExchangeRepositoryModel"/> class.
        /// </summary>
        /// <param name="fromCurrency">From currency.</param>
        /// <param name="toCurrency">To currency.</param>
        /// <param name="unitExchangeRate">The unit exchange rate.</param>
        public CurrencyExchangeRepositoryModel(Currency fromCurrency, Currency toCurrency, decimal unitExchangeRate)
        {
            FromCurrency = fromCurrency;
            ToCurrency = toCurrency;
            UnitExchangeRate = unitExchangeRate;
        }

        #endregion
    }
}
