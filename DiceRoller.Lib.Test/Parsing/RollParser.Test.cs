#region Usings

using System;
using System.Linq;
using NUnit.Framework;

#endregion

namespace DiceRoller.Lib.Test
{
    [TestFixture]
    public class RollParserTest
    {
        [Test]
        public void CanParsRepetitionQuantifier()
        {
            var target = new RollParser();
            var actual = target.ParsRoll( "100x" );
            Assert.AreEqual( 100, actual.NumberOfRepetitions );
        }

        [Test]
        public void CanParsRepetitionQuantifier1()
        {
            var target = new RollParser();
            var actual = target.ParsRoll( "10X" );
            Assert.AreEqual( 10, actual.NumberOfRepetitions );
        }

        [Test]
        public void DefaultRepetitionQuantifierWorks()
        {
            var target = new RollParser();
            var actual = target.ParsRoll( "1d6" );
            Assert.AreEqual( 1, actual.NumberOfRepetitions );

            Assert.AreEqual( 1, actual.Count );
            Assert.AreEqual( 1, ( actual[0] as DiceRoll ).NumberOfRolls );
            Assert.AreEqual( 6, ( actual[0] as DiceRoll ).Dice.Sides );
            Assert.AreEqual( RollModification.None, ( actual[0] as DiceRoll ).RollModifier.RollModification );
        }

        [Test]
        [ExpectedException( typeof (ParsRollException) )]
        public void EmptyStringTrowsException()
        {
            var target = new RollParser();
            target.ParsRoll( String.Empty );
        }

        [Test]
        [ExpectedException( typeof (ParsRollException) )]
        public void FirstGroupIsOperator()
        {
            var target = new RollParser();
            var actual = target.ParsRoll( "+" );
        }

        [Test]
        [ExpectedException( typeof (ParsRollException) )]
        public void FirstGroupIsOperator1()
        {
            var target = new RollParser();
            var actual = target.ParsRoll( "10x+" );
        }

        [Test]
        [ExpectedException( typeof (ParsRollException) )]
        public void LastGroupCanNotBeOperator()
        {
            var target = new RollParser();
            target.ParsRoll( "1+2-" );
        }

        [Test]
        [ExpectedException( typeof (ParsRollException) )]
        public void NullStringTrowsException()
        {
            var target = new RollParser();
            target.ParsRoll( null );
        }

        [Test]
        public void ParsComplexDice()
        {
            var target = new RollParser();
            var actual = target.ParsRoll( "2d4b10" );
            Assert.AreEqual( 1, actual.NumberOfRepetitions );

            Assert.AreEqual( 1, actual.Count );
            var item = actual.First() as DiceRoll;

            Assert.AreEqual( 2, item.NumberOfRolls );
            Assert.AreEqual( 4, item.Dice.Sides );
            Assert.AreEqual( RollModification.Best, item.RollModifier.RollModification );
            Assert.AreEqual( 10, item.RollModifier.ModificationQuantifier );
        }

        [Test]
        public void ParsComplexDice1()
        {
            var target = new RollParser();
            var actual = target.ParsRoll( "2d4B" );
            Assert.AreEqual( 1, actual.NumberOfRepetitions );

            Assert.AreEqual( 1, actual.Count );
            var item = actual.First() as DiceRoll;

            Assert.AreEqual( 2, item.NumberOfRolls );
            Assert.AreEqual( 4, item.Dice.Sides );
            Assert.AreEqual( RollModification.Best, item.RollModifier.RollModification );
            Assert.AreEqual( 2, item.RollModifier.ModificationQuantifier );
        }

        [Test]
        public void ParsComplexDice2()
        {
            var target = new RollParser();
            var actual = target.ParsRoll( "2d4w10" );
            Assert.AreEqual( 1, actual.NumberOfRepetitions );

            Assert.AreEqual( 1, actual.Count );
            var item = actual.First() as DiceRoll;

            Assert.AreEqual( 2, item.NumberOfRolls );
            Assert.AreEqual( 4, item.Dice.Sides );
            Assert.AreEqual( RollModification.Worst, item.RollModifier.RollModification );
            Assert.AreEqual( 10, item.RollModifier.ModificationQuantifier );
        }

