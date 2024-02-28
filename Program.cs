using System.Text;

namespace NumToWords
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter a  number: ");
            if (int.TryParse(Console.ReadLine(), out int input))
            {
                INumberConverter converter = input < 0 ? new NegativeNumberConverter() : new PositiveNumberConverter();
                Console.WriteLine(converter.ConvertNumberToWords(input));
            }
            else
            {
                Console.WriteLine("Wrong Input. Press Any Key to Exit..");
            }
            Console.ReadKey();
        }
    }
    public class NegativeNumberConverter : NumberConverterBase, INumberConverter
    {
        public string ConvertNumberToWords(int input)
        {
            int number = -input;
            return "minus " + NumberToWords(number);
        }
    }

    public class PositiveNumberConverter : NumberConverterBase, INumberConverter
    {
        public string ConvertNumberToWords(int input)
        {
            return NumberToWords(input);
        }

    }

    public class NumberConverterBase
    {
        public static readonly string[] unitsMap = { "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
        public static readonly string[] tensMap = { "Zero", "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

        public string NumberToWords(int number)
        {
            if (number == 0)
                return "Zero";

            StringBuilder text = new StringBuilder();

            int[] values = { 1000000, 1000, 100 };
            string[] words = { " million ", " thousand ", " hundred " };

            for (int i = 0; i < values.Length; i++)
            {
                if ((number / values[i]) > 0)
                {
                    text.Append(NumberToWords(number / values[i]) + words[i]);
                    number %= values[i];
                }
            }

            if (number > 0)
            {
                if (number < 20)
                    text.Append(unitsMap[number]);
                else
                {
                    text.Append(tensMap[number / 10]);
                    if ((number % 10) > 0)
                        text.Append(" " + unitsMap[number % 10]);
                }
            }

            return text.ToString();
        }

    }

    public interface INumberConverter
    {
        string ConvertNumberToWords(int input);
    }
}
