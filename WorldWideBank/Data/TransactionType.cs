namespace WorldWideBank.Data
{
    /// <summary>
    /// Enum to define the type of the transactions.
    /// </summary>
    public enum TransactionType
    {
        /// <summary>
        /// The withdrawal transaction type.
        /// </summary>
        Withdrawal,
        /// <summary>
        /// The deposite transaction type.
        /// </summary>
        Deposite,
        /// <summary>
        /// The transfer transaction type.
        /// </summary>
        Transfer,
        /// <summary>
        /// Unknown transaction type.
        /// </summary>
        Unknown
    }
}
