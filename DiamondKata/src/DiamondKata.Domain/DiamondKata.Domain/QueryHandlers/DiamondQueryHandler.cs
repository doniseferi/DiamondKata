using System.Text;
using DiamondKata.Domain.ValueType;
using DiamondKata.DomainService.Requests;

namespace DiamondKata.DomainService.QueryHandlers;

internal class DiamondQueryHandler : IDiamondQueryHandler
{
    private readonly IRowGeneratorQueryHandler _rowGeneratorQueryHandler;
    private readonly IGetLowerEnglishLettersQueryHandlers _getLowerEnglishLettersQueryHandlers;

    public DiamondQueryHandler(IRowGeneratorQueryHandler rowGeneratorQueryHandler,
        IGetLowerEnglishLettersQueryHandlers getLowerEnglishLettersQueryHandlers)
    {
        _rowGeneratorQueryHandler = rowGeneratorQueryHandler ??
                                    throw new ArgumentNullException(nameof(rowGeneratorQueryHandler));

        _getLowerEnglishLettersQueryHandlers = getLowerEnglishLettersQueryHandlers ??
                                               throw new ArgumentNullException(
                                                   nameof(getLowerEnglishLettersQueryHandlers));
    }

    public string Handle(DiamondRequest request)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));


        var rows = GenerateOutputPerChar(request);

        return new StringBuilder()
            .Append(string
                .Join(
                    Environment.NewLine,
                    rows
                        .Select(x => x.Value)
                        .Reverse()))
            .Append(Environment.NewLine)
            .Append(string
                .Join(
                    Environment.NewLine,
                    rows
                        .Skip(1)
                        .Select(x => x.Value)))
            .ToString();
    }


    private Dictionary<EnglishChar, string> GenerateOutputPerChar(DiamondRequest request)
        => _getLowerEnglishLettersQueryHandlers
            .Handle(request.RequestChar)
            .Select(currentChar => new
            {
                Key = currentChar,
                Value = _rowGeneratorQueryHandler.Handle(
                    new RowQueryRequest(
                        request.RequestChar,
                        currentChar,
                        request.OuterPaddingChar,
                        request.InnerPaddingChar))
            })
            .ToDictionary(t => t.Key, t => t.Value);
}