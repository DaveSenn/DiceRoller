namespace DiceRoller.Lib
{
    /// <summary>
    ///     Interface representing a part of a roll.
    /// </summary>
    public interface IRollPart
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the type of the roll part.
        /// </summary>
        /// <value>The type of the roll part.</value>
        RollPartType Type { get; }

        #endregion
    }
}