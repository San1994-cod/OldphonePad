// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;

class OldPhonePad
{
    // Dictionary to map keypad numbers to letters
    private static Dictionary<char, string> keypad = new Dictionary<char, string>()
    {
        {'2', "ABC"},
        {'3', "DEF"},
        {'4', "GHI"},
        {'5', "JKL"},
        {'6', "MNO"},
        {'7', "PQRS"},
        {'8', "TUV"},
        {'9', "WXYZ"},
        {'0', " "} // 0 = space
    };

    // Method to convert keypad input into text
    public static string Translate(string input)
    {
        string result = "";   // stores the final output
        char lastKey = '\0';  // keeps track of the last pressed key
        int pressCount = 0;   // counts consecutive presses of the same key

        foreach (char c in input)
        {
            if (c == '#') // send key → finalize and stop
            {
                if (lastKey != '\0')
                {
                    result += GetLetter(lastKey, pressCount);
                }
                break;
            }
            else if (c == ' ') // pause → finalize current letter
            {
                if (lastKey != '\0')
                {
                    result += GetLetter(lastKey, pressCount);
                    lastKey = '\0';
                    pressCount = 0;
                }
            }
            else // number key
            {
                if (c == lastKey)
                {
                    pressCount++; // pressed same key again
                }
                else
                {
                    if (lastKey != '\0')
                    {
                        result += GetLetter(lastKey, pressCount);
                    }
                    lastKey = c;
                    pressCount = 1;
                }
            }
        }
        return result;
    }

    // Helper method: convert press count into correct letter
    private static char GetLetter(char key, int count)
    {
        if (keypad.ContainsKey(key))
        {
            string letters = keypad[key];
            int index = (count - 1) % letters.Length;
            return letters[index];
        }
        return key; // fallback (shouldn’t happen)
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter input(end with #):");
        string input = Console.ReadLine();

        string output = OldPhonePad.Translate(input);
        Console.WriteLine("Output: " + output);
    }
}

