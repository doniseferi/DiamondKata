namespace DiamondKata.Domain.Exception;

using System;

internal class CharIsNotAWrittenSymbolException : Exception
{
    private static string GenerateErrorMessage() =>
        "The attempted char is not a written symbol. Please supply a char that represents a written symbol";

    public CharIsNotAWrittenSymbolException() 
        : base(GenerateErrorMessage())
    {
    }
}