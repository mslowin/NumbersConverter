using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumbersConverter
{
    internal static class Converter
    {
        private static readonly List<string> _alphabet = new List<string>() { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", 
                                                                              "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", 
                                                                              "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", 
                                                                              "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "0", "1", "2", "3", 
                                                                              "4", "5", "6", "7", "8", "9", "+", "/" };

        private static readonly List<string> _asciiAlphabet = new List<string>() { " ", "!", "\"", "#", "$", "%", "&", "'", "(", ")", "*", "+", ",", "-", 
                                                                                   ".", "/", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", ":", ";", 
                                                                                   "<", "=", ">", "?", "@", "A", "B", "C", "D", "E", "F", "G", "H", "I", 
                                                                                   "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", 
                                                                                   "X", "Y", "Z", "[", "\\", "]", "^", "_", "`", "a", "b", "c", "d", "e", 
                                                                                   "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", 
                                                                                   "t", "u", "v", "w", "x", "y", "z", "{", "|", "}", "~"};

        public static string Convert(int input, string desiredType)
        {
            var output = "";
            switch (desiredType)
            {
                case "ASCII":
                    output = ConvertToASCII(input);
                    break;

                case "Binary":
                    output = ConvertToBinary(input.ToString());
                    break;

                case "Hexadecimal":
                    output = ConvertToHexadecimal(input);
                    break;

                case "Base64":
                    output = ConvertToBase64(input);
                    break;
            }
            return output;
        }

        public static string ConvertToASCII(int input)
        {
            var inputDec = DecConvert(input.ToString());
            var inputBin = inputDec.Select(c => { c = ConvertToBinary(c); return c; }).ToList();  // list of characters in binary form
            inputDec = inputBin.Select(c => { c = BinaryToDec(c); return c; }).ToList();  // converting each sample to its Decimal values
            string output = DecToASCII(inputDec);  // converting decimal values to ASCII

            return output;
        }

        private static string DecToASCII(List<string> inputDec)
        {
            bool run = true;
            string outputBase64 = "";
            int counter = 0;
            while (run)
            {
                var decNumber = int.Parse(inputDec[counter]);
                outputBase64 += _asciiAlphabet[decNumber];
                if (counter == inputDec.Count - 1)
                {
                    run = false;
                }
                counter++;
            }
            return outputBase64;
        }

        public static string ConvertToBinary(string input)
        {
            var binNum = new List<int>();
            var tmp = int.Parse(input);         //<-----

            while (true)
            {
                if (tmp < 1)
                {
                    binNum.Add(tmp % 2);
                    break;
                }
                else
                {
                    binNum.Add(tmp % 2);
                    tmp /= 2;
                }
            }
            var output = String.Join("", binNum.ToArray());
            char[] tmpArray = output.ToCharArray();
            Array.Reverse(tmpArray);
            output = new string(tmpArray);

            return output;
        }

        public static string ConvertToHexadecimal(int input)
        {
            var hexNum = new List<string>();
            var tmp = input;
            int hexTmp;

            while (tmp >= 1)
            {
                hexTmp = tmp % 16;
                hexNum.Add(HexConvert(hexTmp.ToString()));
                tmp /= 16;
            }
            var output = String.Join("", hexNum.ToArray());
            char[] tmpArray = output.ToCharArray();
            Array.Reverse(tmpArray);
            output = new string(tmpArray);

            return output;
        }

        public static string ConvertToBase64(int input)
        {
            var inputDec = DecConvert(input.ToString());
            var inputBin = inputDec.Select(c => { c = ConvertToBinary(c); return c; }).ToList();  // list of characters in binary form
            string combinedBinaries = String.Join("", inputBin.ToArray());  // string with all binar values
            List<string> bit6 = SplitTo6Bit(combinedBinaries);  // spliting combied binaries to 6 bit long samples
            inputDec = bit6.Select(c => { c = BinaryToDec(c); return c; }).ToList();  // converting each sample to its Decimal values
            string output = DecToBase64(inputDec);  // converting decimal values to base64

            return output;
        }

        private static string DecToBase64(List<string> inputDec)
        {
            bool run = true;
            string outputBase64 = "";
            int counter = 0;
            while(run)
            {
                var decNumber = int.Parse(inputDec[counter]);
                outputBase64 += _alphabet[decNumber];
                if(counter == inputDec.Count - 1)
                {
                    run = false;
                }
                counter++;
            }
            return outputBase64;
        }

        private static string BinaryToDec(string input)
        {
            char[] array = input.ToCharArray();
            Array.Reverse(array);

            int sum = 0;

            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == '1')
                {
                    sum += (int)Math.Pow(2, i);
                }

            }

            return sum.ToString();
        }

        private static List<string> SplitTo6Bit(string combinedBinaries)
        {
            List<string> output = new List<string>();
            var tmpStr = "";
            // We need to skip 0 index so it doesnt get stuck on 0 % 6 = 0
            for (int i = 1; i < combinedBinaries.Length + 1; i++)  // thats why we start from i = 1 and end on length + 1
            {
                tmpStr += combinedBinaries[i - 1];  // also that is why we take i - 1 here to get the actual char

                if (i % 6 == 0)
                {
                    output.Add(tmpStr);
                    tmpStr = "";
                }
            }
            return output;
        }

        private static List<string> DecConvert(string input)
        {
            char[] inputDec = input.ToString().ToCharArray();
            var outputDec = new List<string>();
            foreach (var character in inputDec)
            {
                outputDec.Add(((int)character).ToString());
            }

            return outputDec;
        }

        private static string HexConvert(string hexTmp)
        {
            if(hexTmp == "10")
            {
                hexTmp = "A";
            }
            if (hexTmp == "11")
            {
                hexTmp = "B";
            }
            if (hexTmp == "12")
            {
                hexTmp = "C";
            }
            if (hexTmp == "13")
            {
                hexTmp = "D";
            }
            if (hexTmp == "14")
            {
                hexTmp = "E";
            }
            if (hexTmp == "15")
            {
                hexTmp = "F";
            }
            return hexTmp;
        }
    }
}
