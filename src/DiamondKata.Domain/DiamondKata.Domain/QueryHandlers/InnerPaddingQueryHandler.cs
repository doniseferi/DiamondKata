using System.Text;
using DiamondKata.DomainService.ValueType;

namespace DiamondKata.DomainService.QueryHandlers;

internal class InnerPaddingQueryHandler : IInnerPaddingQueryHandler
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

    private static int CalculateLength(EnglishChar @char)
    {
        var orderValue = @char.GetOrderInAlphabet();

        var numberOfCharsNeededForInternalPadding = orderValue * 2 - 1;

        return numberOfCharsNeededForInternalPadding < 0
            ? 0
            : numberOfCharsNeededForInternalPadding;
    }
}