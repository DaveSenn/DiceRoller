using System;

namespace DiceRoller
{
    /// <summary>
    /// Exception thrown if the argument parsing has failed.
    /// </summary>
    public class ArgumentParsingException : Exception
    {
        #region Ctor

        /// <summary>
        ///     Initializes a new instance of the <see cref="ArgumentParsingException" /> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public ArgumentParsingException(String message)
            : base( message )
        {
        }

        #endregion
    }
}