using System;

namespace WorldWideBank.Models
{
    /// <summary>
    /// Class to define properties / methods for a currency.
    /// </summary>
    /// <seealso cref="System.IEquatable{Currency}" />
    public class Currency : IEquatable<Currency>
    {
        #region Properties.

        /// <summary>
        /// Property gets the currency code.
        /// </summary>
        public string Code
        {
            get;
        }

        /// <summary>
        /// Property gets the detail name of the currency.
        /// </summary>
        public string Details
        {
            get;
        }

        #endregion

        #region Methods.

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
        /// </returns>
        public bool Equals(Currency other)
        {
            if (other == null)
            {
                return false;
            }

            return string.Equals(this.Code, other.Code, StringComparison.CurrentCultureIgnoreCase);
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="otherObject">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object otherObject)
        {
            Currency currency = otherObject as Currency;
            if (currency == null)
            {
                return false;
            }

            return Equals(currency);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return this.Code.GetHashCode();
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="currency1">The currency1.</param>
        /// <param name="currency2">The currency2.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator ==(Currency currency1, Currency currency2)
        {
            if (((object)currency1) == null || ((object)currency2) == null)
            {
                return object.Equals(currency1, currency2);
            }

            return currency1.Equals(currency2);
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="currency1">The currency1.</param>
        /// <param name="currency2">The currency2.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator !=(Currency currency1, Currency currency2)
        {
            if (((object)currency1) == null || ((object)currency2) == null)
            {
                return !(object.Equals(currency1, currency2));
            }

            return !(currency1.Equals(currency2));
        }

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="Currency"/> class.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="details">The details.</param>
        public Currency(string code, string details)
        {
            Code = code;
            Details = details;
        }

        #endregion

    }
}
