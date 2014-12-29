#region Usings

using System;
using System.Collections.Generic;

#endregion

namespace DiceRoller.Lib
{
    /// <summary>
    ///     Class representing a roll.
    /// </summary>
    public class Roll : List<IRollPart>
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the number of repetitions of the roll.
        /// </summary>
        /// <value>The number of repetitions of the roll.</value>
        public Int32 NumberOfRepetitions { get; set; }

        #endregion

        #region Ctor

        /// <summary>
        ///     Initialize a new instance of the <see cref="Roll" /> class.
        /// </summary>
        public Roll()
        {
            NumberOfRepetitions = 1;
        }

        #endregion
    }
}