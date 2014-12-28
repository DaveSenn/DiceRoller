#region Usings

using System;

#endregion

namespace DiceRoller
{
    /// <summary>
    ///     Exception thrown when a parsing has no default action.
    /// </summary>
    public class NoDefaultException : Exception
    {
        #region Ctor

        /// <summary>
        ///     Initializes a new instance of the <see cref="NoDefaultException" /> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public NoDefaultException( String message )
            : base( message )
        {
        }

        #endregion

    }
}