using System.Text;
using DiamondKata.DomainService.Generators;
using DiamondKata.DomainService.Requests;

namespace DiamondKata.DomainService.QueryHandlers;

internal class RowGeneratorQueryHandler : IRowGeneratorQueryHandler
{
    private readonly IOuterPaddingStringGenerator _outerPaddingStringGenerator;
    private readonly IInnerPaddingStringGenerator _innerPaddingStringGenerator;


    public RowGeneratorQueryHandler(IOuterPaddingStringGenerator outerPaddingStringGenerator,
        IInnerPaddingStringGenerator innerPaddingStringGenerator)
    {
        _outerPaddingStringGenerator = outerPaddingStringGenerator ??
                                       throw new ArgumentNullException(nameof(outerPaddingStringGenerator));
        _innerPaddingStringGenerator = innerPaddingStringGenerator ??
                                       throw new ArgumentNullException(nameof(innerPaddingStringGenerator));
    }

    public string Handle(RowQueryRequest request)
    {
        var outerPadding =
            _outerPaddingStringGenerator.Generate(request.RequestChar, request.CurrentRowsChar,
                request.OuterPaddingChar);

        var innerPadding =
            _innerPaddingStringGenerator.Generate(request.CurrentRowsChar, request.InnerPaddingChar);

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