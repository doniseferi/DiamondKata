using System.Text;
using DiamondKata.DomainService.ValueType;

namespace DiamondKata.DomainService.QueryHandlers;

internal class RowGeneratorQueryHandler : IRowGeneratorQueryHandler
{
    private readonly IInnerPaddingQueryHandler _innerPaddingQueryHandler;
    private readonly IOuterPaddingQueryHandler _outerPaddingQueryHandler;

    public RowGeneratorQueryHandler(IOuterPaddingQueryHandler outerPaddingQueryHandler,
        IInnerPaddingQueryHandler innerPaddingQueryHandler)
    {
        _outerPaddingQueryHandler = outerPaddingQueryHandler ??
                                    throw new ArgumentNullException(nameof(outerPaddingQueryHandler));
        _innerPaddingQueryHandler = innerPaddingQueryHandler ??
                                    throw new ArgumentNullException(nameof(innerPaddingQueryHandler));
    }

    public string Handle(EnglishChar @char, EnglishChar lastCharInDiamond)
    {
        if (@char == null)
            throw new ArgumentNullException(nameof(@char));

        if (lastCharInDiamond == null)
            throw new ArgumentNullException(nameof(lastCharInDiamond));

        var outerPadding =
            _outerPaddingQueryHandler.Handle(@char: @char, lastCharInDiamond: lastCharInDiamond);

        var innerPadding =
            _innerPaddingQueryHandler.Handle(@char);

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