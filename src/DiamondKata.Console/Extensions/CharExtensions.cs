namespace DiamondKata.Console.Extensions;

internal static class CharExtensions
{
    public static bool IsAnEnglishLetter(this char c) =>
        Enumerable.Range('A', 26)
            .Select(x => (char)x)
            .Concat(
                Enumerable.Range('a', 26)
                    .Select(x => (char)x))
            .ToList()
            .Contains(c);

    public static bool IsPrintableChar(this char c) =>
        char.IsControl(c);
}