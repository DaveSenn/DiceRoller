#region Usings

using System;

#endregion

namespace DiceRoller.Lib
{
    /// <summary>
    ///     Interface representing a value group.
    /// </summary>
    public interface IValueGroup : IRollPart
    {
        /// <summary>
        ///     Gets the result of the value group.
        /// </summary>
        /// <returns>Returns the value.</returns>
        Int32 GetValue();

        /// <summary>
        ///     Gets the log containing the information how the value was created.
        /// </summary>
        /// <returns>Returns the log.</returns>
        String GetLog();
    }
}