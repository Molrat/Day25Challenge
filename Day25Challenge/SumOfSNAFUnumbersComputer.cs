namespace Day25Challenge
{
    public static class SumOfSNAFUnumbersComputer
    {
        public static string Compute(string inputAsTextWithOneNumberPerLine)
        {
            List<string> SNAFUnumbers = inputAsTextWithOneNumberPerLine.Split('\n').ToList();
            return Compute(SNAFUnumbers);
        }

        public static string Compute(List<string> snafuNumbers, bool pureSNAFUmath = true)
        {
            if (pureSNAFUmath)
            {
                PureSNAFU pureSNAFU = new("0");
                foreach (string SNAFU in snafuNumbers)
                {
                    pureSNAFU.AddOtherSNAFUnumber(SNAFU);
                }
                return pureSNAFU.ReturnAsString();
            }
            else
            {
                int sum = 0;
                foreach (string SNAFU in snafuNumbers)
                {
                    sum += SnafuDecimalConverser.SNAFUtoDecimal(SNAFU);
                }
                return SnafuDecimalConverser.DecimalToSNAFU(sum);
            }    
        }
    }
}
