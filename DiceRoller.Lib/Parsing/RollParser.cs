#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using PortableExtensions;

#endregion

namespace DiceRoller.Lib
{
    /// <summary>
    ///     Class containing the logic to pars a roll.
    /// </summary>
    public class RollParser
    {
        #region Consts

        /// <summary>
        ///     Modifiers for picking best roll.
        /// </summary>
        private readonly List<Char> _bestModifiers = new List<Char> { 'b', 'B' };

        /// <summary>
        ///     List of characters representing a dice.
        /// </summary>
        private readonly List<Char> _diceCharacters = new List<Char> { 'd', 'D' };

        /// <summary>
        ///     The allowed dice roll modifiers.
        /// </summary>
        private readonly List<Char> _diceRollModifiers = new List<Char> { 'b', 'B', 'w', 'W' };

        /// <summary>
        ///     All allowed operators.
        /// </summary>
        private readonly List<Char> _operators = new List<Char> { '+', '-', '*', '/', '=' };

        /// <summary>
        ///     List of characters representing a quantifier.
        /// </summary>
        private readonly List<Char> _quantifierCharacters = new List<Char> { 'x', 'X' };

        /// <summary>
        ///     Modifiers for picking worst roll.
        /// </summary>
        private readonly List<Char> _worstModifiers = new List<Char> { 'w', 'W' };

        #endregion

        #region Public Members

        /// <summary>
        ///     Parses the given roll.
        /// </summary>
        /// <exception cref="ParsRollException">Parsing failed.</exception>
        /// <param name="roll">The roll to pars.</param>
        /// <returns>Returns the parsed roll.</returns>
        public Roll ParsRoll( String roll )
        {
            if ( roll.IsEmpty() )
                throw new ParsRollException( "Empty or null is not a valid roll.", roll );

            var result = new Roll();

            Int32 outValue;
            var remainingRoll = GetRepetitionQuantifier( roll, out outValue );
            result.NumberOfRepetitions = outValue;

            if ( remainingRoll.IsEmpty() )
                return result;

            //Get one group after the other
            while ( remainingRoll.IsNotEmpty() )
            {
                IRollPart part;
                remainingRoll = GetPart( remainingRoll, out part );

                //Check if part is valid
                if ( part == null )
                    throw new ParsRollException( "Invalid part received, part can not be null.", roll );

                if ( part.Type == RollPartType.Operator && result.NotAny() )
                    throw new ParsRollException( "First group can not be an operator.", roll );

                if ( part.Type == RollPartType.Operator && result.Any() && result.Last()
                                                                                 .Type == RollPartType.Operator )
                    throw new ParsRollException( "Two operator groups in a row is not allowed.", roll );

                result.Add( part );
            }

            if ( result.Any() && result.Last()
                                       .Type == RollPartType.Operator )
                throw new ParsRollException( "Last group can not be an operator.", roll );

            return result;
        }

        /// <summary>
        ///     Tries to pars a the given roll.
        /// </summary>
        /// <param name="roll">The roll to pars.</param>
        /// <param name="convertedRoll">The converted roll.</param>
        /// <returns>Returns a value of true if the roll was parsed successfully, otherwise false. </returns>
        public Boolean TryParsRoll( String roll, out Roll convertedRoll )
        {
            try
            {
                convertedRoll = ParsRoll( roll );
                return true;
            }
            catch ( ParsRollException )
            {
                convertedRoll = null;
                return false;
            }
        }

        #endregion

        #region Private Members

        /// <summary>
        ///     Gets the next group of the given roll.
        /// </summary>
        /// <exception cref="ParsRollException">Parsing failed.</exception>
        /// <param name="roll">Te roll.</param>
        /// <param name="part">The group as roll part.</param>
        /// <returns>Returns the remaining roll.</returns>
        private String GetPart( String roll, out IRollPart part )
        {
            //Check if next group is operator
            if ( IsOperator( roll ) )
            {
                Operator o;
                roll = GetOperator( roll, out o );
                part = o;
                return roll;
            }
            //Check if next group is static value
            if ( IsStaticValue( roll ) )
            {
                StaticValue staticValue;
                roll = GetStaticValue( roll, out staticValue );
                part = staticValue;
                return roll;
            }

            //Try pars dice roll
            try
            {
                DiceRoll diceRoll;
                roll = ParsDiceRoll( roll, out diceRoll );
                part = diceRoll;
                return roll;
            }
            catch ( ParsRollException ex )
            {
                throw new ParsRollException( "Group does not match any known types.", roll, ex );
            }
        }

