using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

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
        public static string ConvertFromASCII(string input, string desiredType)
        {
            var output = "";
            switch (desiredType)
            {
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

        internal static string ConvertToASCII(string input, string desiredType)
        {
            var output = "";
            switch (desiredType)
            {
                case "Binary":
                    output = ConvertFromBinary(input.ToString());
                    break;

                case "Hexadecimal":
                    output = ConvertFromHexadecimal(input.ToString());
                    break;

                case "Base64":
                    output = ConvertFromBase64(input.ToString());
                    break;
            }
            return output;
        }

        /// <summary>
        /// Converts binary values to decimals
        /// </summary>
        /// <param name="input">Binary values to be converted (string with one or
        /// more binary numbers with spaces between them)</param>
        /// <returns>Decimal representation of those values</returns>
        private static string ConvertFromBinary(string input)
        {
            if (!IsBinary(input)) { return "Number isn't binary"; }

            var binNumbers = input.Split(' ');
            double output;
            var outputList = new List<string>();

            foreach (var binNumber in binNumbers)
            {
                output = 0;
                char[] tmpArray = binNumber.ToCharArray();
                Array.Reverse(tmpArray);
                input = new string(tmpArray);

                for (int i = 0, power = 1; i < input.Length; i++, power *= 2)
                {
                    output += int.Parse(input[i].ToString()) * power;
                }
                outputList.Add(output.ToString());
            }
            return String.Join(" ", outputList.ToArray());
        }

        /// <summary>
        /// Checks if a number is binary.
        /// </summary>
        /// <param name="input">Number to be checked.</param>
        /// <returns>true if is binary, otherwise false.</returns>
        private static bool IsBinary(string input)
        {
            foreach (var character in input)
            {
                if (character.ToString() != "0" && character.ToString() != "1" && character.ToString() != " ") { return false; }
            }
            return true;
        }

        /// <summary>
        /// Converts hexadecimal values to decimals
        /// </summary>
        /// <param name="input">Hexadecimal values to be converted (string with one or
        /// more hex numbers with spaces between them)</param>
        /// <returns>Decimal representation of those values</returns>
        private static string ConvertFromHexadecimal(string input)
        {
            if (!IsHexadecimal(input)) { return "Number isn't hexadecimal"; }

            var HexNumbers = input.Split(' ');
            double output;
            var outputList = new List<string>();

            foreach (var HexNumber in HexNumbers)
            {
                output = 0;
                char[] tmpArray = HexNumber.ToCharArray();
                Array.Reverse(tmpArray);
                input = new string(tmpArray);

                for (int i = 0; i < input.Length; i++)
                {
                    var character = 0;
                    if (!int.TryParse(input[i].ToString(), out var intCharacter))
                    {
                        character = int.Parse(HexToDecHelper(input[i].ToString()));  // character = 12 || 13 itd.
                    }
                    else
                    {
                        character = intCharacter;
                    }
                    output += character * Math.Pow(16, i);
                }
                outputList.Add(output.ToString());
            }

            //string outputASCII = DecToASCII(String.Join(" ", outputList.ToArray()));

            return String.Join(" ", outputList.ToArray());
        }

        /// <summary>
        /// Checks if a number is hexadecimal.
        /// </summary>
        /// <param name="input">Number to be checked.</param>
        /// <returns>true if is hexadecimal, otherwise false.</returns>
        private static bool IsHexadecimal(string input)
        {
            foreach (var character in input)
            {
                if (!Regex.IsMatch(character.ToString(), @"[0-9A-F ]+"))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Converts input string from base64 to ASCII.
        /// </summary>
        /// <param name="input">input string (e.g. QXR0YWNrIGF0IGRhd24h).</param>
        /// <returns>ASCII representation of the base64 input.</returns>
        private static string ConvertFromBase64(string input)
        {
            if (!IsBase64(input)) { return "Number isn't base64"; }
            string decNumbers = Base64ToDec(input.ToList());  // Converting base64 values to decimal values
            string[] bit6Numbers = DecToBit6(decNumbers);  // Converting decimal values to 6 bit long binary values
            string[] bit8Numbers = Bit6ToBit8(bit6Numbers);  // Putting 6bits together and creating 8bits out of them
            string DecNumbers = ConvertFromBinary(String.Join(" ", bit8Numbers.ToArray()));  // Converting 8bits numbers to decimal values
            string outputASCII = DecToASCII(DecNumbers);  // Converting decimals to ASCII

            return outputASCII;
        }

        private static bool IsBase64(string input)
        {
            foreach (var character in input)
            {
                if (!Regex.IsMatch(character.ToString(), @"[A-Za-z0-9+/= ]"))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Converts decimal values (string with spaces between values) to ASCII.
        /// </summary>
        /// <param name="input">input string.</param>
        /// <returns>ASCII representation of the input string.</returns>
        private static string DecToASCII(string input)
        {
            var decNumbers = input.Split(' ');
            string outputASCII = "";

            for (int i = 0; i < decNumbers.Length; i++)
            {
                var decNumber = decNumbers[i];

                if (int.Parse(decNumber) < 32 || int.Parse(decNumber) > 126)  // if decimal is out of this range it cannot be converted to ASCII
                {
                    return "Can't convert to ASCII, try decimal.";
                }
                outputASCII += _asciiAlphabet[int.Parse(decNumber) - 32];  // 32 because my alphabet doesn't start from the beginning of ASCII
            }

            return outputASCII;
        }

        /// <summary>
        /// Converts 6 bit long binaries to 8 bit long ones.
        /// </summary>
        /// <param name="bit6Numbers">String array of 6 bit long binaries.</param>
        /// <returns>String array of 8 bits</returns>
        private static string[] Bit6ToBit8(string[] bit6Numbers)
        {
            var combinedBinaries = String.Join("", bit6Numbers.ToArray());
            List<string> output8bits = new List<string>();
            var bit8Number = "";

            for (int i = 1; i < combinedBinaries.Length + 1; i++)
            {
                bit8Number += combinedBinaries[i - 1];
                if (i % 8 == 0)
                {
                    output8bits.Add(bit8Number);
                    bit8Number = "";
                }
            }

            return output8bits.ToArray();
        }

        /// <summary>
        /// Converts decimal values ( <= 255) to 6 bit long binaries.
        /// </summary>
        /// <param name="input">input string (with spaces between values).</param>
        /// <returns>String array of 6 bits.</returns>
        private static string[] DecToBit6(string input)
        {
            var decNumbers = input.Split(' ');
            decNumbers = decNumbers.Reverse().Skip(1).Reverse().ToArray();  // getting rid of last index whitch is empty
            var outputList = new List<string>();

            foreach (var decNumber in decNumbers)
            {
                outputList.Add(SingleItemToBinary(decNumber).Remove(0,2));  // here a bin number must have 6 digits - so we remove 2 first
            }

            return outputList.ToArray();
        }

        /// <summary>
        /// Converts base64 value to decimal value.
        /// </summary>
        /// <param name="inputBase64">base64 value.</param>
        /// <returns>decimal value.</returns>
        private static string Base64ToDec(List<char> inputBase64)
        {
            bool run = true;
            string outputDec = "";
            int counter = 0;
            
            for (int i = 0; i < inputBase64.Count; i++)
            {
                var base64Number = inputBase64[i];

                for (int j = 0; j < _alphabet.Count; j++)
                {
                    if (base64Number.ToString() == _alphabet[j])
                    {
                        outputDec += j.ToString() + " ";
                    }
                }
            }

            return outputDec;
        }

        ///// <summary>
        ///// Converts input string into ASCII form
        ///// </summary>
        ///// <param name="input">Input string.</param>
        ///// <returns>ASCII value.</returns>
        //public static string ConvertToASCII(string input)
        //{
        //    var inputDec = DecConvert(input.ToString());
        //    var inputBin = inputDec.Select(c => { c = ConvertToBinary(c); return c; }).ToList();  // list of characters in binary form
        //    inputDec = inputBin.Select(c => { c = BinaryToDec(c); return c; }).ToList();  // converting each sample to its Decimal values
        //    string output = DecToASCII(inputDec);  // converting decimal values to ASCII

        //    return output;
        //}

        ///// <summary>
        ///// Converts decimal value into ASCII.
        ///// </summary>
        ///// <param name="inputDec">Decimal value.</param>
        ///// <returns>ASCII value.</returns>
        //private static string DecToASCII(List<string> inputDec)
        //{
        //    bool run = true;
        //    string outputBase64 = "";
        //    int counter = 0;
        //    while (run)
        //    {
        //        var decNumber = int.Parse(inputDec[counter]);
        //        outputBase64 += _asciiAlphabet[decNumber];
        //        if (counter == inputDec.Count - 1)
        //        {
        //            run = false;
        //        }
        //        counter++;
        //    }
        //    return outputBase64;
        //}

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

                foreach (var decimalItem in inputInDecimals)
                {
                    outputList.Add(SingleItemToBinary(decimalItem));
                }

                return String.Join(" ", outputList.ToArray());
            }
            else
            {
                return SingleItemToBinary(input);
            }
        }

        /// <summary>
        /// Converts single item (peace of text or a number) to its binary counterpart.
        /// </summary>
        /// <param name="input">Item to convert.</param>
        /// <returns>Converted item.</returns>
        private static string SingleItemToBinary(string input)
        {
            //if (input == "") { return ""; }
            string output;
            var binNum = new List<int>();
            var tmp = int.Parse(input);

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

            return output;
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

                foreach (var decimalItem in inputInDecimals)
                {
                    outputList.Add(SingleItemToHex(decimalItem));
                }
                return String.Join(" ", outputList.ToArray());
            }
            else
            {
                return SingleItemToHex(input);
            }
        }

        /// <summary>
        /// Converts single item (peace of text or a number) to its hex counterpart.
        /// </summary>
        /// <param name="input">Item to convert.</param>
        /// <returns>Converted item.</returns>
        private static string SingleItemToHex(string input)
        {
            string output;
            var hexNum = new List<string>();
            var tmp = int.Parse(input);
            int hexTmp;

            while (tmp >= 1)
            {
                hexTmp = tmp % 16;
                hexNum.Add(DecToHexHelper(hexTmp.ToString()));
                tmp /= 16;
            }
            output = String.Join("", hexNum.ToArray());
            char[] tmpArray = output.ToCharArray();
            Array.Reverse(tmpArray);
            output = new string(tmpArray);

            return output;
        }

        /// <summary>
        /// Converts input string into base64 value.
        /// </summary>
        /// <param name="input">Input string.</param>
        /// <returns>Base64 value.</returns>
        public static string ConvertToBase64(string input)
        {
            if (input.Length == 0) { return ""; }  // if input is empty there is nothing to convert
            var inputDec = DecConvert(input.ToString());  // getting a list of characters (intigers) from input
            var inputBin = inputDec.Select(c => { c = ConvertToBinary(c); return c; }).ToList();  // list of characters in binary form
            string combinedBinaries = String.Join("", inputBin.ToArray());  // string with all binar values
            combinedBinaries = AddZerosIfNecessary(combinedBinaries);
            List<string> bit6 = SplitTo6Bit(combinedBinaries);  // spliting combied binaries to 6 bit long samples
            inputDec = bit6.Select(c => { c = SingleBinaryToDec(c); return c; }).ToList();  // converting each sample to its Decimal values
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
        private static string SingleBinaryToDec(string input)
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
        private static string DecToHexHelper(string hexTmp)
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

        /// <summary>
        /// Converts hexadecimal value to decimal value.
        /// </summary>
        /// <param name="decTmp">String to be converted.</param>
        /// <returns>Converted value.</returns>
        private static string HexToDecHelper(string decTmp)
        {
            return _ = decTmp switch
            {
                "A" => "10",
                "B" => "11",
                "C" => "12",
                "D" => "13",
                "E" => "14",
                "F" => "15",
                _ => decTmp,
            };
        }
    }
}