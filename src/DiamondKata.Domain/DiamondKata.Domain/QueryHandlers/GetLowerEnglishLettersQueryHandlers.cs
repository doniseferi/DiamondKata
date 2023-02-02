using DiamondKata.DomainService.ValueType;

namespace DiamondKata.DomainService.QueryHandlers;

internal class GetLowerEnglishLettersQueryHandlers : IGetLowerEnglishLettersQueryHandlers
{
    private const int AsciiValueForUpperCaseA = 65;

    public IReadOnlyCollection<EnglishChar> Handle(EnglishChar @char)
    {
        if (@char == null)
            throw new ArgumentNullException(nameof(@char));

        var englishLetterAccum = new List<EnglishChar>();

        for (var i = (int) @char.Value; i > AsciiValueForUpperCaseA - 1; i--)
        {
            var previousEnglishChar = new EnglishChar((char) i);
            englishLetterAccum.Add(previousEnglishChar);
        }

        return englishLetterAccum;
    }
}