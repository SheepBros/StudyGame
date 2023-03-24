namespace TRTS.Unit
{
    public interface IMineralResource
    {
        int Amount { get; }

        int Mining(int amount);
    }
}