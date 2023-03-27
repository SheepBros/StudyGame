using SB;
using UnityEngine;

namespace TRTS
{
    public class GameContext : MonoBehaviour, IInjectable
    {
        private GameManager _gameManager;
        
        [Inject]
        public void InjectBindings(GameManager gameManager)
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