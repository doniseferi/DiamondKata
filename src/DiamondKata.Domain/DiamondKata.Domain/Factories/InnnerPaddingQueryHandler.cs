using DiamondKata.DomainService.ValueType;
using System.Text;

namespace DiamondKata.DomainService.Factories;

internal class InnnerPaddingQueryHandler : IInnnerPaddingQueryHandler
{
    private const char InnerPaddingChar = '-';

    public string Handle(EnglishChar @char)
    {
        var sb = new StringBuilder();

        var innerPaddingCharLength = CalculateLength(@char);

        for (var i = 0; i < innerPaddingCharLength; i++)
            sb.Append(InnerPaddingChar);

        return sb.ToString();
    }

    public int CalculateLength(EnglishChar @char)
    {
        var charsNumericalValue = @char.GetNumericalValue();

        var numberOfCharsNeededForInternalPadding = charsNumericalValue * 2 - 1;

        return numberOfCharsNeededForInternalPadding < 0
            ? 0
            : numberOfCharsNeededForInternalPadding;
    }
}