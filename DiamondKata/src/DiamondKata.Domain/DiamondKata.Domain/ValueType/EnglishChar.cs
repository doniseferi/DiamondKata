using System.Runtime.CompilerServices;
using DiamondKata.Domain.Exception;
using LanguageExt;

[assembly: InternalsVisibleTo("DiamondKata.Domain.UnitTests")]

namespace DiamondKata.Domain.ValueType;

internal sealed class EnglishChar : NewType<EnglishChar, char>
{
    public EnglishChar(char value) : base(value)
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
}