        /// <summary>
        ///     Tries to pars a dice roll.
        /// </summary>
        /// <exception cref="ParsRollException">Parsing failed.</exception>
        /// <param name="roll">The roll.</param>
        /// <param name="diceRoll">The parsed dice roll.</param>
        /// <returns>Returns the remaining roll.</returns>
        private String ParsDiceRoll( String roll, out DiceRoll diceRoll )
        {
            diceRoll = new DiceRoll();
            Int32 outValue;

            //Get number of rolls
            var remainingRoll = GetDiceRollQuantifier( roll, out outValue );
            diceRoll.NumberOfRolls = outValue;

            //Get dice side count
            remainingRoll = GetDiceSideCount( remainingRoll, out outValue );
            diceRoll.Dice.Sides = outValue;

            //Get roll modification
            RollModification rollModification;
            remainingRoll = GetRollModification( remainingRoll, out rollModification );
            diceRoll.RollModifier.RollModification = rollModification;

            //Roll can not have a modification quantifier
            if ( rollModification == RollModification.None )
                return remainingRoll;

            //Get modification quantifier
            remainingRoll = GetRollModificationQuantifier( remainingRoll, out outValue );
            diceRoll.RollModifier.ModificationQuantifier = outValue;

            return remainingRoll;
        }

        /// <summary>
        ///     Gets the roll modifier quantifier from the given roll.
        /// </summary>
        /// <exception cref="ParsRollException">Parsing failed.</exception>
        /// <param name="roll">The roll.</param>
        /// <param name="modificationQuantifier">The parsed modification quantifier.</param>
        /// <returns>Returns the remaining roll.</returns>
        private String GetRollModificationQuantifier( String roll, out Int32 modificationQuantifier )
        {
            //No quantifier specified
            if ( roll.IsEmpty() || !roll.First()
                                        .IsDigit() )
            {
                modificationQuantifier = 2;
                return roll;
            }

            //Pars the first number
            return GetFirstNumber( roll, out modificationQuantifier );
        }

        /// <summary>
        ///     Gets the roll modification from the given roll.
        /// </summary>
        /// <exception cref="ParsRollException">Parsing failed.</exception>
        /// <param name="roll">The roll.</param>
        /// <param name="rollModification">The parsed roll modification.</param>
        /// <returns>Returns the remaining roll.</returns>
        private String GetRollModification( String roll, out RollModification rollModification )
        {
            //Check if the roll has a modifier
            if ( !IsDiceRollModifier( roll ) )
            {
                rollModification = RollModification.None;
                return roll;
            }

            //Get modifier
            var first = roll.First();

            if ( first.IsIn( _bestModifiers ) )
                rollModification = RollModification.Best;
            else if ( first.IsIn( _worstModifiers ) )
                rollModification = RollModification.Worst;
            else
                throw new ParsRollException( "Failed to pars dice roll modification.", roll );

            return roll.Substring( 1 );
        }

        /// <summary>
        ///     Gets the number of sites of a dice.
        /// </summary>
        /// <exception cref="ParsRollException">Parsing failed.</exception>
        /// <param name="roll">The roll.</param>
        /// <param name="sideCount">The parsed number of sites.</param>
        /// <returns>Returns the remaining roll.</returns>
        private String GetDiceSideCount( String roll, out Int32 sideCount )
        {
            //Check if roll starts with a digit
            if ( !roll.First()
                      .IsDigit() )
                throw new ParsRollException( "Failed to pars side count, first character must be a digit.", roll );

            //Try to pars the side count.
            return GetFirstNumber( roll, out sideCount );
        }

        /// <summary>
        ///     Checks whether the given roll starts with a dice roll modifier or not.
        /// </summary>
        /// <param name="roll">The roll.</param>
        /// <returns>Returns a value of true if the roll starts with a dice roll modifier; otherwise, false.</returns>
        private Boolean IsDiceRollModifier( String roll )
        {
            return roll.IsNotEmpty() && roll.First()
                                            .IsIn( _diceRollModifiers );
        }

