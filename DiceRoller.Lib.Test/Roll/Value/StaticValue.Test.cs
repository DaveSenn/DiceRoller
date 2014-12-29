#region Usings

using System.Globalization;
using NUnit.Framework;
using PortableExtensions;

#endregion

namespace DiceRoller.Lib.Test
{
    [TestFixture]
    public class StaticValueTest
    {
        [Test]
        public void GetLogTest()
        {
            var target = new StaticValue();
            var value = RandomValueEx.GetRandomInt32();
            target.Value = value;

            var expected = value.ToString( CultureInfo.InvariantCulture );
            Assert.AreEqual( expected, target.GetLog() );
        }

        [Test]
        public void GetValueTest()
        {
            var target = new StaticValue();
            var expected = RandomValueEx.GetRandomInt32();
            target.Value = expected;
            Assert.AreEqual( expected, target.GetValue() );
        }

        [Test]
        public void ToStringTest()
        {
            var target = new StaticValue();
            var value = RandomValueEx.GetRandomInt32();
            target.Value = value;

            var expected = value.ToString( CultureInfo.InvariantCulture );
            Assert.AreEqual( expected, target.ToString() );
        }

        [Test]
        public void TypeTest()
        {
            var target = new StaticValue();
            Assert.AreEqual( RollPartType.ValueGroup, target.Type );
        }

        [Test]
        public void ValueTest()
        {
            var target = new StaticValue();
            var expected = RandomValueEx.GetRandomInt32();
            target.Value = expected;
            Assert.AreEqual( expected, target.Value );
        }
    }
}