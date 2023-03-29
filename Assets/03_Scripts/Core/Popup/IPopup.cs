using SB;

namespace TRTS.UI
{
    public interface IPopup : IInjectable
    {
        void OnOpened(IPopupData data);

        void OnClosed();
    }
}