        [Test]
        public void ParsComplexDice3()
        {
            var target = new RollParser();
            var actual = target.ParsRoll( "2d4W" );
            Assert.AreEqual( 1, actual.NumberOfRepetitions );

            Assert.AreEqual( 1, actual.Count );
            var item = actual.First() as DiceRoll;

            Assert.AreEqual( 2, item.NumberOfRolls );
            Assert.AreEqual( 4, item.Dice.Sides );
            Assert.AreEqual( RollModification.Worst, item.RollModifier.RollModification );
            Assert.AreEqual( 2, item.RollModifier.ModificationQuantifier );
        }

        [Test]
        public void ParsDiceMath()
        {
            var target = new RollParser();
            var actual = target.ParsRoll( "1d20+1d4" );
            Assert.AreEqual( 1, actual.NumberOfRepetitions );

            Assert.AreEqual( 3, actual.Count );
            var item = actual.First() as DiceRoll;

            Assert.AreEqual( 1, item.NumberOfRolls );
            Assert.AreEqual( 20, item.Dice.Sides );
            Assert.AreEqual( RollModification.None, item.RollModifier.RollModification );

            Assert.AreEqual( RollOperator.Plus, ( actual[1] as Operator ).OperatorType );

            item = actual[2] as DiceRoll;
            Assert.AreEqual( 1, item.NumberOfRolls );
            Assert.AreEqual( 4, item.Dice.Sides );
            Assert.AreEqual( RollModification.None, item.RollModifier.RollModification );
        }

        [Test]
        public void ParsDiceMath1()
        {
            var target = new RollParser();
            var actual = target.ParsRoll( "d20+d4" );
            Assert.AreEqual( 1, actual.NumberOfRepetitions );

            Assert.AreEqual( 3, actual.Count );
            var item = actual.First() as DiceRoll;

            Assert.AreEqual( 1, item.NumberOfRolls );
            Assert.AreEqual( 20, item.Dice.Sides );
            Assert.AreEqual( RollModification.None, item.RollModifier.RollModification );

            Assert.AreEqual( RollOperator.Plus, ( actual[1] as Operator ).OperatorType );

            item = actual[2] as DiceRoll;
            Assert.AreEqual( 1, item.NumberOfRolls );
            Assert.AreEqual( 4, item.Dice.Sides );
            Assert.AreEqual( RollModification.None, item.RollModifier.RollModification );
        }

        [Test]
        public void ParsDiceMath2()
        {
            var target = new RollParser();
            var actual = target.ParsRoll( "d20+d4=d12b" );
            Assert.AreEqual( 1, actual.NumberOfRepetitions );

            Assert.AreEqual( 5, actual.Count );
            var item = actual.First() as DiceRoll;

            Assert.AreEqual( 1, item.NumberOfRolls );
            Assert.AreEqual( 20, item.Dice.Sides );
            Assert.AreEqual( RollModification.None, item.RollModifier.RollModification );

            Assert.AreEqual( RollOperator.Plus, ( actual[1] as Operator ).OperatorType );

            item = actual[2] as DiceRoll;
            Assert.AreEqual( 1, item.NumberOfRolls );
            Assert.AreEqual( 4, item.Dice.Sides );
            Assert.AreEqual( RollModification.None, item.RollModifier.RollModification );

            Assert.AreEqual( RollOperator.Map, ( actual[3] as Operator ).OperatorType );
            item = actual[4] as DiceRoll;
            Assert.AreEqual( 1, item.NumberOfRolls );
            Assert.AreEqual( 12, item.Dice.Sides );
            Assert.AreEqual( RollModification.Best, item.RollModifier.RollModification );
            Assert.AreEqual( 2, item.RollModifier.ModificationQuantifier );
        }

