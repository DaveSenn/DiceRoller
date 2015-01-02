#region Usings

using System.Linq;
using DiceRoller.Extensions;
using DiceRoller.Lib.Lib;
using NUnit.Framework;

#endregion

namespace DiceRoller.Lib.Test
{
    [TestFixture]
    public class RollCalculatorTest
    {
        [Test]
        public void AddTwoDice()
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
                new Operator { OperatorType = RollOperator.Plus },
                new DiceRoll
                {
                    Dice = new Dice { Sides = 6 },
                    RollModifier = new RollModifier { RollModification = RollModification.Best, ModificationQuantifier = 5 },
                    NumberOfRolls = 2
                },
            };

            var actual = targeet.Calculate( roll )
                                .ToList();
            Assert.AreEqual( 1, actual.Count() );

            var result = actual[0];
            Assert.AreEqual( ResultType.SingleValue, result.Result.Type );
            Assert.AreEqual( 40, result.Log.Length );
            var resultValue = result.Result as SingleResult;
            Assert.IsTrue( resultValue.Result > 1 && resultValue.Result < 19,
                           "Result value '{0}' is out of range".F( resultValue.Result ) );
        }

        [Test]
        public void AddTwoDiceMultipleTimes()
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
                new Operator { OperatorType = RollOperator.Plus },
                new DiceRoll
                {
                    Dice = new Dice { Sides = 6 },
                    RollModifier = new RollModifier { RollModification = RollModification.Best, ModificationQuantifier = 5 },
                    NumberOfRolls = 2
                },
            };
            roll.NumberOfRepetitions = 4;

            var actual = targeet.Calculate( roll )
                                .ToList();
            Assert.AreEqual( 4, actual.Count() );

            actual.ForEach( result =>
            {
                Assert.AreEqual( ResultType.SingleValue, result.Result.Type );
                Assert.AreEqual( 40, result.Log.Length );
                var resultValue = result.Result as SingleResult;
                Assert.IsTrue( resultValue.Result > 1 && resultValue.Result < 19,
                               "Result value '{0}' is out of range".F( resultValue.Result ) );
            } );
        }

        [Test]
        public void CalculateDiceAllOperators()
        {
            var targeet = new RollCalculator();
            var roll = new Roll
            {
                new DiceRoll
                {
                    Dice = new Dice { Sides = 30 },
                    RollModifier = new RollModifier { RollModification = RollModification.None },
                    NumberOfRolls = 5
                },
                new Operator { OperatorType = RollOperator.Plus },
                new DiceRoll
                {
                    Dice = new Dice { Sides = 4 },
                    RollModifier = new RollModifier { RollModification = RollModification.Best, ModificationQuantifier = 5 },
                    NumberOfRolls = 2
                },
                new Operator { OperatorType = RollOperator.Divide },
                new DiceRoll
                {
                    Dice = new Dice { Sides = 6 },
                    RollModifier = new RollModifier { RollModification = RollModification.Worst, ModificationQuantifier = 5 },
                    NumberOfRolls = 3
                },
                new Operator { OperatorType = RollOperator.Minus },
                new DiceRoll
                {
                    Dice = new Dice { Sides = 8 },
                    RollModifier = new RollModifier { RollModification = RollModification.Worst, ModificationQuantifier = 10 },
                    NumberOfRolls = 1
                },
                new Operator { OperatorType = RollOperator.Times },
                new StaticValue { Value = 2 },
            };

            for ( var i = 0; i < 1000; i++ )
            {
                var actual = targeet.Calculate( roll )
                                    .ToList();
                Assert.AreEqual( 1, actual.Count() );

                var result = actual[0];
                Assert.AreEqual( ResultType.SingleValue, result.Result.Type );
                Assert.IsTrue( result.Log.Length > 144 && result.Log.Length < 152,
                               "Log has invalid length of '{0}' '{1}'".F( result.Log.Length, result.Log ) );
                var resultValue = result.Result as SingleResult;

                Assert.IsTrue( resultValue.Result > 2 && resultValue.Result < 150,
                               "Result value '{0}' is out of range".F( resultValue.Result ) );
            }
        }

        [Test]
        public void CalculateMap()
        {
            var targeet = new RollCalculator();
            var roll = new Roll
            {
                new DiceRoll
                {
                    Dice = new Dice { Sides = 9 },
                    RollModifier = new RollModifier { RollModification = RollModification.None },
                    NumberOfRolls = 3
                },
                new Operator { OperatorType = RollOperator.Map },
                new DiceRoll
                {
                    Dice = new Dice { Sides = 9 },
                    RollModifier = new RollModifier { RollModification = RollModification.None },
                    NumberOfRolls = 1
                },
            };

            var actual = targeet.Calculate( roll )
                                .ToList();
            Assert.AreEqual( 1, actual.Count() );

            var result = actual[0];
            Assert.AreEqual( ResultType.Map, result.Result.Type );
            Assert.AreEqual( 15, result.Log.Length );
            var resultValue = result.Result as MapResult;
            Assert.IsTrue( resultValue.Right > 0 && resultValue.Right < 10,
                           "Result value right '{0}' is out of range".F( resultValue.Right ) );
            Assert.IsTrue( resultValue.Left > 0 && resultValue.Right < 10,
                           "Result value left '{0}' is out of range".F( resultValue.Left ) );
        }

        [Test]
        public void CalculateSingleDice()
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

            var actual = targeet.Calculate( roll )
                                .ToList();
            Assert.AreEqual( 1, actual.Count() );

            var result = actual[0];
            Assert.AreEqual( ResultType.SingleValue, result.Result.Type );
            Assert.AreEqual( 3, result.Log.Length );
            var resultValue = result.Result as SingleResult;
            Assert.IsTrue( resultValue.Result > 0 && resultValue.Result < 7,
                           "Result value '{0}' is out of range".F( resultValue.Result ) );
        }

        [Test]
        public void CalculateSingleStaticValue()
        {
            var targeet = new RollCalculator();
            var roll = new Roll
            {
                new StaticValue { Value = 10 }
            };

            var actual = targeet.Calculate( roll )
                                .ToList();
            Assert.AreEqual( 1, actual.Count() );

            var result = actual[0];
            Assert.AreEqual( ResultType.SingleValue, result.Result.Type );
            Assert.AreEqual( "10", result.Log );
            var resultValue = result.Result as SingleResult;
            Assert.AreEqual( 10, resultValue.Result );
        }

        [Test]
        public void CalculateWithStaticValue()
        {
            var targeet = new RollCalculator();
            var roll = new Roll
            {
                new StaticValue { Value = 10 },
                new Operator { OperatorType = RollOperator.Minus },
                new StaticValue { Value = 7 },
            };

            var actual = targeet.Calculate( roll )
                                .ToList();
            Assert.AreEqual( 1, actual.Count() );

            var result = actual[0];
            Assert.AreEqual( ResultType.SingleValue, result.Result.Type );
            Assert.AreEqual( "10 - 7", result.Log );
            var resultValue = result.Result as SingleResult;
            Assert.AreEqual( 3, resultValue.Result );
        }

        [Test]
        public void CalculateWithStaticValue1()
        {
            var targeet = new RollCalculator();
            var roll = new Roll
            {
                new StaticValue { Value = 10 },
                new Operator { OperatorType = RollOperator.Minus },
                new StaticValue { Value = 7 },
                new Operator { OperatorType = RollOperator.Plus },
                new StaticValue { Value = 5 },
            };

            var actual = targeet.Calculate( roll )
                                .ToList();
            Assert.AreEqual( 1, actual.Count() );

            var result = actual[0];
            Assert.AreEqual( ResultType.SingleValue, result.Result.Type );
            Assert.AreEqual( "10 - 7 + 5", result.Log );
            var resultValue = result.Result as SingleResult;
            Assert.AreEqual( 8, resultValue.Result );
        }

        [Test]
        public void CalculateWithStaticValueAllOperators()
        {
            var targeet = new RollCalculator();
            var roll = new Roll
            {
                new StaticValue { Value = 10 },
                new Operator { OperatorType = RollOperator.Plus },
                new StaticValue { Value = 6 },
                new Operator { OperatorType = RollOperator.Divide },
                new StaticValue { Value = 2 },
                new Operator { OperatorType = RollOperator.Minus },
                new StaticValue { Value = 1 },
                new Operator { OperatorType = RollOperator.Times },
                new StaticValue { Value = 3 },
            };

            var actual = targeet.Calculate( roll )
                                .ToList();
            Assert.AreEqual( 1, actual.Count() );

            var result = actual[0];
            Assert.AreEqual( ResultType.SingleValue, result.Result.Type );
            Assert.AreEqual( "10 + 6 / 2 - 1 * 3", result.Log );
            var resultValue = result.Result as SingleResult;
            Assert.AreEqual( 10, resultValue.Result );
        }

        [Test]
        public void CalculateWithStaticValueLeftToRight()
        {
            var targeet = new RollCalculator();
            var roll = new Roll
            {
                new StaticValue { Value = 10 },
                new Operator { OperatorType = RollOperator.Divide },
                new StaticValue { Value = 2 },
                new Operator { OperatorType = RollOperator.Times },
                new StaticValue { Value = 3 },
            };

            var actual = targeet.Calculate( roll )
                                .ToList();
            Assert.AreEqual( 1, actual.Count() );

            var result = actual[0];
            Assert.AreEqual( ResultType.SingleValue, result.Result.Type );
            Assert.AreEqual( "10 / 2 * 3", result.Log );
            var resultValue = result.Result as SingleResult;
            Assert.AreEqual( 15, resultValue.Result );
        }

        [Test]
        public void CalculateWithStaticValueTimesBevoreMinus()
        {
            var targeet = new RollCalculator();
            var roll = new Roll
            {
                new StaticValue { Value = 10 },
                new Operator { OperatorType = RollOperator.Minus },
                new StaticValue { Value = 2 },
                new Operator { OperatorType = RollOperator.Times },
                new StaticValue { Value = 4 },
            };

            var actual = targeet.Calculate( roll )
                                .ToList();
            Assert.AreEqual( 1, actual.Count() );

            var result = actual[0];
            Assert.AreEqual( ResultType.SingleValue, result.Result.Type );
            Assert.AreEqual( "10 - 2 * 4", result.Log );
            var resultValue = result.Result as SingleResult;
            Assert.AreEqual( 2, resultValue.Result );
        }

        [Test]
        public void SubtractTwoDice()
        {
            var targeet = new RollCalculator();
            var roll = new Roll
            {
                new DiceRoll
                {
                    Dice = new Dice { Sides = 6 },
                    RollModifier = new RollModifier { RollModification = RollModification.Worst, ModificationQuantifier = 2 },
                    NumberOfRolls = 1
                },
                new Operator { OperatorType = RollOperator.Minus },
                new DiceRoll
                {
                    Dice = new Dice { Sides = 4 },
                    RollModifier = new RollModifier { RollModification = RollModification.None },
                    NumberOfRolls = 2
                },
            };

            var actual = targeet.Calculate( roll )
                                .ToList();
            Assert.AreEqual( 1, actual.Count() );

            var result = actual[0];
            Assert.AreEqual( ResultType.SingleValue, result.Result.Type );
            Assert.AreEqual( 17, result.Log.Length );
            var resultValue = result.Result as SingleResult;
            Assert.IsTrue( resultValue.Result > -8 && resultValue.Result < 6,
                           "Result value '{0}' is out of range".F( resultValue.Result ) );
        }
    }
}