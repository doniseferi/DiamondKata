using System.Text;
using DiamondKata.DomainService.QueryHandlers.GetPadding;
using DiamondKata.DomainService.ValueType;

namespace DiamondKata.DomainService.QueryHandlers.GetRowForChar;

internal class GetRowForCharQueryHandler(
    IGetOuterPaddingQueryHandler getOuterPaddingQueryHandler,
    IGetInnerPaddingQueryHandler getInnerPaddingQueryHandler)
    : IGetRowForCharQueryHandler
{
    private readonly IGetInnerPaddingQueryHandler _getInnerPaddingQueryHandler = getInnerPaddingQueryHandler ??
                                                                                 throw new ArgumentNullException(nameof(getInnerPaddingQueryHandler));
    private readonly IGetOuterPaddingQueryHandler _getOuterPaddingQueryHandler = getOuterPaddingQueryHandler ??
                                                                                 throw new ArgumentNullException(nameof(getOuterPaddingQueryHandler));

    public string Handle(EnglishChar @char, EnglishChar lastCharInDiamond)
    {
        ArgumentNullException.ThrowIfNull(@char);

        ArgumentNullException.ThrowIfNull(lastCharInDiamond);

        var outerPadding =
            _getOuterPaddingQueryHandler.Handle(@char: @char, lastCharInDiamond: lastCharInDiamond);

        var innerPadding =
            _getInnerPaddingQueryHandler.Handle(@char);

        var resultBuilder = new StringBuilder();
        resultBuilder.Append(outerPadding);
        resultBuilder.Append(@char.Value);
        resultBuilder.Append(innerPadding);

        return IsTheEnglishLetterCharA(@char)
            ? resultBuilder.Append(outerPadding).ToString()
            : resultBuilder.Append(@char.Value).Append(outerPadding).ToString();
    }

    private static bool IsTheEnglishLetterCharA(EnglishChar @char)
    {
        const char firstEnglishLetter = 'A';

        return char.ToUpperInvariant(@char.Value) == char.ToUpperInvariant(firstEnglishLetter);
    }
}