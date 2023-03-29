namespace TRTS.UI
{
    /// <summary>
    /// It's called by UIContentHandler.
    /// NOTE: The class inherits this must be a monobehaviour class.
    /// A game object of the component class inheriting this interface must have a UIContent.
    /// </summary>
    public interface IUIContentLifeCycle
    {
        void ViewAwake();

        void ViewDestroy();
    }
}