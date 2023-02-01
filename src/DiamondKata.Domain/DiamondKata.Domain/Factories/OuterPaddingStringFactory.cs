using System.Text;
using DiamondKata.DomainService.QueryHandlers;
using DiamondKata.DomainService.ValueType;

namespace DiamondKata.DomainService.Factories;

internal class OuterPaddingStringFactory : IOuterPaddingStringFactory
{
    private readonly IOuterPaddingLengthQueryHandler _outerPaddingLengthQueryHandler;

    private const char OuterPaddingChar = ' ';

    public OuterPaddingStringFactory(IOuterPaddingLengthQueryHandler outerPaddingLengthQueryHandler)
    {
        _outerPaddingLengthQueryHandler = outerPaddingLengthQueryHandler ??
                                          throw new ArgumentNullException(nameof(outerPaddingLengthQueryHandler));
    }

    public string Create(EnglishChar requestChar, EnglishChar currentRowsChar)
    {
        if (requestChar == null)
            throw new ArgumentNullException(nameof(requestChar));

        if (currentRowsChar == null)
            throw new ArgumentNullException(nameof(currentRowsChar));

        var sb = new StringBuilder();

        var outerPaddingCharLength =
            _outerPaddingLengthQueryHandler
                .Handle(requestChar, currentRowsChar);

        for (var i = 0; i < outerPaddingCharLength; i++)
            sb.Append(OuterPaddingChar);

        return sb.ToString();
    }
}