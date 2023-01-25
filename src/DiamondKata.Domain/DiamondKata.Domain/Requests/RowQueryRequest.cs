using DiamondKata.Domain.ValueType;

namespace DiamondKata.DomainService.Requests;

class RowQueryRequest
{
    public RowQueryRequest(
        EnglishChar inputChar,
        EnglishChar currentRowsChar,
        PaddingChar outerPaddingChar,
        PaddingChar innerPaddingChar)
    {
        RequestChar = inputChar ?? throw new ArgumentNullException(nameof(inputChar));
        CurrentRowsChar = currentRowsChar ?? throw new ArgumentNullException(nameof(currentRowsChar));
        OuterPaddingChar = outerPaddingChar ?? throw new ArgumentNullException(nameof(outerPaddingChar));
        InnerPaddingChar = innerPaddingChar ?? throw new ArgumentNullException(nameof(innerPaddingChar));
    }

    public EnglishChar RequestChar { get; }
    public EnglishChar CurrentRowsChar { get; }
    public PaddingChar OuterPaddingChar { get; }
    public PaddingChar InnerPaddingChar { get; }
}