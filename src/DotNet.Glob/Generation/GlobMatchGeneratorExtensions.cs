using System;
using System.Linq;
using System.Text;

namespace DotNet.Globbing.Generation
{
    public static class GlobMatchGeneratorExtensions
    {


        public static void AppendRandomLiteralCharacterBetween(this StringBuilder builder, Random random, char start, char end)
        {
            builder.Append(random.GetRandomCharacterBetween(start, end));
        }
        public static void AppendRandomLiteralCharacterNotBetween(this StringBuilder builder, Random random, char start, char end)
        {
            builder.Append(random.GetRandomLiteralCharacterNotBetween(start, end));
        }

        public static void AppendRandomNumberCharacterNotBetween(this StringBuilder builder, Random random, char start, char end)
        {
            builder.Append(random.GetRandomNumberCharacterNotBetween(start, end));
        }
        //public static void AppendRandomNumberCharacterBetween(this StringBuilder builder, Random random, char start, char end)
        //{
        //    builder.Append(random.GetRandomNumberCharacterBetween(start, end));
        //}


        public static void AppendRandomLetterCharacter(this StringBuilder builder, Random random)
        {
            var typeOfCharacterToAppend = random.Next(0, 1);
            switch (typeOfCharacterToAppend)
            {
                case 0:
                    builder.Append(random.GetRandomCharacterBetween('a', 'z'));
                    break;
                case 1:
                    builder.Append(random.GetRandomCharacterBetween('A', 'Z'));
                    break;
                default:
                    throw new InvalidOperationException();
                    // break;
            }
        }

        public static void AppendRandomNumberCharacter(this StringBuilder builder, Random random)
        {
            builder.Append(random.GetRandomCharacterBetween('0', '9'));
        }

        public static void AppendRandomLiteralCharacter(this StringBuilder builder, Random random)
        {
            var typeOfCharacterToAppend = random.Next(0, 3);
            switch (typeOfCharacterToAppend)
            {
                case 0:
                    builder.Append(random.GetRandomCharacterBetween('a', 'z'));
                    break;
                case 1:
                    builder.Append(random.GetRandomCharacterBetween('A', 'Z'));
                    break;
                case 2:
                    builder.Append(random.GetRandomCharacterBetween('0', '9'));
                    break;
                case 3:
                    builder.Append(random.GetRandomCharacterBetween('-', '.'));
                    break;
                default:
                    throw new InvalidOperationException();
                    // break;
            }
        }

        public static void AppendRandomLiteralString(this StringBuilder builder, Random random, int maxLength = 10)
        {
            // append 0 or many, random literal characters
            var numberToAppend = random.Next(0, maxLength);
            if (numberToAppend > 0)
            {
                for (int i = 1; i <= numberToAppend; i++)
                {
                    builder.AppendRandomLiteralCharacter(random);
                }
            }
        }

        public static char GetRandomCharacterBetween(this Random random, char start, char end)
        {
            return (char)random.Next((int)start, (int)end);
        }



        public static char GetRandomLiteralCharacterNotBetween(this Random random, char start, char end)
        {


            bool canGenerateHigher = !((int)end >= 'z');
            bool canGenerateLower = !((int)start <= '0');

            if (canGenerateHigher && canGenerateLower)
            {
                var generateHigher = random.NextDouble() > 0.5;
                if (generateHigher)
                {
                    return (char)random.Next(end >= 'a' ? end + 1 : 'a', 'z');
                }
                else
                {
                    return (char)random.Next('0', start <= '9' ? start - 1 : '9');
                }
            }

            if (canGenerateHigher)
            {
                return (char)random.Next(end >= 'a' ? end + 1 : 'a', 'z');
            }

            if (canGenerateLower)
            {
                return (char)random.Next('0', start <= '9' ? start - 1 : '9');
            }

            if (start == end)
            {
                if (GlobStringReader.AllowedNonAlphaNumericChars.Contains(start))
                {
                    var roll = random.NextDouble();
                    var generateAlphaNumeric = roll >= 0.33;
                    if (generateAlphaNumeric)
                    {
                        var nextCharAttempt = start;
                        while (nextCharAttempt == start)
                        {
                            nextCharAttempt = GlobStringReader.AllowedNonAlphaNumericChars[(char)random.Next(0, GlobStringReader.AllowedNonAlphaNumericChars.Length - 1)];
                        }
                        return nextCharAttempt;
                    }

                    // generate a random letter.
                    if (roll >= .66)
                    {
                        return (char)random.Next(end >= 'a' ? end + 1 : 'a', 'z');
                    }

                    // otherwise generate a random number.
                    return (char)random.Next('0', start <= '9' ? start - 1 : '9');

                }
            
            }

            throw new ArgumentOutOfRangeException("Could not generate character outside range..");

        }

        public static char GetRandomLetterCharacterNotBetween(this Random random, char start, char end)
        {

            var generateHigher = random.NextDouble() > 0.5;
            if ((int)end >= 'z')
            {
                generateHigher = false; // end char is already z or greater so attempt to generate lower.
            }
            if (generateHigher)
            {
                // generating a random letter thats higher than the end char.
                return (char)random.Next(end >= 'a' ? end + 1 : 'a', 'z');
            }
            if ((int)start <= 'A')
            {
                throw new NotImplementedException("Could not generate a random letter character that is outside the specified range: (start - end) = " + start + " - " + end);
            }
            // generating a random char thats lower than the start char.
            return (char)random.Next('A', start <= 'Z' ? start - 1 : 'Z');

        }

        public static char GetRandomNumberCharacterNotBetween(this Random random, char start, char end)
        {

            var generateHigher = random.NextDouble() > 0.5;
            if ((int)end >= '9')
            {
                generateHigher = false; // end char is already z or greater so attempt to generate lower.
            }
            if (generateHigher)
            {
                // generating a random letter thats higher than the end char.
                return (char)random.Next(end >= '0' ? end + 1 : '0', '9');
            }
            if ((int)start <= '0')
            {
                throw new NotImplementedException("Could not generate a random letter character that is outside the specified range: (start - end) = " + start + " - " + end);
            }
            // generating a random char thats lower than the start char.
            return (char)random.Next('0', start <= '9' ? start - 1 : '9');

        }


    }
}