        [Test]
        [ExpectedException(typeof(ParsRollException))]
        public void MultipleMapOperators()
        {
            var target = new RollParser();
            var actual = target.ParsRoll("d20+d4=d12b=d12");
            Assert.AreEqual(1, actual.NumberOfRepetitions);

            Assert.AreEqual(5, actual.Count);
            var item = actual.First() as DiceRoll;

            Assert.AreEqual(1, item.NumberOfRolls);
            Assert.AreEqual(20, item.Dice.Sides);
            Assert.AreEqual(RollModification.None, item.RollModifier.RollModification);

            Assert.AreEqual(RollOperator.Plus, (actual[1] as Operator).OperatorType);

            item = actual[2] as DiceRoll;
            Assert.AreEqual(1, item.NumberOfRolls);
            Assert.AreEqual(4, item.Dice.Sides);
            Assert.AreEqual(RollModification.None, item.RollModifier.RollModification);

            Assert.AreEqual(RollOperator.Map, (actual[3] as Operator).OperatorType);
            item = actual[4] as DiceRoll;
            Assert.AreEqual(1, item.NumberOfRolls);
            Assert.AreEqual(12, item.Dice.Sides);
            Assert.AreEqual(RollModification.Best, item.RollModifier.RollModification);
            Assert.AreEqual(2, item.RollModifier.ModificationQuantifier);
        }

        [Test]
        public void ParsDiceMath3()
        {
            var target = new RollParser();
            var actual = target.ParsRoll( "3xd20b+6+2=d12b+2" );
            Assert.AreEqual( 3, actual.NumberOfRepetitions );

            Assert.AreEqual( 9, actual.Count );
            var item = actual.First() as DiceRoll;

            Assert.AreEqual( 1, item.NumberOfRolls );
            Assert.AreEqual( 20, item.Dice.Sides );
            Assert.AreEqual( RollModification.Best, item.RollModifier.RollModification );
            Assert.AreEqual( 2, item.RollModifier.ModificationQuantifier );

            Assert.AreEqual( RollOperator.Plus, ( actual[1] as Operator ).OperatorType );

            Assert.AreEqual( 6, ( actual[2] as StaticValue ).Value );

            Assert.AreEqual( RollOperator.Plus, ( actual[3] as Operator ).OperatorType );

            Assert.AreEqual( 2, ( actual[4] as StaticValue ).Value );

            Assert.AreEqual( RollOperator.Map, ( actual[5] as Operator ).OperatorType );

            item = actual[6] as DiceRoll;
            Assert.AreEqual( 1, item.NumberOfRolls );
            Assert.AreEqual( 12, item.Dice.Sides );
            Assert.AreEqual( RollModification.Best, item.RollModifier.RollModification );
            Assert.AreEqual( 2, item.RollModifier.ModificationQuantifier );

            Assert.AreEqual( RollOperator.Plus, ( actual[7] as Operator ).OperatorType );

            Assert.AreEqual( 2, ( actual[8] as StaticValue ).Value );
        }

        [Test]
        public void ParsMultipleNumberGroups()
        {
            var target = new RollParser();
            var actual = target.ParsRoll( "1+2-3*4/5" );

            Assert.AreEqual( 1, actual.NumberOfRepetitions );
            Assert.AreEqual( 9, actual.Count );

            Assert.AreEqual( 1, ( actual[0] as StaticValue ).Value );
            Assert.AreEqual( RollOperator.Plus, ( actual[1] as IOperator ).OperatorType );

            Assert.AreEqual( 2, ( actual[2] as StaticValue ).Value );
            Assert.AreEqual( RollOperator.Minus, ( actual[3] as IOperator ).OperatorType );

            Assert.AreEqual( 3, ( actual[4] as StaticValue ).Value );
            Assert.AreEqual( RollOperator.Times, ( actual[5] as IOperator ).OperatorType );

            Assert.AreEqual( 4, ( actual[6] as StaticValue ).Value );
            Assert.AreEqual( RollOperator.Divide, ( actual[7] as IOperator ).OperatorType );

            Assert.AreEqual( 5, ( actual[8] as StaticValue ).Value );
        }

