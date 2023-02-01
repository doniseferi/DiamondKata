namespace DiamondKata.Console.Extensions;

internal static class CharExtensions
{
    public static bool IsAnEnglishLetter(this char c) =>
        Enumerable.Range('A', 26)
            .Select(x => (char)x)
            .Contains(char.ToUpperInvariant(c));
}