#region Usings

using System;

#endregion

namespace DiceRoller.Lib
{
    /// <summary>
    ///     Class representing a roll modifier.
    /// </summary>
    public class RollModifier
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the roll modification.
        /// </summary>
        /// <value>The roll modification.</value>
        public RollModification RollModification { get; set; }

        /// <summary>
        ///     Gets or sets the modification quantifier.
        /// </summary>
        /// <value>The modification quantifier.</value>
        public Int32 ModificationQuantifier { get; set; }

        #endregion

        #region Ctor

        /// <summary>
        ///     Initialize a new instance of the <see cref="ModificationQuantifier" /> class.
        /// </summary>
        public RollModifier()
        {
            ModificationQuantifier = 2;
        }

        #endregion
    }
}