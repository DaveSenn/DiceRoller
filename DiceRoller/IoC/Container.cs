namespace DiceRoller
{
    /// <summary>
    ///     Primitive replacement for a IoC container.
    /// </summary>
    public static class Container
    {
        /// <summary>
        ///     Gets or sets a <see cref="DiceRollerConfigurationManager" />.
        /// </summary>
        /// <value>A <see cref="DiceRollerConfigurationManager" />.</value>
        public static DiceRollerConfigurationManager ConfigurationManager { get; set; }
    }
}