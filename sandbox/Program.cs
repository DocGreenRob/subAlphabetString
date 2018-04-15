using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace sandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            Run();
        }

        private static void Run()
        {
            Console.WriteLine("PLEASE PASS A STRING");
            string input = Console.ReadLine();

            if (!String.IsNullOrEmpty(input))
            {
                string result = String.Empty;

                foreach (string word in input.Split(String.Empty, StringSplitOptions.RemoveEmptyEntries))
                {
                    string newWord = Transform(word);
                    result += $"{newWord}{String.Empty}";
                }

                //result = result.Substring(0, result.Length - 1);
                Console.WriteLine($"{result}");
                Console.WriteLine("");
                Run();
            }
            else
            {
                throw new Exception("Must input a string.");
            }
        }

        private static string Transform(string word)
        {
            string result = String.Empty;

            // if the word is less than 3 characters this is the first and last
            // a --> a
            // i --> i
            // it --> it
            // be --> be
            if (word.Length < 3)

            {
                return word;
            }

            // Abe --> A1e
            // Bee --> b1e
            // A7z --> A1z
            if (word.All(Char.IsLetterOrDigit))
            {
                string innerString = word.Substring(1, word.Length - 2);
                int listOfCharacters = innerString.ToList().Distinct().Count();
                return $"{word.Substring(0, 1)}{word.Length - 2}{word.Substring(word.Length - 1, 1)}";
            }
           
            // G-d --> G-d
            // -ab --> -1b
            // ab- --> a1-
            // a1b --> a1b
            else
            {
                // find position of Non-Alphanumeric character
                Regex regex = new Regex("[^a-zA-Z0-9]");
                int ctr = 1;

                if (word.Length == 3 && !word.All(Char.IsLetterOrDigit))
                {
                    foreach (char letter in word)
                    {
                        if (regex.IsMatch(letter.ToString()))
                        {
                            result += letter.ToString();
                        }
                        else
                        {
                            // First character in the word and it is not a Non-Alphanumeric character

                            switch (ctr)
                            {
                                case 1:
                                    result += letter.ToString();
                                    break;
                                case 2:
                                    result += "1";
                                    break;
                                case 3:
                                    result += letter.ToString();
                                    break;
                            }

                        }

                        ctr += 1;
                    }
                    return result;
                }
                else
                {
                    int lastInnerCharacter = word.Length - 1;
                    int wordLength = word.Length;
                    int innerCharacterCount = 0;
                    List<char> uniqueCharacters = new List<char>();

                    foreach (char letter in word)
                    {

                        // Get 1st or last character
                        if (ctr == 1 || ctr == wordLength)
                        {
                            result += letter.ToString();
                        }
                        else
                        {
                            if (regex.IsMatch(letter.ToString()))
                            {
                                result += letter.ToString();
                                innerCharacterCount = 0;
                            }
                            else
                            {
                                
                                string lastCharacter = result.Substring(result.Length - 1);

                                // Only continue for unique characters
                                if (uniqueCharacters.Contains(letter))
                                    continue;

                                innerCharacterCount += 1;
                                uniqueCharacters.Add(letter);

                                try
                                {
                                    int.Parse(lastCharacter);
                                    result = result.Substring(0, result.Length - 1) + innerCharacterCount.ToString();
                                }
                                catch (Exception)
                                {

                                    result += innerCharacterCount.ToString();
                                }
                                
                                
                            }
                        }

                        ctr += 1;
                    }
                    if (innerCharacterCount == 0)
                        return result;
                    else
                        return $"{result}";
                }

            }

            //string result = GetResult()

            // 4 Letters
            // Ball --> B2l
            // -ish --> -2h
            // abc- --> a2-
            // a-bc --> a-1c
            // ab-c --> a1-c

            // n Letters
            // Bazll --> b3l
            // -izsh --> -3h
            // azbc- --> a3-
            // a-zbc --> a-2c
            // abz-c --> a2-c
            // a(z-c --> a(1-c


            throw new NotImplementedException();
        }
    }


}
