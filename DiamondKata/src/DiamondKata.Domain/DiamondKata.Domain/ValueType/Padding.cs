using DiamondKata.Domain.Exception;
using LanguageExt;

namespace DiamondKata.Domain.ValueType;

internal sealed class Padding : NewType<Padding, char>
{
    public Padding(char value)
        : base(value)
    {
        if (char.IsControl(value))
            throw new CharIsNotAWrittenSymbolException();
    }
}