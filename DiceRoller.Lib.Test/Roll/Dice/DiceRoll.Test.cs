#region Usings

using System;
using DiceRoller.Extensions;
using DiceRoller.Lib.Lib;
using NUnit.Framework;

#endregion

namespace DiceRoller.Lib.Test
{
    [TestFixture]
    public class DiceRollTest
    {
        private const Int32 RollTestRepeat = 100 * 1000;

        [Test]
        public void CtorTest()
        {
            var target = new DiceRoll();
            Assert.AreEqual( 0, target.Dice.Sides );
            Assert.AreEqual( 1, target.NumberOfRolls );
            Assert.AreEqual( 2, target.RollModifier.ModificationQuantifier );
            Assert.AreEqual( RollModification.None, target.RollModifier.RollModification );
        }

        [Test]
        public void DiceTest()
        {
            var target = new DiceRoll();
            var expected = new Dice();
            target.Dice = expected;
            Assert.AreSame( expected, target.Dice );
        }

        [Test]
        public void GetLogTest()
        {
            var target = new DiceRoll
            {
                Dice = new Dice { Sides = 4 },
                NumberOfRolls = 19,
                RollModifier = new RollModifier { RollModification = RollModification.Worst, ModificationQuantifier = 2 }
            };

            var actual = target.GetLog();
            Assert.AreEqual( "[]", actual );

            for ( var i = 0; i < RollTestRepeat; i++ )
            {
                target.GetValue();
                actual = target.GetLog();
                Assert.AreEqual( 152, actual.Length );
                Assert.IsTrue( actual.StartsWith( "[" ) );
                Assert.IsTrue( actual.EndsWith( "]" ) );
            }
        }

        [Test]
        public void GetValueTest()
        {
            var target = new DiceRoll
            {
                Dice = new Dice { Sides = 4 },
                NumberOfRolls = 1,
                RollModifier = new RollModifier { RollModification = RollModification.None }
            };

            for ( var i = 0; i < RollTestRepeat; i++ )
            {
                var actual = target.GetValue();
                Assert.IsTrue( actual >= 1 && actual <= 4 );
            }
        }

        [Test]
        public void GetValueTest1()
        {
            var target = new DiceRoll
            {
                Dice = new Dice { Sides = 4 },
                NumberOfRolls = 3,
                RollModifier = new RollModifier { RollModification = RollModification.None }
            };

            for ( var i = 0; i < RollTestRepeat; i++ )
            {
                var actual = target.GetValue();
                Assert.IsTrue( actual >= 1 * 3 && actual <= 4 * 3 );
            }
        }

        [Test]
        public void GetValueTest2()
        {
            var target = new DiceRoll
            {
                Dice = new Dice { Sides = 4 },
                NumberOfRolls = 3,
                RollModifier = new RollModifier { RollModification = RollModification.Best, ModificationQuantifier = 10 }
            };

            for ( var i = 0; i < RollTestRepeat; i++ )
            {
                var actual = target.GetValue();
                Assert.IsTrue( actual >= 1 * 3 && actual <= 4 * 3 );
            }
        }

        [Test]
        public void GetValueTest3()
        {
            var target = new DiceRoll
            {
                Dice = new Dice { Sides = 4 },
                NumberOfRolls = 5,
                RollModifier = new RollModifier { RollModification = RollModification.Worst, ModificationQuantifier = 2 }
            };

            for ( var i = 0; i < RollTestRepeat; i++ )
            {
                var actual = target.GetValue();
                Assert.IsTrue( actual >= 1 * 5 && actual <= 4 * 5 );
            }
        }

        [Test]
        public void NumberOfRollsTest()
        {
            var target = new DiceRoll();
            var expected = RandomValueEx.GetRandomInt32();
            target.NumberOfRolls = expected;
            Assert.AreEqual( expected, target.NumberOfRolls );
        }

        [Test]
        public void RollModifierTest()
        {
            var target = new DiceRoll();
            var expected = new RollModifier();
            target.RollModifier = expected;
            Assert.AreSame( expected, target.RollModifier );
        }

        [Test]
        public void ToStringTest()
        {
            var target = new DiceRoll
            {
                Dice = new Dice { Sides = 6 },
                NumberOfRolls = 3,
                RollModifier = new RollModifier { RollModification = RollModification.Best }
            };

            const String expected = "3d6b";
            var actual = target.ToString();
            Assert.AreEqual( expected, actual );
        }

        [Test]
        public void ToStringTest1()
        {
            var target = new DiceRoll
            {
                Dice = new Dice { Sides = 6 },
                NumberOfRolls = 3,
                RollModifier = new RollModifier { RollModification = RollModification.Worst, ModificationQuantifier = 4 }
            };

            const String expected = "3d6w4";
            var actual = target.ToString();
            Assert.AreEqual( expected, actual );
        }

        [Test]
        public void ToStringTest2()
        {
            var target = new DiceRoll
            {
                Dice = new Dice { Sides = 6 },
            };

            const String expected = "1d6";
            var actual = target.ToString();
            Assert.AreEqual( expected, actual );
        }

        [Test]
        public void ToStringTest3()
        {
            var target = new DiceRoll
            {
                Dice = new Dice { Sides = 6 },
                NumberOfRolls = 3,
                RollModifier = new RollModifier { ModificationQuantifier = 5, RollModification = RollModification.Best }
            };

            const String expected = "3d6b5";
            var actual = target.ToString();
            Assert.AreEqual( expected, actual );
        }

        [Test]
        public void ToStringTest4()
        {
            var target = new DiceRoll
            {
                Dice = new Dice { Sides = 6 },
                NumberOfRolls = 3,
                RollModifier = new RollModifier { ModificationQuantifier = 2, RollModification = RollModification.Best }
            };

            const String expected = "3d6b";
            var actual = target.ToString();
            Assert.AreEqual( expected, actual );
        }

        [Test]
        public void TypeTest()
        {
            var target = new DiceRoll();
            Assert.AreEqual( RollPartType.ValueGroup, target.Type );
        }
    }
}