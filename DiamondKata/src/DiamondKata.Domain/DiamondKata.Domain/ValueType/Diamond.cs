using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("DiamondKata.Domain.UnitTests"),
           InternalsVisibleTo("DiamondKata.Domain.UnitTests")]

namespace DiamondKata.Domain.ValueType;

using OuterPaddingChar = PaddingChar;
using InnerPaddingChar = PaddingChar;
using System.Text;
using System.Collections.Generic;

internal class Diamond
{
    public Diamond(EnglishChar englishChar, OuterPaddingChar outerPadding,
        InnerPaddingChar innerPaddingChar)
    {
        Value = Print(englishChar.Value, outerPadding.Value, innerPaddingChar.Value);
    }

    public string Value { get; }

    private string Print(char c, char outerPadding, char innerPadding)
    {
        var sb = new StringBuilder();
        var d = new Dictionary<char, string>();
        var chars = GetAllChars(c);
        for (var i = chars.Count; i > 0; i--)
        {
            var @char = chars[i - 1];
            d.Add(@char, GenerateRow(@char, innerPadding, outerPadding, c));
        }

        for (int index = 0; index < d.Count; index++)
        {
            var item = d.ElementAt(index);
            var itemValue = item.Value;
            sb.Append(itemValue);
            sb.Append(Environment.NewLine);
        }

        for (int index = d.Count - 1; index > 0; index--)
        {
            var item = d.ElementAt(index - 1);
            var itemValue = item.Value;
            sb.Append(itemValue);

            if (index == 1)
                continue;

            sb.Append(Environment.NewLine);
        }

        return sb.ToString();
    }

    private List<char> GetAllChars(char c)
    {
        var l = new List<char>();
        l.Add(c);
        for (var i = GetNumericalValue(c); i > 0; i--)
        {
            var s = GetFirstLowerLetter(i);
            l.Add(s);
        }

        return l;
    }

    private string GenerateRow(char currentLetter, char innerPadding, char outerPadding, char lastLetter)
    {
        var sb = new StringBuilder();
        sb.Append(AddOuterPadding(currentLetter, outerPadding, lastLetter));
        sb.Append(currentLetter);
        sb.Append(AddInternalPadding(currentLetter, innerPadding));
        if (char.ToUpperInvariant(currentLetter) != char.ToUpperInvariant('a'))
        {
            sb.Append(currentLetter);
        }

        sb.Append(AddOuterPadding(currentLetter, outerPadding, lastLetter));
        return sb.ToString();
    }

    private string AddInternalPadding(char letter, char seperator)
    {
        var sb = new StringBuilder();

        var internalPadding = WHITE__CalculateInternalPadding(letter);

        for (var i = 0; i < internalPadding; i++)
        {
            sb.Append(seperator);
        }

        return sb.ToString();
    }

    private string AddOuterPadding(char previousChar, char seperator, char letter)
    {
        var sb = new StringBuilder();
        var outerPadding = YELLOW__CalculatOuterPadding(letter, previousChar) / 2;
        for (var i = 0; i < outerPadding; i++)
        {
            sb.Append(seperator);
        }

        return sb.ToString();
    }

    private Func<char, int> GetAsciiValue = c => Convert.ToInt32(Encoding.ASCII.GetBytes(new[] {c})[0]);


    private int YELLOW__CalculatOuterPadding(char inputChar, char currentChar)
    {
        var charsNumbericalValue = GetNumericalValue(inputChar);

        var inputCharValue = (charsNumbericalValue * 2) + 1;

        var rowNumericValue = GetNumericalValue(currentChar);

        var rowCharValue = (rowNumericValue * 2) + 1;

        return inputCharValue - rowCharValue;
    }

    private int WHITE__CalculateInternalPadding(char c)
    {
        var charsNumericalValue = GetNumericalValue(c);
        var value = (charsNumericalValue * 2) - 1;
        return value < 0
            ? 0
            : value;
    }

    private int GetNumericalValue(char c)
    {
        var asciiValueForUpperCaseA = 65;
        var val = GetAsciiValue(char.ToUpper(c)) - asciiValueForUpperCaseA;
        return val;
    }

    private char GetFirstLowerLetter(int currentLettersNumericalValue)
    {
        var numericalValue = (65 + (currentLettersNumericalValue - 1));
        return (char) numericalValue;
    }
}