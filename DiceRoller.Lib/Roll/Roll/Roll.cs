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

        /// <summary>
        ///     Initializes a new instance of the <see cref="Roll" /> class
        ///     that contains elements copied from the specified collection and has sufficient
        ///     capacity to accommodate the number of elements copied.
        /// </summary>
        /// <exception cref="ArgumentNullException">collection is null.</exception>
        /// <param name="collection">The collection whose elements are copied to the new list.</param>
        public Roll( IEnumerable<IRollPart> collection )
            : base( collection )
        {
            NumberOfRepetitions = 1;
        }

        #endregion
    }
}