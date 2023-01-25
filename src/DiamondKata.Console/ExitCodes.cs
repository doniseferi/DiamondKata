namespace DiamondKata.Console;

internal enum ExitCode : int
{
    Success = 0,
    InvalidEnglishChar = -1,
    InvalidOuterPaddingChar = 1,
    InvalidInnerPaddingChar = 2,
    UnexpectedError = 5,
}