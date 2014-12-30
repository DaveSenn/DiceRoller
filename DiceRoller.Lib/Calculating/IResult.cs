namespace DiceRoller.Lib
{
    /// <summary>
    ///     Interface representing a result.
    /// </summary>
    public interface IResult
    {
        /// <summary>
        ///     Gets the result type.
        /// </summary>
        /// <value>The result type.</value>
        ResultType Type { get; }
    }
}