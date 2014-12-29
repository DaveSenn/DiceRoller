namespace DiceRoller.Lib
{
    /// <summary>
    ///     Enumeration of all roll modifications.
    /// </summary>
    public enum RollModification
    {
        /// <summary>
        ///     No modification.
        /// </summary>
        None = 0,

        /// <summary>
        ///     Roll dice N times, take best result.
        /// </summary>
        Best = 3,

        /// <summary>
        ///     Roll dice N times, take worst result.
        /// </summary>
        Worst = 4
    }
}