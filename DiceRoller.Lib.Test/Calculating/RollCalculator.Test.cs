using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiceRoller.Lib.Lib;
using NUnit.Framework;

namespace DiceRoller.Lib.Test
{
    [TestFixture]
    public class RollCalculatorTest
    {
        [Test]
        public void SimpleCalculateTest()
        {
            var targeet = new RollCalculator();
            var roll = new Roll
            {
                new DiceRoll
                {
                    Dice = new Dice { Sides = 6 },
                    RollModifier = new RollModifier { RollModification = RollModification.None },
                    NumberOfRolls = 1
                }
            };

            var actual = targeet.Calculate( roll );
        }

        [Test]
        public void CalculateTwoDice()
        {
            var targeet = new RollCalculator();
            var roll = new Roll
            {
                new DiceRoll
                {
                    Dice = new Dice { Sides = 6 },
                    RollModifier = new RollModifier { RollModification = RollModification.None },
                    NumberOfRolls = 1
                },
                new Operator{ OperatorType = RollOperator.Plus},
                new DiceRoll
                {
                    Dice = new Dice { Sides = 6 },
                    RollModifier = new RollModifier { RollModification = RollModification.Best, ModificationQuantifier = 5 },
                    NumberOfRolls = 2
                },
            };

            var actual = targeet.Calculate(roll);
        }
    }
}
