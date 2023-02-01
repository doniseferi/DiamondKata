using System.Text;
using DiamondKata.DomainService.Factories;
using DiamondKata.DomainService.Requests;

namespace DiamondKata.DomainService.QueryHandlers;

internal class RowGeneratorQueryHandler : IRowGeneratorQueryHandler
{
    private readonly IOuterPaddingStringFactory _outerPaddingStringFactory;
    private readonly IInnerPaddingStringFactory _innerPaddingStringFactory;


    public RowGeneratorQueryHandler(IOuterPaddingStringFactory outerPaddingStringFactory,
        IInnerPaddingStringFactory innerPaddingStringFactory)
    {
        _outerPaddingStringFactory = outerPaddingStringFactory ??
                                       throw new ArgumentNullException(nameof(outerPaddingStringFactory));
        _innerPaddingStringFactory = innerPaddingStringFactory ??
                                       throw new ArgumentNullException(nameof(innerPaddingStringFactory));
    }

    public string Handle(RowQueryRequest request)
    {
        var outerPadding =
            _outerPaddingStringFactory.Create(request.RequestChar, request.CurrentRowsChar);

        var innerPadding =
            _innerPaddingStringFactory.Create(request.CurrentRowsChar);

        var currentChar = request.CurrentRowsChar;

        var sb = new StringBuilder();
        sb.Append(outerPadding);
        sb.Append(currentChar.Value);
        sb.Append(innerPadding);
        return currentChar.Value == char.ToUpperInvariant('A')
            ? sb.Append(outerPadding).ToString()
            : sb.Append(currentChar.Value).Append(outerPadding).ToString();
    }
}