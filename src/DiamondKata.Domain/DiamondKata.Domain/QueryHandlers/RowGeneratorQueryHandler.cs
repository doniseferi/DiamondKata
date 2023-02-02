using DiamondKata.DomainService.Factories;
using DiamondKata.DomainService.ValueType;
using System.Text;

namespace DiamondKata.DomainService.QueryHandlers;

internal class RowGeneratorQueryHandler : IRowGeneratorQueryHandler
{
    private readonly IOuterPaddingQueryHandler _outerPaddingQueryHandler;
    private readonly IInnnerPaddingQueryHandler _innnerPaddingQueryHandler;
    private const char FirstEnglishLetter = 'A';


    public RowGeneratorQueryHandler(IOuterPaddingQueryHandler outerPaddingQueryHandler,
        IInnnerPaddingQueryHandler innnerPaddingQueryHandler)
    {
        _outerPaddingQueryHandler = outerPaddingQueryHandler ??
                                     throw new ArgumentNullException(nameof(outerPaddingQueryHandler));
        _innnerPaddingQueryHandler = innnerPaddingQueryHandler ??
                                     throw new ArgumentNullException(nameof(innnerPaddingQueryHandler));
    }

    public string Handle(EnglishChar @char, EnglishChar lastCharInDiamond)
    {
        var outerPadding =
            _outerPaddingQueryHandler.Handle(@char, lastCharInDiamond);

        var innerPadding =
            _innnerPaddingQueryHandler.Handle(@char);

        var sb = new StringBuilder();
        sb.Append(outerPadding);
        sb.Append(@char.Value);
        sb.Append(innerPadding);
        return @char.Value == char.ToUpperInvariant(FirstEnglishLetter)
            ? sb.Append(outerPadding).ToString()
            : sb.Append(@char.Value).Append(outerPadding).ToString();
    }
}