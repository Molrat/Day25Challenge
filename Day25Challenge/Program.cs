﻿using Day25Challenge;

string input = "1=-0-2\n12111\n2=0=\n21\n2=01\n111\n20012\n112\n1=-1=\n1-12\n12\n1=\n122";
string SNAFUsum = SumOfSNAFUnumbersComputer.Compute(input);
Console.WriteLine(SNAFUsum);