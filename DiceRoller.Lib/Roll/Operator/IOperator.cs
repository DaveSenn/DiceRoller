namespace DiceRoller.Lib
{
    /// <summary>
    ///     Interface representing an operator.
    /// </summary>
    public interface IOperator : IRollPart
    {
        /// <summary>
        ///     Gets or sets the operator type.
        /// </summary>
        /// <value>The operator type.</value>
        RollOperator OperatorType { get; set; }
    }
}