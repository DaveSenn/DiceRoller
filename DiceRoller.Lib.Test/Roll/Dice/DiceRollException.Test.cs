#region Usings

using NUnit.Framework;
using PortableExtensions;

#endregion

namespace DiceRoller.Lib.Test
{
    [TestFixture]
    public class DiceRollExceptionTest
    {
        [Test]
        public void CtorTest()
        {
            var message = RandomValueEx.GetRandomString();
            var roll = new DiceRoll();
            var target = new DiceRollException( message, roll );

            Assert.AreEqual( message, target.Message );
            Assert.AreSame( roll, target.Roll );
        }
    }
}