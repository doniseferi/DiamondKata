using System.Text;
using DiamondKata.DomainService.ValueType;

namespace DiamondKata.DomainService.QueryHandlers.GetPadding;

internal class GetInnerPaddingQueryHandler : IGetInnerPaddingQueryHandler
{
    private const char InnerPaddingChar = '-';

    public string Handle(EnglishChar @char)
    {
        var resultBuilder = new StringBuilder();

        var innerPaddingCharLength = GetInnerPaddingLength(@char);

        for (var i = 0; i < innerPaddingCharLength; i++)
            resultBuilder.Append(InnerPaddingChar);

        return resultBuilder.ToString();
    }

    private static int GetInnerPaddingLength(EnglishChar @char)
    {
        var orderValue = @char.GetOrderInAlphabet();

        var numberOfCharsNeededForInternalPadding = orderValue * 2 - 1;

        return numberOfCharsNeededForInternalPadding;
    }
}