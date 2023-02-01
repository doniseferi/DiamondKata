using DiamondKata.DomainService.ValueType;

namespace DiamondKata.DomainService.Requests;

class RowQueryRequest
{
    public RowQueryRequest(
        EnglishChar inputChar,
        EnglishChar currentRowsChar)
    {
        RequestChar = inputChar ?? throw new ArgumentNullException(nameof(inputChar));
        CurrentRowsChar = currentRowsChar ?? throw new ArgumentNullException(nameof(currentRowsChar));
    }

    public EnglishChar RequestChar { get; }
    public EnglishChar CurrentRowsChar { get; }
}