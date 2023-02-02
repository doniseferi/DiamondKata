namespace DiamondKata.Domain.UnitTests.Extensions;

internal class CharExtensions
{
    public static IReadOnlyList<char> GetAllEnglishChars() =>
        "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz"
            .ToList();

    public static IReadOnlyCollection<char> GetAllAsciiChars() =>
        Enumerable.Range('\x1', 127).Select(x => (char)x)
            .ToList();
}