using DiamondKata.DomainService.Exception;

namespace DiamondKata.DomainService.ValueType;

internal sealed class EnglishChar
{
    public EnglishChar(char value)
    {
        var @char = char.ToUpperInvariant(value);

        if (!IsAnEnglishLetter(@char))
            throw new CharIsNotAnEnglishLetterException(@char);

        Value = @char;

        static bool IsAnEnglishLetter(char c) =>
            Enumerable.Range('A', 26)
                .Select(x => (char)x)
                .Contains(c);
    }

    public char Value { get; }

    public int GetOrderInAlphabet()
    {
        const int unicodeValueForUpperCaseA = 65;
        return Value - unicodeValueForUpperCaseA;
    }
}