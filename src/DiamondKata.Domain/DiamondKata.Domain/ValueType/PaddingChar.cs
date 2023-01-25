using DiamondKata.Domain.Exception;
using LanguageExt;

namespace DiamondKata.Domain.ValueType;

internal sealed class PaddingChar : NewType<PaddingChar, char>
{
    public PaddingChar(char value)
        : base(value)
    {
        if (char.IsControl(value))
            throw new CharIsNotAWrittenSymbolException();
    }
}