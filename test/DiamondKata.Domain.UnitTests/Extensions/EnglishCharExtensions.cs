using DiamondKata.DomainService.ValueType;

namespace DiamondKata.Domain.UnitTests.Extensions;

internal class EnglishCharExtensions
{
    public static IReadOnlyCollection<EnglishChar> GetAllEnglishCharacters() =>
        CharExtensions.GetAllEnglishChars()
            .Select(x => new EnglishChar(x))
            .ToList();

    public static IReadOnlyCollection<EnglishChar> GetAllEnglishCharactersExceptA() =>
        CharExtensions.GetAllEnglishChars()
            .Except(new[] { 'A', 'a' }).Select(x => new EnglishChar(x))
            .ToList();
}