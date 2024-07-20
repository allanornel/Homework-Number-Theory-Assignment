using System.Numerics;

namespace TeoriaDosNumerosTrab
{
    internal static class Test
    {
        internal static bool Validate(string numberString)
        {            
            return IsPalindrome(numberString) && IsDivisible(numberString) && HasCorrectDigitCount(numberString) && NoMoreThanTwoConsecutive(numberString);
        }

        static bool IsPalindrome(string numberString)
        {
            return numberString.SequenceEqual(numberString.Reverse());
        }

        static bool IsDivisible(string numberString)
        {
            BigInteger number = BigInteger.Parse(numberString);
            return number % 7 == 0 && number % 11 == 0 && number % 73 == 0;
        }

        static bool HasCorrectDigitCount(string numberString)
        {
            for (int i = 0; i <= 9; i++)
            {
                if (numberString.Count(c => c == (i + '0')) < 9)
                {
                    return false;
                }
            }
            return true;
        }

        static bool NoMoreThanTwoConsecutive(string numberString)
        {
            for (int i = 0; i < numberString.Length - 2; i++)
            {
                if (numberString[i] == numberString[i + 1] && numberString[i] == numberString[i + 2])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
