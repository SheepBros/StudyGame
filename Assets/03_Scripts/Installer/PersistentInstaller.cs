using SB;

namespace TRTS
{
    public class PersistentInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            _container.BindAllInterfaces<GameEventManager>();
        }
    }
}