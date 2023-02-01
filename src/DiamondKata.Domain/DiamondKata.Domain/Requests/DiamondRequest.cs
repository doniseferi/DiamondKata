using DiamondKata.DomainService.ValueType;

namespace DiamondKata.DomainService.Requests;

class DiamondRequest
{
    public DiamondRequest(
        EnglishChar inputChar)
    {
        RequestChar = inputChar ?? throw new ArgumentNullException(nameof(inputChar));
    }

    public EnglishChar RequestChar { get; }
}