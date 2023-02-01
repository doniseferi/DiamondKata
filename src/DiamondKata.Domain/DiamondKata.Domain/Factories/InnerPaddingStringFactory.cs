using System.Text;
using DiamondKata.DomainService.QueryHandlers;
using DiamondKata.DomainService.ValueType;

namespace DiamondKata.DomainService.Factories;

internal class InnerPaddingStringFactory : IInnerPaddingStringFactory
{
    private const char InnerPaddingChar = '-';
    private readonly IInnerPaddingLengthQueryHandler _innerPaddingLengthQueryHandler;

    public InnerPaddingStringFactory(IInnerPaddingLengthQueryHandler innerPaddingLengthQueryHandler)
    {
        _innerPaddingLengthQueryHandler = innerPaddingLengthQueryHandler ??
                                          throw new ArgumentNullException(nameof(innerPaddingLengthQueryHandler));
    }

    public string Create(EnglishChar @char)
    {
        if (@char == null)
            throw new ArgumentNullException(nameof(@char));

        var sb = new StringBuilder();

        var innerPaddingCharLength = _innerPaddingLengthQueryHandler.Handle(@char);

        for (var i = 0; i < innerPaddingCharLength; i++)
            sb.Append(InnerPaddingChar);

        return sb.ToString();
    }
}