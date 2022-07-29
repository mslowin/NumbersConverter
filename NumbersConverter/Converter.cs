using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumbersConverter
{
    internal static class Converter
    {
        public static string Convert(int input, string desiredType)
        {
            var output = "";
            switch (desiredType)
            {
                case "ASCII":
                    output = ConvertToASCII(input);
                    break;

                case "Binary":
                    var outputBin = ConvertToBinary(input);
                    output = String.Join("", outputBin.ToArray());
                    char[] tmpArray = output.ToCharArray();
                    Array.Reverse(tmpArray);
                    output = new string(tmpArray);
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
            return input.ToString();
        }

        public static List<int> ConvertToBinary(int input)
        {
            var binNum = new List<int>();
            var tmp = input;

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

            return binNum;
        }

        public static string ConvertToHexadecimal(int input)
        {
            throw new NotImplementedException();
        }

        public static string ConvertToBase64(int input)
        {
            throw new NotImplementedException();
        }
    }
}