        /// <summary>
        ///     Gets the dice roll quantifier from the given roll.
        /// </summary>
        /// <exception cref="ParsRollException">Parsing failed.</exception>
        /// <param name="roll">The roll</param>
        /// <param name="quantifier">The parsed quantifier.</param>
        /// <returns>Returns the remaining roll.</returns>
        private String GetDiceRollQuantifier( String roll, out Int32 quantifier )
        {
            //Check for short dice notation.
            if ( roll.First()
                     .IsIn( _diceCharacters ) )
            {
                quantifier = 1;
                return roll.Substring( 1 );
            }

            //Check if roll could be a dice roll
            if ( !roll.First()
                      .IsDigit() || !roll.ContainsAny( _diceCharacters ) )
                throw new ParsRollException( "Given string is not a valid dice roll group", roll );

            //Try get quantifier from 1d6 notation
            return GetNumberUntil( roll, out quantifier, true, 'd' );
        }

        /// <summary>
        ///     Checks if the next group is a number.
        /// </summary>
        /// <param name="roll">The roll to check.</param>
        /// <returns>Returns a value of true if the next group is a number; otherwise, false.</returns>
        private Boolean IsStaticValue( String roll )
        {
            return IsNumberFollowedBy( roll, true, _operators );
        }

        /// <summary>
        ///     Pars the next group into an operator group.
        /// </summary>
        /// <exception cref="ParsRollException">Parsing failed.</exception>
        /// <param name="roll">The roll.</param>
        /// <param name="staticValue">The parsed static value.</param>
        /// <returns>Returns the remaining roll.</returns>
        private String GetStaticValue( String roll, out StaticValue staticValue )
        {
            Int32 value;
            roll = GetNumberUntil( roll, out value, _operators, false );
            staticValue = new StaticValue { Value = value };

            return roll;
        }

        /// <summary>
        ///     Checks if the next group is an operator.
        /// </summary>
        /// <param name="roll">The roll to check.</param>
        /// <returns>Returns a value of true if the next group is an operator; otherwise, false.</returns>
        private Boolean IsOperator( String roll )
        {
            return FirstCharIs( roll, _operators );
        }

        /// <summary>
        ///     Pars the next group into an operator group.
        /// </summary>
        /// <exception cref="ParsRollException">Parsing failed.</exception>
        /// <param name="roll">The roll.</param>
        /// <param name="o">The parsed operator.</param>
        /// <returns>Returns the remaining roll.</returns>
        private String GetOperator( String roll, out Operator o )
        {
            var operatorCharacter = roll.First();
            o = new Operator();

            switch ( operatorCharacter )
            {
                case '+':
                    o.OperatorType = RollOperator.Plus;
                    break;
                case '-':
                    o.OperatorType = RollOperator.Minus;
                    break;
                case '*':
                    o.OperatorType = RollOperator.Times;
                    break;
                case '/':
                    o.OperatorType = RollOperator.Divide;
                    break;
                case '=':
                    o.OperatorType = RollOperator.Map;
                    break;
                default:
                    throw new ParsRollException( "'{0}' is not a valid operator".F( operatorCharacter ), roll );
            }

            return roll.Substring( 1 );
        }

        /// <summary>
        ///     Gets the repetition quantifier specified in the given roll.
        /// </summary>
        /// <exception cref="ParsRollException">Parsing failed.</exception>
        /// <param name="roll">The roll.</param>
        /// <param name="numberOfRepetition">The number of repetitions.</param>
        /// <returns>Returns the remaining roll.</returns>
        private String GetRepetitionQuantifier( String roll, out Int32 numberOfRepetition )
        {
            //Roll contains repetition quantifier
            if ( IsNumberFollowedBy( roll, false, _quantifierCharacters ) )
                return GetNumberUntil( roll, out numberOfRepetition, _quantifierCharacters );

            //Repetition quantifier is not specified
            numberOfRepetition = 1;
            return roll;
        }

        /// <summary>
        ///     Gets a number from the beginning of the given string until one of the given values occurs.
        /// </summary>
        /// <exception cref="ParsRollException">Parsing failed.</exception>
        /// <param name="source">The source string.</param>
        /// <param name="number">The parsed number.</param>
        /// <param name="values">The values marking the end of the number.</param>
        /// <param name="removeTrailingCharacter">
        ///     A value indicating whether the last character after the number should ger removed
        ///     or not.
        /// </param>
        /// <returns>Returns the source string with number and value removed.</returns>
        private String GetNumberUntil( String source,
                                       out Int32 number,
                                       Boolean removeTrailingCharacter = true,
                                       params Char[] values )
        {
            return GetNumberUntil( source, out number, values.ToList(), removeTrailingCharacter );
        }

