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
        var outerPadding =
            _outerPaddingQueryHandler.Handle(@char: @char, lastCharInDiamond: lastCharInDiamond);

        var innerPadding =
            _innerPaddingQueryHandler.Handle(@char);

        var sb = new StringBuilder();
        sb.Append(outerPadding);
        sb.Append(@char.Value);
        sb.Append(innerPadding);

        return IsTheEnglishLetterCharA(@char)
            ? sb.Append(outerPadding).ToString()
            : sb.Append(@char.Value).Append(outerPadding).ToString();
    }

    private bool IsTheEnglishLetterCharA(EnglishChar @char)
    {
        const char firstEnglishLetter = 'A';

        return char.ToUpperInvariant(@char.Value) == char.ToUpperInvariant(firstEnglishLetter);
    }
}