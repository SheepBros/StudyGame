using SB;
using UnityEngine;

namespace TRTS
{
    public class GameContext : MonoBehaviour, IInjectable
    {
        private IGameManager _gameManager;
        
        [Inject]
        public void InjectBindings(IGameManager gameManager)
        {
            _gameManager = gameManager;
        }

        private void Start()
        {
            _gameManager.Start();
        }

        private void Update()
        {
            _gameManager.Update();
        }
    }
}