namespace DiamondKata.Domain.UnitTests.Extensions;

internal class CharExtensions
{
    public static IReadOnlyList<char> GetAllEnglishChars() =>
        "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz"
            .ToCharArray();

    public static IReadOnlyCollection<char> GetAllPrintableChars() =>
        GetAllChars()
            .Where(x => !char.IsControl(x))
            .ToList();

    public static IReadOnlyCollection<char> GetAllChars() =>
        Enumerable.Range(char.MinValue, char.MaxValue + 1).Select(x => (char)x)
            .Select(i => i)
            .ToList();
}