using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Reflection;
using System.Globalization;
using System.Text.RegularExpressions;

namespace MyMath
{
    public class Example1
    {
        /// <summary>
        /// example illustrates how a StringBuilder object 
        /// allocates new memory and increases its capacity dynamically as the string assigned to the object expands.
        /// The example displays the following output:
        ///    Capacity: 16    MaxCapacity: 2,147,483,647    Length: 0
        ///    Capacity: 32    MaxCapacity: 2,147,483,647    Length: 19
        ///    Capacity: 64    MaxCapacity: 2,147,483,647    Length: 50
        /// </summary>
        public static void ExampleSB1()
        {
            StringBuilder sb = new StringBuilder();
            ShowSBInfo(sb);
            sb.Append("This is a sentence.");
            ShowSBInfo(sb);
            for (int ctr = 0; ctr <= 10; ctr++)
            {
                sb.Append("This is an additional sentence.");
                ShowSBInfo(sb);
            }
        }

        private static void ShowSBInfo(StringBuilder sb)
        {
            foreach (var prop in sb.GetType().GetProperties())
            {
                if (prop.GetIndexParameters().Length == 0)
                    Console.Write("{0}: {1:N0}    ", prop.Name, prop.GetValue(sb));
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Calling StringBuilder methods
        ///  individual method calls and ignore the return value,
        ///  The example displays the following output:
        /// This is a complete sentence.
        /// </summary>
        public static void ExampleSB2()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("This is the beginning of a sentence, ");
            sb.Replace("the beginning of ", "");
            sb.Insert(sb.ToString().IndexOf("a ") + 2, "complete ");
            sb.Replace(",", ".");
            Console.WriteLine(sb.ToString());
        }

        /// <summary>
        ///  Calling StringBuilder methods
        ///  series of method calls in a single statement
        ///  The example displays the following output:
        ///    This is a complete sentence.
        /// </summary>
        public static void ExampleSB3()
        {
            StringBuilder sb = new StringBuilder("This is the beginning of a sentence, ");
            sb.Replace("the beginning of ", "").Insert(sb.ToString().IndexOf("a ") + 2,
                                                       "complete ").Replace(",", ".");
            Console.WriteLine(sb.ToString());
        }

        /// <summary>
        /// Iterating StringBuilder characters
        ///  The example displays the following output:
        ///    The original string:
        ///    1,457,531,530.00000940,522,609.000001,668,113,564.000001,998,992,883.000001,792,660,834.00
        ///    000101,203,251.000002,051,183,075.000002,066,000,067.000001,643,701,043.000001,702,382,508
        ///    .00000
        ///    
        ///    The new string:
        ///    0,346,420,429.99999839,411,598.999990,557,002,453.999990,887,881,772.999990,681,559,723.99
        ///    999090,192,140.999991,940,072,964.999991,955,999,956.999990,532,690,932.999990,691,271,497
        ///    .99999
        /// </summary>
        public static void ExampleSB4()
        {
            Random rnd = new Random();
            StringBuilder sb = new StringBuilder();

            // Generate 10 random numbers and store them in a StringBuilder.
            for (int ctr = 0; ctr <= 9; ctr++)
                sb.Append(rnd.Next().ToString("N5"));

            Console.WriteLine("The original string:");
            Console.WriteLine(sb.ToString());

            // Decrease each number by one.
            for (int ctr = 0; ctr < sb.Length; ctr++)
            {
                if (Char.GetUnicodeCategory(sb[ctr]) == UnicodeCategory.DecimalDigitNumber)
                {
                    int number = (int)Char.GetNumericValue(sb[ctr]);
                    number--;
                    if (number < 0) number = 9;

                    sb[ctr] = number.ToString()[0];
                }
            }
            Console.WriteLine("\nThe new string:");
            Console.WriteLine(sb.ToString());
        }

        /// <summary>
        /// Adding text to a StringBuilder object
        /// The example displays the following output:
        ///    ********** Adding Text to a StringBuilder Object **********
        ///    
        ///    Some code points and their corresponding characters:
        ///    
        ///       Code Unit    Character
        ///            0032            2
        ///            0033            3
        ///            0034            4
        /// </summary>
        public static void ExampleSB5()
        {
            // Create a StringBuilder object with no text.
            StringBuilder sb = new StringBuilder();
            // Append some text.
            sb.Append('*', 10).Append(" Adding Text to a StringBuilder Object ").Append('*', 10);
            sb.AppendLine("\n");
            sb.AppendLine("Some code points and their corresponding characters:");
            // Append some formatted text.
            for (int ctr = 50; ctr <= 60; ctr++)
            {
                sb.AppendFormat("{0,12:X4} {1,12}", ctr, Convert.ToChar(ctr));
                sb.AppendLine();
            }
            // Find the end of the introduction to the column.
            int pos = sb.ToString().IndexOf("characters:") + 11 +
                      Environment.NewLine.Length;
            // Insert a column header.
            sb.Insert(pos, String.Format("{2}{0,12:X4} {1,12}{2}", "Code Unit",
                                         "Character", "\n"));

            // Convert the StringBuilder to a string and display it.      
            Console.WriteLine(sb.ToString());
        }

        /// <summary>
        /// Deleting text from a StringBuilder object
        /// The example displays the following output:
        ///    Value: A StringBuilder object
        ///    Capacity: 22    MaxCapacity: 2,147,483,647    Length: 22
        ///    
        ///    Value: A StringBuilder
        ///    Capacity: 22    MaxCapacity: 2,147,483,647    Length: 16
        ///    
        ///    Value:
        ///    Capacity: 22    MaxCapacity: 2,147,483,647    Length: 0
        /// </summary>
        public static void ExampleSB6()
        {
            StringBuilder sb = new StringBuilder("A StringBuilder object");
            ShowSBInfo(sb);
            // Remove "object" from the text.
            string textToRemove = "object";
            int pos = sb.ToString().IndexOf(textToRemove);
            if (pos >= 0)
            {
                sb.Remove(pos, textToRemove.Length);
                ShowSBInfo(sb);
            }
            // Clear the StringBuilder contents.
            sb.Clear();
            ShowSBInfo(sb);
        }

        /// <summary>
        /// The example displays the following output:
        ///  Hello World?
        /// </summary>
        public static void ExampleSB7()
        {
            StringBuilder MyStringBuilder = new StringBuilder("Hello World!");
            MyStringBuilder.Replace('!', '?');
            Console.WriteLine(MyStringBuilder);
        }

        /// <summary>
        /// The example displays output similar to the following:
        ///    Average Daily Temperature in Degrees Celsius
        ///    
        ///    5/1/2013: 21.2C
        ///    5/2/2013: 16.1C
        ///    5/3/2013: 23.5C
        ///    5/4/2013: 22.9C
        /// </summary>
        public static void ExampleSB8()
        {
            Random rnd = new Random();
            string[] tempF = { "47.6F", "51.3F", "49.5F", "62.3F" };
            string[] tempC = { "21.2C", "16.1C", "23.5C", "22.9C" };
            string[][] temps = { tempF, tempC };

            StringBuilder sb = new StringBuilder();
            var f = new StringBuilderFinder(sb, "F");
            var baseDate = new DateTime(2013, 5, 1);
            String[] temperatures = temps[rnd.Next(2)];
            bool isFahrenheit = false;
            foreach (var temperature in temperatures)
            {
                if (isFahrenheit)
                    sb.AppendFormat("{0:d}: {1}\n", baseDate, temperature);
                else
                    isFahrenheit = f.SearchAndAppend(String.Format("{0:d}: {1}\n",
                                                     baseDate, temperature));
                baseDate = baseDate.AddDays(1);
            }
            if (isFahrenheit)
            {
                sb.Insert(0, "Average Daily Temperature in Degrees Fahrenheit");
                sb.Insert(47, "\n\n");
            }
            else
            {
                sb.Insert(0, "Average Daily Temperature in Degrees Celsius");
                sb.Insert(44, "\n\n");
            }
            Console.WriteLine(sb.ToString());
        }

        /// <summary>
        /// converts the text to a String object and uses a regular expression to identify the starting position of each four-character sequence. 
        /// The example displays the following output:
        ///    Aaaa_Bbbb_Cccc_Dddd_Eeee_Ffff_Gggg_Hhhh_Iiii_Jjjj_Kkkk_Llll_Mmmm_Nnnn_Oooo_Pppp_
        ///    Qqqq_Rrrr_Ssss_Tttt_Uuuu_Vvvv_Wwww_Xxxx_Yyyy_Zzzz
        /// </summary>
        public static void ExampleSB9()
        {
            // Create a StringBuilder object with 4 successive occurrences 
            // of each character in the English alphabet. 
            StringBuilder sb = new StringBuilder();
            for (ushort ctr = (ushort)'a'; ctr <= (ushort)'z'; ctr++)
                sb.Append(Convert.ToChar(ctr), 4);

            // Create a parallel string object.
            String sbString = sb.ToString();
            // Determine where each new character sequence begins.
            String pattern = @"(\w)\1+";
            MatchCollection matches = Regex.Matches(sbString, pattern);

            // Uppercase the first occurrence of the sequence, and separate it
            // from the previous sequence by an underscore character.
            for (int ctr = matches.Count - 1; ctr >= 0; ctr--)
            {
                Match m = matches[ctr];
                sb[m.Index] = Char.ToUpper(sb[m.Index]);
                if (m.Index > 0) sb.Insert(m.Index, "_");
            }
            // Display the resulting string.
            sbString = sb.ToString();
            int line = 0;
            do
            {
                int nChars = line * 80 + 79 <= sbString.Length ?
                                    80 : sbString.Length - line * 80;
                Console.WriteLine(sbString.Substring(line * 80, nChars));
                line++;
            } while (line * 80 < sbString.Length);
        }

        /// <summary>
        /// uses the Chars property to detect when a character value has changed, 
        /// inserts an underscore at that position, and converts the first character
        /// The example displays the following output:
        ///    Aaaa_Bbbb_Cccc_Dddd_Eeee_Ffff_Gggg_Hhhh_Iiii_Jjjj_Kkkk_Llll_Mmmm_Nnnn_Oooo_Pppp_
        ///    Qqqq_Rrrr_Ssss_Tttt_Uuuu_Vvvv_Wwww_Xxxx_Yyyy_Zzzz
        /// 
        /// </summary>
        public static void ExampleSB10()
        {
            // Create a StringBuilder object with 4 successive occurrences 
            // of each character in the English alphabet. 
            StringBuilder sb = new StringBuilder();
            for (ushort ctr = (ushort)'a'; ctr <= (ushort)'z'; ctr++)
                sb.Append(Convert.ToChar(ctr), 4);

            // Iterate the text to determine when a new character sequence occurs.
            int position = 0;
            Char current = '\u0000';
            do
            {
                if (sb[position] != current)
                {
                    current = sb[position];
                    sb[position] = Char.ToUpper(sb[position]);
                    if (position > 0)
                        sb.Insert(position, "_");
                    position += 2;
                }
                else
                {
                    position++;
                }
            } while (position <= sb.Length - 1);
            // Display the resulting string.
            String sbString = sb.ToString();
            int line = 0;
            do
            {
                int nChars = line * 80 + 79 <= sbString.Length ?
                                    80 : sbString.Length - line * 80;
                Console.WriteLine(sbString.Substring(line * 80, nChars));
                line++;
            } while (line * 80 < sbString.Length);
        }

        /// <summary>
        /// creates a StringBuilder object, converts it to a String object, and then uses a regular expression
        /// The example displays the following output:
        ///    Aaaa_Bbbb_Cccc_Dddd_Eeee_Ffff_Gggg_Hhhh_Iiii_Jjjj_Kkkk_Llll_Mmmm_Nnnn_Oooo_Pppp_
        ///    Qqqq_Rrrr_Ssss_Tttt_Uuuu_Vvvv_Wwww_Xxxx_Yyyy_Zzzz
        /// </summary>
        public static void ExampleSB11()
        {
            // Create a StringBuilder object with 4 successive occurrences 
            // of each character in the English alphabet. 
            StringBuilder sb = new StringBuilder();
            for (ushort ctr = (ushort)'a'; ctr <= (ushort)'z'; ctr++)
                sb.Append(Convert.ToChar(ctr), 4);

            // Convert it to a string.
            String sbString = sb.ToString();

            // Use a regex to uppercase the first occurrence of the sequence, 
            // and separate it from the previous sequence by an underscore.
            string pattern = @"(\w)(\1+)";
            sbString = Regex.Replace(sbString, pattern,
                                     m => (m.Index > 0 ? "_" : "") +
                                     m.Groups[1].Value.ToUpper() +
                                     m.Groups[2].Value);

            // Display the resulting string.
            int line = 0;
            do
            {
                int nChars = line * 80 + 79 <= sbString.Length ?
                                    80 : sbString.Length - line * 80;
                Console.WriteLine(sbString.Substring(line * 80, nChars));
                line++;
            } while (line * 80 < sbString.Length);
        }

        /// <summary>
        /// This code produces the following output.
        /// 11 chars: ABCDEFGHIJk
        /// 21 chars: Alphabet: ABCDEFGHIJK
        /// </summary>
        public static void ExampleSB12()
        {
            // Create a StringBuilder that expects to hold 50 characters.
            // Initialize the StringBuilder with "ABC".
            StringBuilder sb = new StringBuilder("ABC", 50);

            // Append three characters (D, E, and F) to the end of the StringBuilder.
            sb.Append(new char[] { 'D', 'E', 'F' });

            // Append a format string to the end of the StringBuilder.
            sb.AppendFormat("GHI{0}{1}", 'J', 'k');

            // Display the number of characters in the StringBuilder and its string.
            Console.WriteLine("{0} chars: {1}", sb.Length, sb.ToString());

            // Insert a string at the beginning of the StringBuilder.
            sb.Insert(0, "Alphabet: ");

            // Replace all lowercase k's with uppercase K's.
            sb.Replace('k', 'K');

            // Display the number of characters in the StringBuilder and its string.
            Console.WriteLine("{0} chars: {1}", sb.Length, sb.ToString());
        }

    }
}


public class StringBuilderFinder
{
    private StringBuilder sb;
    private String text;

    public StringBuilderFinder(StringBuilder sb, String textToFind)
    {
        this.sb = sb;
        this.text = textToFind;
    }

    public bool SearchAndAppend(String stringToSearch)
    {
        sb.Append(stringToSearch);
        return stringToSearch.Contains(text);
    }
}