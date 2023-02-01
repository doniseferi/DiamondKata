using DiamondKata.DomainService.ValueType;

namespace DiamondKata.DomainService.QueryHandlers;

internal class GetLowerEnglishLettersQueryHandlers : IGetLowerEnglishLettersQueryHandlers
{
    private const int AsciiValueForUpperCaseA = 65;

    public IReadOnlyCollection<EnglishChar> Handle(EnglishChar @char)
    {
        if (@char == null)
            throw new ArgumentNullException(nameof(@char));

        var englishLetterAccum = new List<EnglishChar>() { @char };

        for (var i = @char.GetNumericalValue(); i > 0; i--)
        {
            var previousEnglishChar = GetCharPriorTo(i);
            englishLetterAccum.Add(previousEnglishChar);
        }

        return englishLetterAccum;
    }

    private static EnglishChar GetCharPriorTo(int currentCharsNumericalValue)
    {
        var previousCharsAsciiValue = AsciiValueForUpperCaseA + (currentCharsNumericalValue - 1);
        return new EnglishChar((char)previousCharsAsciiValue);
    }
}