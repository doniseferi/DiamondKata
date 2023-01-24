using DiamondKata.Domain.ValueType;

namespace DiamondKata.DomainService.QueryHandlers;

internal class OuterPaddingLengthQueryHandler : IOuterPaddingLengthQueryHandler
{
    public int Handle(EnglishChar requestChar, EnglishChar @char)
    {
        var requestCharValue = requestChar.GetNumericalValue() * 2 + 1;
        var charValue = @char.GetNumericalValue() * 2 + 1;
        return (requestCharValue - charValue) / 2;
    }
}

internal class InnerPaddingLengthQueryHandler : IInnerPaddingLengthQueryHandler
{
    public int Handle(EnglishChar @char)
    {
        var charsNumericalValue = @char.GetNumericalValue();

        var numberOfCharsNeededForInternalPadding = charsNumericalValue * 2 - 1;

        return numberOfCharsNeededForInternalPadding < 0
            ? 0
            : numberOfCharsNeededForInternalPadding;
    }
}