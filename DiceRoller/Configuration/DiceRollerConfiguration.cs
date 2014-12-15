using System;

namespace DiceRoller
{
    /// <summary>
    ///     Class representing the configuration of the DiceRoller application.
    /// </summary>
    public class DiceRollerConfiguration
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the result color.
        /// </summary>
        /// <value>The result color.</value>
        public ConsoleColor ResultColor { get; set; }

        /// <summary>
        ///     Gets or sets the error color.
        /// </summary>
        /// <value>The error color.</value>
        public ConsoleColor ErrorColor { get; set; }

        #endregion

        #region Public Members

        /// <summary>
        ///     Restores the default settings.
        /// </summary>
        public void RestoreDefault()
        {
            ResultColor = ConsoleColor.Green;
            ErrorColor = ConsoleColor.Red;
        }

        #endregion
    }
}