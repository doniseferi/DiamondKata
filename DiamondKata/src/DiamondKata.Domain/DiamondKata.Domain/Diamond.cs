using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("DiamondKata.Domain.UnitTests")]

namespace DiamondKata.Domain;

internal sealed class Diamond
{
    public Diamond(Letter letter)
    {
        Letter = letter 
                 ?? throw new ArgumentNullException(nameof(letter));
    }

    public Letter Letter { get; }
}

internal class CharIsNotALetterException : ArgumentOutOfRangeException
{
    private static readonly Func<char, string> GenerateErrorMessage = (attemptedValue)
        => $"{attemptedValue} is not a letter. Please supply a single letter letter";

    public CharIsNotALetterException(char attemptedValue)
        : base(GenerateErrorMessage(attemptedValue))
    {
    }
}

internal sealed class Letter
{
    public Letter(char value)
    {
        Value = char.IsLetter(value)
            ? value
            : throw new CharIsNotALetterException(value);
    }

    public char Value { get; }
}