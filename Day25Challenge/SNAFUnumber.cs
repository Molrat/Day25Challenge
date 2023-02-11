namespace Day25Challenge
{
    public class SNAFUnumber
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
            // The problem can be converted to a regular conversion from decimal to pental.
            string result = "";

            // Leftmost character is either 1 or 2. First, determine this number and the number of characters:
            double x = Math.Log(dec) / Math.Log(5);
            int largestPowerOfFive = Convert.ToInt32(Math.Floor(Math.Log(dec) / Math.Log(5)));
            int power = Convert.ToInt32(Math.Pow(5, largestPowerOfFive));
            int halfPower = power / 2;

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
                largestPowerOfFive += 1;
                power = Convert.ToInt32(Math.Pow(5, largestPowerOfFive));
                halfPower = power / 2;
                dec -= power - halfPower;
            }

            // The remainder as computed can be converted to a Pental base number with symbols '=', '-', '0', '1', '2', instead of the conventional 1, 2, 3, 4, 5.
            string pentalOfRemain = DecToPent(dec);
            string SNAFUofRemain = ConvertConventionalPentalSymbolsToSNAFUsymbols(pentalOfRemain);
            // Add missing "=" symbols between SNAFU of the remain and the first symbol to match the total number of symbols.
            for (int i = 0; i < largestPowerOfFive - SNAFUofRemain.Length;i++)
            {
                result += '=';
            }
            // Addd the SNAFU of the remain.
            result += SNAFUofRemain;
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
