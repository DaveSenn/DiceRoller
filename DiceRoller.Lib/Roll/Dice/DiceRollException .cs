#region Usings

using System;

#endregion

namespace DiceRoller.Lib
{
    /// <summary>
    ///     Exception thrown when a dice rolls fails.
    /// </summary>
    public class DiceRollException : Exception
    {
        #region Properties

        /// <summary>
        ///     Gets the failed roll.
        /// </summary>
        /// <value>The failed roll.</value>
        public DiceRoll Roll { get; private set; }

        #endregion

        #region Ctor

        /// <summary>
        ///     Initializes a new instance of the <see cref="DiceRollException" /> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="roll">The failed roll.</param>
        public DiceRollException( String message, DiceRoll roll )
            : base( message )
        {
            Roll = roll;
        }

        #endregion
    }
}