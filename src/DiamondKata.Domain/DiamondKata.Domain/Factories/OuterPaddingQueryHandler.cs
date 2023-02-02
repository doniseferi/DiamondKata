using DiamondKata.DomainService.ValueType;
using System.Text;

namespace DiamondKata.DomainService.Factories;

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
        var requestCharValue = lastChar.GetNumericalValue() * 2 + 1;
        var charValue = @char.GetNumericalValue() * 2 + 1;
        return (requestCharValue - charValue) / 2;
    }
}