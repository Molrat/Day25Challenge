namespace Day25Challenge
{
    public static class SumOfSNAFUnumbersComputer
    {
        public static string Compute(string inputAsTextWithOneNumberPerLine)
        {
            List<string> SNAFUnumbers = inputAsTextWithOneNumberPerLine.Split('\n').ToList();
            return Compute(SNAFUnumbers);
        }

        public static string Compute(List<string> snafuNumbers)
        {
            int sum = 0;
            foreach (string SNAFU in snafuNumbers)
            {
                sum += SNAFUnumber.SNAFUtoDecimal(SNAFU);
            }
            return SNAFUnumber.DecimalToSNAFU(sum);
        }
    }
}
