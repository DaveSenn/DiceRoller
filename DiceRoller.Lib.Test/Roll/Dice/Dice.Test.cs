#region Usings

using DiceRoller.Lib.Lib;
using NUnit.Framework;
using PortableExtensions;

#endregion

namespace DiceRoller.Lib.Test
{
    [TestFixture]
    public class DiceTest
    {
        public void SidesTest()
        {
            var target = new Dice();
            var expected = RandomValueEx.GetRandomInt32();
            target.Sides = expected;
            Assert.AreEqual( expected, target.Sides );
        }
    }
}