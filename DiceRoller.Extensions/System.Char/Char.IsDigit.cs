﻿#region Usings

using System;

#endregion

namespace DiceRoller.Extensions
{
    /// <summary>
    ///     Class containing some extension methods for <see cref="char" />.
    /// </summary>
    public static class CharEx
    {
        /// <summary>
        ///     Indicates whether the specified Unicode character is categorized as a decimal digit.
        /// </summary>
        /// <param name="c">The Unicode character to evaluate.</param>
        /// <returns>True if the given char is a decimal digit, otherwise false.</returns>
        public static Boolean IsDigit( this Char c )
        {
            return Char.IsDigit( c );
        }
    }
}