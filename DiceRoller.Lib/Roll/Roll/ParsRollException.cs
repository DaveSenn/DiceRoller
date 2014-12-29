#region Usings

using System;

#endregion

namespace DiceRoller.Lib
{
    /// <summary>
    ///     Exception thrown when the parsing of a roll fails.
    /// </summary>
    public class ParsRollException : Exception
    {
        #region Properties

        /// <summary>
        ///     Gets the roll.
        /// </summary>
        /// <value>The roll.</value>
        public String Roll { get; private set; }

        #endregion

        #region Ctor

        /// <summary>
        ///     Initializes a new instance of the <see cref="ParsRollException" /> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="roll">The roll.</param>
        public ParsRollException( String message, String roll )
            : base( message )
        {
            Roll = roll;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ParsRollException" /> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="roll">The roll.</param>
        /// <param name="innerException">
        ///     The exception that is the cause of the current exception, or a null reference (Nothing in
        ///     Visual Basic) if no inner exception is specified.
        /// </param>
        public ParsRollException( String message, String roll, Exception innerException )
            : base( message, innerException )
        {
            Roll = roll;
        }

        #endregion
    }
}