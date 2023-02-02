namespace DiamondKata.DomainService.Exception;

internal class CharIsNotAnEnglishLetterException : System.Exception
{
    private static readonly Func<char, string> GenerateErrorMessage = attemptedValue
        => $"{attemptedValue} is not a English char. Please supply a single English char";

    public CharIsNotAnEnglishLetterException(char attemptedValue)
        : base(GenerateErrorMessage(attemptedValue))
    {
    }
}