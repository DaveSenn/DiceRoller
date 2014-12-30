#region Usings

using System;

#endregion

namespace DiceRoller.Lib
{
    /// <summary>
    ///     Class representing the result of a roll.
    /// </summary>
    public class RollResult
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the result.
        /// </summary>
        /// <value>The result.</value>
        public IResult Result { get; set; }

        /// <summary>
        ///     Gets or sets the calculation log.
        /// </summary>
        /// <value>The calculation log.</value>
        public String Log { get; set; }

        #endregion
    }
}