using System.Text;
using DiamondKata.DomainService.ValueType;

namespace DiamondKata.DomainService.QueryHandlers;

internal class OuterPaddingQueryHandler : IOuterPaddingQueryHandler
{
    private const char OuterPaddingChar = ' ';

    public string Handle(EnglishChar @char, EnglishChar lastCharInDiamond)
    {
        if (@char == null)
            throw new ArgumentNullException(nameof(@char));

        if (@char == null)
            throw new ArgumentNullException(nameof(@char));

        var sb = new StringBuilder();

        var outerPaddingCharLength = CalculateOuterPadding(@char, lastCharInDiamond);

        for (var i = 0; i < outerPaddingCharLength; i++)
            sb.Append(OuterPaddingChar);

        return sb.ToString();
    }

    public int CalculateOuterPadding(EnglishChar @char, EnglishChar lastChar)
    {
        var lastCharsOrderValue = lastChar.GetOrderInAlphabet() * 2 + 1;
        var charsOrderValue = @char.GetOrderInAlphabet() * 2 + 1;
        return (lastCharsOrderValue - charsOrderValue) / 2;
    }
}