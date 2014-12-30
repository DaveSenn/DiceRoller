#region Usings

using NUnit.Framework;
using PortableExtensions;

#endregion

namespace DiceRoller.Lib.Test
{
    [TestFixture]
    public class OperatorTest
    {
        [Test]
        public void OperatorTypeTest()
        {
            var target = new Operator();
            var expected = RandomValueEx.GetRandomEnum<RollOperator>();
            target.OperatorType = expected;
            Assert.AreEqual( expected, target.OperatorType );
        }

        [Test]
        public void ToStringTest()
        {
            var target = new Operator
            {
                OperatorType = RollOperator.Plus
            };
            Assert.AreEqual( "+", target.ToString() );

            target.OperatorType = RollOperator.Minus;
            Assert.AreEqual( "-", target.ToString() );

            target.OperatorType = RollOperator.Times;
            Assert.AreEqual( "*", target.ToString() );

            target.OperatorType = RollOperator.Divide;
            Assert.AreEqual( "/", target.ToString() );

            target.OperatorType = RollOperator.Map;
            Assert.AreEqual( "=", target.ToString() );
        }

        [Test]
        public void TypeTest()
        {
            var target = new Operator();
            Assert.AreEqual( RollPartType.Operator, target.Type );
        }
    }
}