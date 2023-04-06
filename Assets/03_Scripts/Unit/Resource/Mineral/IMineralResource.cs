using SB.Util;

namespace TRTS.Unit
{
    public interface IMineralResource
    {
        IntReactiveProperty Amount { get; }

        int Mining(int amount);
    }
}