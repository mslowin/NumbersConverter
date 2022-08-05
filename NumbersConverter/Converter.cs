using System;
using System.Collections.Generic;
using System.Linq;

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

        /// <summary>
        /// Performs conversion of the input depending on the selected conversion method
        /// </summary>
        /// <param name="input">Input value to be converted.</param>
        /// <param name="desiredType">Data type to convert to.</param>
        /// <returns>Converted value.</returns>
        public static string Convert(string input, string desiredType)
        {
            var output = "";
            switch (desiredType)
            {
                case "ASCII":
                    output = ConvertToASCII(input.ToString());
                    break;

                case "Binary":
                    output = ConvertToBinary(input.ToString());
                    break;

                case "Hexadecimal":
                    output = ConvertToHexadecimal(input.ToString());
                    break;

                case "Base64":
                    output = ConvertToBase64(input.ToString());
                    break;
            }
            return output;
        }

        /// <summary>
        /// Converts input string into ASCII form
        /// </summary>
        /// <param name="input">Input string.</param>
        /// <returns>ASCII value.</returns>
        public static string ConvertToASCII(string input)
        {
            var inputDec = DecConvert(input.ToString());
            var inputBin = inputDec.Select(c => { c = ConvertToBinary(c); return c; }).ToList();  // list of characters in binary form
            inputDec = inputBin.Select(c => { c = BinaryToDec(c); return c; }).ToList();  // converting each sample to its Decimal values
            string output = DecToASCII(inputDec);  // converting decimal values to ASCII

            return output;
        }

        /// <summary>
        /// Converts decimal value into ASCII.
        /// </summary>
        /// <param name="inputDec">Decimal value.</param>
        /// <returns>ASCII value.</returns>
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

        /// <summary>
        /// Converts input string into binary form.
        /// </summary>
        /// <param name="input">Input string.</param>
        /// <returns>Binary value.</returns>
        public static string ConvertToBinary(string input)
        {
            if (!int.TryParse(input, out var inputInt))  // if input is a text string
            {
                var outputList = new List<string>();
                var inputInDecimals = DecConvert(input);
                string output;

                foreach (var decimalItem in inputInDecimals)
                {
                    var binNum = new List<int>();
                    var tmp = int.Parse(decimalItem);         //<-----

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
                    output = String.Join("", binNum.ToArray());

                    char[] tmpArray = output.ToCharArray();
                    Array.Reverse(tmpArray);
                    output = new string(tmpArray);

                    while (output.Length % 8 != 0)
                    {
                        output = "0" + output;
                    }

                    outputList.Add(output);
                }

                output = String.Join(" ", outputList.ToArray());

                return output;
            }
            else
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

                while (output.Length % 8 != 0)
                {
                    output = "0" + output;
                }

                return output;
            }
        }

        /// <summary>
        /// Converts input string into hexadecimal form.
        /// </summary>
        /// <param name="input">Input string.</param>
        /// <returns>Hexadecimal value.</returns>
        public static string ConvertToHexadecimal(string input)
        {
            if (!int.TryParse(input, out var inputInt))  // if input is a text string
            {
                var outputList = new List<string>();
                var inputInDecimals = DecConvert(input);
                string output;

                foreach (var decimalItem in inputInDecimals)
                {
                    var hexNum = new List<string>();
                    var tmp = int.Parse(decimalItem);
                    int hexTmp;

                    while (tmp >= 1)
                    {
                        hexTmp = tmp % 16;
                        hexNum.Add(HexConvert(hexTmp.ToString()));
                        tmp /= 16;
                    }
                    output = String.Join("", hexNum.ToArray());

                    char[] tmpArray = output.ToCharArray();
                    Array.Reverse(tmpArray);
                    output = new string(tmpArray);

                    outputList.Add(output);
                }

                output = String.Join(" ", outputList.ToArray());

                return output;
            }
            else
            {
                var hexNum = new List<string>();
                var tmp = int.Parse(input);
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
        }

        /// <summary>
        /// Converts input string into base64 value.
        /// </summary>
        /// <param name="input">Input string.</param>
        /// <returns>Base64 value.</returns>
        public static string ConvertToBase64(string input)
        {
            var inputDec = DecConvert(input.ToString());  // getting a list of characters (intigers) from input
            var inputBin = inputDec.Select(c => { c = ConvertToBinary(c); return c; }).ToList();  // list of characters in binary form
            string combinedBinaries = String.Join("", inputBin.ToArray());  // string with all binar values
            combinedBinaries = AddZerosIfNecessary(combinedBinaries);
            List<string> bit6 = SplitTo6Bit(combinedBinaries);  // spliting combied binaries to 6 bit long samples
            inputDec = bit6.Select(c => { c = BinaryToDec(c); return c; }).ToList();  // converting each sample to its Decimal values
            string output = DecToBase64(inputDec);  // converting decimal values to base64

            while (output.Length % 4 != 0)  // output must be divisible by 4
            {
                output += "=";
            }

            return output;
        }

        /// <summary>
        /// Adds zeros at the end of a string till it is divisible by 6.
        /// </summary>
        /// <param name="combinedBinaries">Input string.</param>
        /// <returns>String with added zeros at the end.</returns>
        private static string AddZerosIfNecessary(string combinedBinaries)
        {
            while (combinedBinaries.Length % 6 != 0)
            {
                combinedBinaries += "0";
            }
            return combinedBinaries;
        }

        /// <summary>
        /// Converts decimal value to base64 value.
        /// </summary>
        /// <param name="inputDec">Decimal value.</param>
        /// <returns>Base64 value.</returns>
        private static string DecToBase64(List<string> inputDec)
        {
            bool run = true;
            string outputBase64 = "";
            int counter = 0;
            while (run)
            {
                var decNumber = int.Parse(inputDec[counter]);
                outputBase64 += _alphabet[decNumber];
                if (counter == inputDec.Count - 1)
                {
                    run = false;
                }
                counter++;
            }
            return outputBase64;
        }

        /// <summary>
        /// Converts binary value to decimal value.
        /// </summary>
        /// <param name="input">Binary string.</param>
        /// <returns>Decimal string.</returns>
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

        /// <summary>
        /// Splits string into 6 character long samples divided with a space.
        /// </summary>
        /// <param name="combinedBinaries">long string (characters count should be divisible by 6)</param>
        /// <returns>Sampled string.</returns>
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

        /// <summary>
        /// Creates a list of INTIGER characters from a string.
        /// </summary>
        /// <param name="input">string which will be converted to a list.</param>
        /// <returns>List of INTIGER characters.</returns>
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

        /// <summary>
        /// Converts decimal value to hexadecimal value.
        /// </summary>
        /// <param name="hexTmp">String to be converted.</param>
        /// <returns>Converted value.</returns>
        private static string HexConvert(string hexTmp)
        {
            return _ = hexTmp switch
            {
                "10" => "A",
                "11" => "B",
                "12" => "C",
                "13" => "D",
                "14" => "E",
                "15" => "F",
                _ => hexTmp,
            };
        }
    }
}