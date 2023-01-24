using System.Text;
using DiamondKata.Domain.ValueType;
using DiamondKata.DomainService.QueryHandlers;

namespace DiamondKata.DomainService.Factories;

internal class InnerPaddingStringFactory : IInnerPaddingStringFactory
{
    private readonly IInnerPaddingLengthQueryHandler _innerPaddingLengthQueryHandler;

    public InnerPaddingStringFactory(IInnerPaddingLengthQueryHandler innerPaddingLengthQueryHandler)
    {
        _innerPaddingLengthQueryHandler = innerPaddingLengthQueryHandler ??
                                          throw new ArgumentNullException(nameof(innerPaddingLengthQueryHandler));
    }

    public string Create(EnglishChar @char, PaddingChar innerPaddingChar)
    {
        if (@char == null)
            throw new ArgumentNullException(nameof(@char));

        if (innerPaddingChar == null)
            throw new ArgumentNullException(nameof(innerPaddingChar));

        var sb = new StringBuilder();

        var innerPaddingCharLength = _innerPaddingLengthQueryHandler.Handle(@char);

        for (var i = 0; i < innerPaddingCharLength; i++)
            sb.Append(innerPaddingChar.Value);

        return sb.ToString();
    }
}