        /// <summary>
        ///     Gets a number from the beginning of the given string until one of the given values occurs.
        /// </summary>
        /// <exception cref="ParsRollException">Parsing failed.</exception>
        /// <param name="source">The source string.</param>
        /// <param name="number">The parsed number.</param>
        /// <param name="values">The values marking the end of the number.</param>
        /// <param name="removeTrailingCharacter">
        ///     A value indicating whether the last character after the number should ger removed
        ///     or not.
        /// </param>
        /// <returns>Returns the source string with number and value removed.</returns>
        private String GetNumberUntil( String source,
                                       out Int32 number,
                                       IEnumerable<Char> values,
                                       Boolean removeTrailingCharacter = true )
        {
            var valueList = values.ToList();
            var returnValue = source;
            var numberString = String.Empty;

            foreach ( var x in source.ToCharArray() )
                //Check if its a number
                if ( x.IsDigit() )
                {
                    numberString += x;
                    //Remove first character
                    returnValue = returnValue.Substring( 1 );
                }

                    //Check if its one of the given values
                else if ( x.IsIn( valueList ) )
                {
                    if ( removeTrailingCharacter )
                        returnValue = returnValue.Substring( 1 );
                    break;
                }

                else
                    throw new ParsRollException(
                        "Disallowed character '{0}' occurred before ending of number. (Expected: '{1}')".F( x,
                                                                                                            valueList.StringJoin(
                                                                                                                ", " ) ),
                        source );

            number = numberString.ToInt32();
            return returnValue;
        }

        /// <summary>
        ///     Gets the first number(starting at the beginning of the string, ending before the first none-digit character) of the
        ///     string.
        /// </summary>
        /// <exception cref="ParsRollException">Parsing failed.</exception>
        /// <param name="source">The source string.</param>
        /// <param name="number">The parsed number.</param>
        /// <returns>Returns the source string with number removed.</returns>
        private String GetFirstNumber( String source, out Int32 number )
        {
            var numberString = String.Empty;
            var remainingString = source;

            foreach ( var x in source.ToCharArray() )
            {
                if ( x.IsDigit() )
                    numberString += x;
                else
                    break;

                remainingString = remainingString.Substring( 1 );
            }

            try
            {
                number = numberString.ToInt32();
            }
            catch ( Exception ex )
            {
                throw new ParsRollException( "Failed to pars a number", source, ex );
            }
            return remainingString;
        }

        /// <summary>
        ///     Checks if the start of the given string is a number followed by any of the given values.
        /// </summary>
        /// <param name="source">The string to check.</param>
        /// <param name="acceptNothingAsValue">
        ///     A value indicating whether the parsing should accept nothing (string end) as a
        ///     allowed value.
        /// </param>
        /// <param name="values">The allowed values.</param>
        /// <returns>Returns true if the string matches the criterion; otherwise, false.</returns>
        private Boolean IsNumberFollowedBy( String source, Boolean acceptNothingAsValue, params Char[] values )
        {
            return IsNumberFollowedBy( source, acceptNothingAsValue, values.ToList() );
        }

        /// <summary>
        ///     Checks if the start of the given string is a number followed by any of the given values.
        /// </summary>
        /// <param name="source">The string to check.</param>
        /// <param name="acceptNothingAsValue">
        ///     A value indicating whether the parsing should accept nothing (string end) as a
        ///     allowed value.
        /// </param>
        /// <param name="values">The allowed values.</param>
        /// <returns>Returns true if the string matches the criterion; otherwise, false.</returns>
        private Boolean IsNumberFollowedBy( String source, Boolean acceptNothingAsValue, IEnumerable<Char> values )
        {
            var allDigits = false;

            foreach ( var x in source.ToCharArray() )
                if ( x.IsDigit() )
                    allDigits = true;
                else if ( x.IsIn( values ) )
                    return allDigits;
                else
                    return false;

            return allDigits && acceptNothingAsValue;
        }

        /// <summary>
        ///     Checks if the first character of the given string is any of the given values
        /// </summary>
        /// <param name="source">The string.</param>
        /// <param name="values">The values to check for.</param>
        /// <returns>Returns a value of true if the given strain starts with any of the given values.</returns>
        private Boolean FirstCharIs( String source, params Char[] values )
        {
            return FirstCharIs( source, values.ToList() );
        }

        /// <summary>
        ///     Checks if the first character of the given string is any of the given values
        /// </summary>
        /// <param name="source">The string.</param>
        /// <param name="values">The values to check for.</param>
        /// <returns>Returns a value of true if the given strain starts with any of the given values.</returns>
        private Boolean FirstCharIs( String source, IEnumerable<Char> values )
        {
            return source.First()
                         .IsIn( values );
        }

        #endregion
    }
}