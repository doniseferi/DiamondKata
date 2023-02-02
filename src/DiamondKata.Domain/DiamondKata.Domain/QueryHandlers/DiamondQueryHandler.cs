using DiamondKata.DomainService.ValueType;
using System.Text;

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

    public string Handle(EnglishChar @char)
    {
        if (@char == null)
            throw new ArgumentNullException(nameof(@char));

        var rows = GenerateOutputPerChar(@char, @char);

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

    private Dictionary<EnglishChar, string> GenerateOutputPerChar(EnglishChar @char, EnglishChar lastCharInDiamond)
        => _getLowerEnglishLettersQueryHandlers
            .Handle(@char)
            .Select(currentChar => new
            {
                Key = currentChar,
                Value = _rowGeneratorQueryHandler.Handle(
                    currentChar, lastCharInDiamond)
            })
            .ToDictionary(t => t.Key, t => t.Value);
}