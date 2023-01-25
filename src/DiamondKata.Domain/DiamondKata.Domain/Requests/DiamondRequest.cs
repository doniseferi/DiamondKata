using DiamondKata.Domain.ValueType;

namespace DiamondKata.DomainService.Requests;

class DiamondRequest
{
    public DiamondRequest(
        EnglishChar inputChar,
        PaddingChar outerPaddingChar,
        PaddingChar innerPaddingChar)
    {
        RequestChar = inputChar ?? throw new ArgumentNullException(nameof(inputChar));
        OuterPaddingChar = outerPaddingChar ?? throw new ArgumentNullException(nameof(outerPaddingChar));
        InnerPaddingChar = innerPaddingChar ?? throw new ArgumentNullException(nameof(innerPaddingChar));
    }

    public EnglishChar RequestChar { get; }
    public PaddingChar OuterPaddingChar { get; }
    public PaddingChar InnerPaddingChar { get; }
}