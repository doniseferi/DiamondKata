using System.Runtime.CompilerServices;
using System.Text;
using DiamondKata.DomainService.Exception;

[assembly: InternalsVisibleTo("DiamondKata.Domain.UnitTests"),
           InternalsVisibleTo("DiamondKata.Console")]

namespace DiamondKata.DomainService.ValueType;

internal sealed class EnglishChar
{
    public EnglishChar(char value)
    {
        bool IsAnEnglishLetter(char c) =>
            Enumerable.Range('A', 26)
                .Select(x => (char)x)
                .Contains(c);

        var @char = char.ToUpperInvariant(value);

        if (!IsAnEnglishLetter(@char))
            throw new CharIsNotAnEnglishLetterException(@char);

        Value = @char;
    }

    public char Value { get; }

    public int GetNumericalValue()
    {
        const int asciiValueForUpperCaseA = 65;
        return GetAsciiValue() - asciiValueForUpperCaseA;
    }

    private int GetAsciiValue() => Convert.ToInt32(Encoding.ASCII.GetBytes(new[] { Value })[0]);
}