using TRTS.Unit;

namespace TRTS.Ability
{
    public class StoreResourceAbility : IAbility
    {
        public IUnit Owner { get; }
        
        public float StorableLength { get; private set; }

        private IGameManager _gameManager;

        public StoreResourceAbility(IGameManager gameManager, float storableLength)
        {
            _gameManager = gameManager;
            StorableLength = storableLength;
        }

        public bool IsAvailable()
        {
            return true;
        }

        public void Update()
        {
        }

        public void StoreMineral(int amount)
        {
            if (amount > 0)
            {
                _gameManager.Minerals.Value += amount;
            }
        }
    }
}