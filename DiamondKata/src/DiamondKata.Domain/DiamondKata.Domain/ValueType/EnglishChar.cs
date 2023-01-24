using DiamondKata.Domain.Exception;
using LanguageExt;
using System.Text;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("DiamondKata.Domain.UnitTests"),
           InternalsVisibleTo("DiamondKata.Console")]

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

    private int GetAsciiValue() => Convert.ToInt32(Encoding.ASCII.GetBytes(new[] {Value})[0]);
}