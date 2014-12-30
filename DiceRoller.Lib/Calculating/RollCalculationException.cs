#region Usings

using System;

#endregion

namespace DiceRoller.Lib
{
    /// <summary>
    ///     Exception thrown when the calculation of a roll fails.
    /// </summary>
    public class RollCalculationException : Exception
    {
        #region Ctor

        /// <summary>
        ///     Initializes a new instance of the <see cref="RollCalculationException" /> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public RollCalculationException( String message )
            : base( message )
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="RollCalculationException" /> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">
        ///     The exception that is the cause of the current exception, or a null reference (Nothing in
        ///     Visual Basic) if no inner exception is specified.
        /// </param>
        public RollCalculationException( String message, Exception innerException )
            : base( message, innerException )
        {
        }

        #endregion
    }
}