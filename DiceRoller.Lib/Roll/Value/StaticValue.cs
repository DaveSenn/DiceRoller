#region Usings

using System;
using System.Globalization;

#endregion

namespace DiceRoller.Lib
{
    /// <summary>
    ///     Class representing a static value.
    /// </summary>
    public class StaticValue : ValueGroupBase, IValueGroup
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public Int32 Value { get; set; }

        #endregion

        #region Implementation of IValueGroup

        /// <summary>
        ///     Gets the result of the value group.
        /// </summary>
        /// <returns>Returns the value.</returns>
        public override Int32 GetValue()
        {
            return Value;
        }

        /// <summary>
        ///     Gets the log containing the information how the value was created.
        /// </summary>
        /// <returns>Returns the log.</returns>
        public override String GetLog()
        {
            return Value.ToString( CultureInfo.InvariantCulture );
        }

        #endregion

        #region Overrides of Object

        /// <summary>
        ///     Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        ///     A string that represents the current object.
        /// </returns>
        public override String ToString()
        {
            return GetLog();
        }

        #endregion
    }
}