        [Test]
        public void ParsMultipleNumberGroups1()
        {
            var target = new RollParser();
            var actual = target.ParsRoll( "1+20-300*40000/500000" );

            Assert.AreEqual( 1, actual.NumberOfRepetitions );
            Assert.AreEqual( 9, actual.Count );

            Assert.AreEqual( 1, ( actual[0] as StaticValue ).Value );
            Assert.AreEqual( RollOperator.Plus, ( actual[1] as IOperator ).OperatorType );

            Assert.AreEqual( 20, ( actual[2] as StaticValue ).Value );
            Assert.AreEqual( RollOperator.Minus, ( actual[3] as IOperator ).OperatorType );

            Assert.AreEqual( 300, ( actual[4] as StaticValue ).Value );
            Assert.AreEqual( RollOperator.Times, ( actual[5] as IOperator ).OperatorType );

            Assert.AreEqual( 40000, ( actual[6] as StaticValue ).Value );
            Assert.AreEqual( RollOperator.Divide, ( actual[7] as IOperator ).OperatorType );

            Assert.AreEqual( 500000, ( actual[8] as StaticValue ).Value );
        }

        [Test]
        public void ParsSimpleMapping()
        {
            var target = new RollParser();
            var actual = target.ParsRoll( "12=20" );

            Assert.AreEqual( 1, actual.NumberOfRepetitions );
            Assert.AreEqual( 3, actual.Count );

            Assert.AreEqual( 12, ( actual[0] as StaticValue ).Value );
            Assert.AreEqual( RollOperator.Map, ( actual[1] as IOperator ).OperatorType );
            Assert.AreEqual( 20, ( actual[2] as StaticValue ).Value );
        }

        [Test]
        public void ParsSimpleMappingWithQuantifier()
        {
            var target = new RollParser();
            var actual = target.ParsRoll( "3x12=20" );

            Assert.AreEqual( 3, actual.NumberOfRepetitions );
            Assert.AreEqual( 3, actual.Count );

            Assert.AreEqual( 12, ( actual[0] as StaticValue ).Value );
            Assert.AreEqual( RollOperator.Map, ( actual[1] as IOperator ).OperatorType );
            Assert.AreEqual( 20, ( actual[2] as StaticValue ).Value );
        }

        [Test]
        public void ParsSimpleMappingWithQuantifier1()
        {
            var target = new RollParser();
            var actual = target.ParsRoll( "3X12=20" );

            Assert.AreEqual( 3, actual.NumberOfRepetitions );
            Assert.AreEqual( 3, actual.Count );

            Assert.AreEqual( 12, ( actual[0] as StaticValue ).Value );
            Assert.AreEqual( RollOperator.Map, ( actual[1] as IOperator ).OperatorType );
            Assert.AreEqual( 20, ( actual[2] as StaticValue ).Value );
        }

        [Test]
        public void ParsSingleNumber()
        {
            var target = new RollParser();
            var actual = target.ParsRoll( "12" );

            Assert.AreEqual( 1, actual.NumberOfRepetitions );
            Assert.AreEqual( 1, actual.Count );
            Assert.AreEqual( 12, ( actual.First() as StaticValue ).Value );
        }

        [Test]
        public void ParsSingleNumberWithQuantifier()
        {
            var target = new RollParser();
            var actual = target.ParsRoll( "5x12" );

            Assert.AreEqual( 5, actual.NumberOfRepetitions );
            Assert.AreEqual( 1, actual.Count );
            Assert.AreEqual( 12, ( actual.First() as StaticValue ).Value );
        }

        [Test]
        [ExpectedException( typeof (ParsRollException) )]
        public void TwoOperatorsInARowOperator()
        {
            var target = new RollParser();
            var actual = target.ParsRoll( "1+-" );
        }

        [Test]
        [ExpectedException( typeof (ParsRollException) )]
        public void TwoOperatorsInARowOperator1()
        {
            var target = new RollParser();
            var actual = target.ParsRoll( "1+10*/" );
        }
    }
}