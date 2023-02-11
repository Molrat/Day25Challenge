using Day25Challenge;

namespace UnitTestingSNAFU
{
    public class UnitTestingSNAFUconversions
    {
        [Fact]
        public void TestSNAFUtoDecimal()
        {
            // Arrange
            string SNAFU1 = "21=-2";
            string SNAFU2 = "1=====";
            string SNAFU3 = "2-2-2-";
            string SNAFU4 = "122-=-";
            string SNAFU5 = "1";
            string SNAFU6 = "2";
            // Act
            int decimal1 = SNAFUnumber.SNAFUtoDecimal(SNAFU1);
            int decimal2 = SNAFUnumber.SNAFUtoDecimal(SNAFU2);
            int decimal3 = SNAFUnumber.SNAFUtoDecimal(SNAFU3);
            int decimal4 = SNAFUnumber.SNAFUtoDecimal(SNAFU4);
            int decimal5 = SNAFUnumber.SNAFUtoDecimal(SNAFU5);
            int decimal6 = SNAFUnumber.SNAFUtoDecimal(SNAFU6);

            // Assert
            Assert.Equal(1322, decimal1);
            Assert.Equal(1563, decimal2);
            Assert.Equal(5859, decimal3);
            Assert.Equal(4589, decimal4);
            Assert.Equal(1, decimal5);
            Assert.Equal(2, decimal6);
        }

        [Fact]
        public void TestDecimalToSNAFU()
        {
            // Arrange
            Random rand = new();
            List<int> randomNumbers = new();
            for( int i =0; i < 100; i++)
            {
                randomNumbers.Add(rand.Next(10000));
            }
            // Act
            List<int> convertedToSNAFUandBack = new();
            foreach(int number in randomNumbers)
            {
                string SNAFU = SNAFUnumber.DecimalToSNAFU(number);
                int numberConvertedBack = SNAFUnumber.SNAFUtoDecimal(SNAFU);
                convertedToSNAFUandBack.Add(numberConvertedBack);
            }
            // Assert
            for(int i = 0; i < randomNumbers.Count; i++)
            {
                Assert.Equal(randomNumbers[i], convertedToSNAFUandBack[i]);
            }
        }
    }
}