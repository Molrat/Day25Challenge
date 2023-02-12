using System.Runtime.Intrinsics.X86;

namespace Day25Challenge
{
    public class SnafuDecimalConverser
    {
        public static int SNAFUtoDecimal(string Number)
        {
            int result = 0;
            for (int i = 0; i < Number.Length; i++)
            {
                int weight = Convert.ToInt32(Math.Pow(5, i));
                char character = Number[^(i + 1)];
                if (character == '-')
                {
                    result -= weight;
                }
                else if (character == '=')
                {
                    result -= 2 * weight;
                }
                else
                {
                    result += weight * (character - '0');
                }
            }
            return result;
        }

        public static string DecimalToSNAFU(int dec)
        {
            // The problem can be converted to a regular conversion from decimal to pental base.
            // The difference is that the leftmost symbol can only have two values, namely '1' or '2'. 
            // Given the leftmost SNAFU symbol (x) and the number of symbols in a SNAFU number (say n), the possible decimal number computed from this number
            // range from x * 5 ^ (n  - 1) +- FLOOR(5 ^ (n - 1) / 2). We use this to compute the leftmost SNAFU symbol and the number of characters.
            // Then we substract from the decimal number the minimum value of the corresponding range for this starting number.
            // The remainder can be simply converted to a pental base number. The resulting number can be mapped from the symbols '0', '1', '2', '3', '4' to '=', '-', '0', '1', '2' respectively.
            // We add this to the lefmost SNAFU symbol, and add '=' symbols in between so that the result has the right number of snafu symbols.
            if (dec == 0) return "0";
            string result = "";

            // Leftmost character is either 1 or 2. First, determine this number and the number of characters in the SNAFU number:
            int numberOfSnafuCharacters = Convert.ToInt32(Math.Floor(Math.Log(dec) / Math.Log(5))); // = floor(5log(dec))
            int power = Convert.ToInt32(Math.Pow(5, numberOfSnafuCharacters));
            int halfPower = power / 2;
            //3 possibilities:
            // 1. numberOfSnafuCharacters is correct and leftmost character is 1,
            // 2. numberOfSnafuCharacters is correct and leftmost character is 2, or 
            // 3. numberOfSnafuCharacters is one higher and leftmost character is 1.
            if (dec <= power + halfPower)
            {
                result += '1';
                dec -= power - halfPower;
            }
            else if(dec <= 2 * power + halfPower)
            {
                result += '2';
                dec -= 2 * power - halfPower;
            }
            else
            {
                result += '1';
                numberOfSnafuCharacters += 1;
                power = Convert.ToInt32(Math.Pow(5, numberOfSnafuCharacters));
                halfPower = power / 2;
                dec -= power - halfPower;
            }

            // The remainder decimal number as computed can be converted to a Pental base number with symbols '=', '-', '0', '1', '2', instead of the conventional 1, 2, 3, 4, 5.
            string pentalOfRemain = DecToPent(dec);
            string SNAFUofRemainingDecimal = ConvertConventionalPentalSymbolsToSNAFUsymbols(pentalOfRemain);
            // Add missing "=" symbols between the first SNAFU symbol and the SNAFU computed from the remaining decimal number, to match the total number of symbols.
            for (int i = 0; i < numberOfSnafuCharacters - SNAFUofRemainingDecimal.Length;i++)
            {
                result += '=';
            }
            // Add the SNAFU of the remaining .
            result += SNAFUofRemainingDecimal;
            return result;
        }

        private static string DecToPent(int number)
        {
            int index = 0;
            string result = "";
            while (number > 0)
            {
                result = Convert.ToString(number % 5) + result;
                index++;
                number /= 5;
            }
            return result;
        }

        private static string ConvertConventionalPentalSymbolsToSNAFUsymbols(string pentalNumber)
        {
            string result = "";
            foreach(char c in pentalNumber)
            {
                switch(c)
                {
                    case '0':
                        result += '=';
                        break;
                    case '1':
                        result += '-';
                        break;
                    case '2':
                        result += '0';
                        break;
                    case '3':
                        result += '1';
                        break;
                    case '4':
                        result += '2';
                        break;
                    default:
                        break;
                }
            }
            return result;
        }
    }
}
