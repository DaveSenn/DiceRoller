#region Usings

using DiceRoller.Lib;

#endregion

namespace DiceRoller
{
    /// <summary>
    ///     Primitive replacement for a IoC container.
    /// </summary>
    public static class Container
    {
        #region Properties

        /// <summary>
        ///     Gets or sets a <see cref="DiceRollerConfigurationManager" />.
        /// </summary>
        /// <value>A <see cref="DiceRollerConfigurationManager" />.</value>
        public static DiceRollerConfigurationManager ConfigurationManager { get; set; }

        /// <summary>
        ///     Gets or sets a <see cref="RollParser" />.
        /// </summary>
        /// <value>A <see cref="RollParser" />.</value>
        public static RollParser RollParser { get; set; }

        #endregion
    }
}