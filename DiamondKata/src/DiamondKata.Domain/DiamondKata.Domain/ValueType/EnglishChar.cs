using DiamondKata.Domain.Exception;
using LanguageExt;
using System.Text;

namespace DiamondKata.Domain.ValueType;

internal sealed class EnglishChar : NewType<EnglishChar, char>
{
    public EnglishChar(char value) : base(char.ToUpperInvariant(value))
    {
        bool IsAnEnglishLetter(char c) =>
            Enumerable.Range('A', 26)
                .Select(x => (char) x)
                .Concat(
                    Enumerable.Range('a', 26)
                        .Select(x => (char) x))
                .ToList()
                .Contains(c);

        if (!IsAnEnglishLetter(value))
            throw new CharIsNotAnEnglishLetterException(value);
    }

    public int GetNumericalValue()
    {
        var asciiValueForUpperCaseA = 65;
        return GetAsciiValue() - asciiValueForUpperCaseA;
    }

    public EnglishChar GetEnglishCharBefore()
    {
        var numericalValue = (65 + (GetNumericalValue() - 1));
        return new EnglishChar((char) numericalValue);
    }

    private int GetAsciiValue() => Convert.ToInt32(Encoding.ASCII.GetBytes(new[] {Value})[0]);
}