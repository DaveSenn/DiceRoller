#region Usings

using NUnit.Framework;
using PortableExtensions;

#endregion

namespace DiceRoller.Lib.Test
{
    [TestFixture]
    public class RollModifierTest
    {
        [Test]
        public void CtorTest()
        {
            var target = new RollModifier();
            Assert.AreEqual( 2, target.ModificationQuantifier );
        }

        [Test]
        public void ModificationQuantifierTest()
        {
            var target = new RollModifier();
            var expected = RandomValueEx.GetRandomInt32();
            target.ModificationQuantifier = expected;
            Assert.AreEqual( expected, target.ModificationQuantifier );
        }

        [Test]
        public void RollModificationTest()
        {
            var target = new RollModifier();
            var expected = RandomValueEx.GetRandomEnum<RollModification>();
            target.RollModification = expected;
            Assert.AreEqual( expected, target.RollModification );
        }
    }
}