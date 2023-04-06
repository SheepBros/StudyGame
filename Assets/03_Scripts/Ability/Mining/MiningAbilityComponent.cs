using TRTS.Unit;
using UnityEngine;

namespace TRTS.Ability
{
    public class MiningAbilityComponent : MonoBehaviour, IAbilityComponent
    {
        private MiningAbility _ability;

        private UnitObject _unitObject;

        private ProgressBarUI _progressBarUI;

        private void Update()
        {
            if (_ability == null)
            {
                return;
            }
            
            _ability.Update();

            if (_progressBarUI != null)
            {
                if (_ability.IsMining.Value && !_progressBarUI.IsActive())
                {
                    _progressBarUI.SetEnable(true);
                }
                else if (!_ability.IsMining.Value && _progressBarUI.IsActive())
                {
                    _progressBarUI.SetEnable(false);
                }
                
                _progressBarUI.SetProgress(_ability.CurrentMiningTime / _ability.MiningTime);
            }
        }
        
        public bool SetUp(UnitObject unitObject, IAbility ability)
        {
            _unitObject = unitObject;
            _ability = ability as MiningAbility;

            _progressBarUI = unitObject.GetCachedComponent<ProgressBarUI>(true);
            
            return _ability != null;
        }
    }
}