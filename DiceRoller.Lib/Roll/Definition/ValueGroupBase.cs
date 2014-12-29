#region Usings

using System;

#endregion

namespace DiceRoller.Lib
{
    /// <summary>
    ///     Abstract base class for value group types.
    /// </summary>
    public abstract class ValueGroupBase : IValueGroup
    {
        #region Implementation of IRollPart

        /// <summary>
        ///     Gets or sets the type of the roll part.
        /// </summary>
        /// <value>The type of the roll part.</value>
        public RollPartType Type
        {
            get { return RollPartType.ValueGroup; }
        }

        #endregion

        #region Implementation of IValueGroup

        /// <summary>
        ///     Gets the result of the value group.
        /// </summary>
        /// <returns>Returns the value.</returns>
        public abstract Int32 GetValue();

        /// <summary>
        ///     Gets the log containing the information how the value was created.
        /// </summary>
        /// <returns>Returns the log.</returns>
        public abstract String GetLog();

        #endregion
    }
}