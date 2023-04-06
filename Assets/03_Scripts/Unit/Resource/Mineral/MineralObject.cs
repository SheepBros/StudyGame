using TMPro;
using UnityEngine;

namespace TRTS.Unit
{
    public class MineralObject : UnitObject
    {
        [SerializeField]
        private TextMeshProUGUI _amountText;
        
        private MineralUnit _mineralUnit;

        private void OnDestroy()
        {
            if (_mineralUnit != null)
            {
                _mineralUnit.Amount.OnValueChanged -= OnMineralAmountChanged;
            }
        }

        public override void SetUp(IUnit unit)
        {
            base.SetUp(unit);
            
            _mineralUnit = unit as MineralUnit;
            
            if (_mineralUnit != null)
            {
                _mineralUnit.SetObject(this);
                _mineralUnit.Amount.OnValueChanged += OnMineralAmountChanged;
                OnMineralAmountChanged(_mineralUnit.Amount.Value);
            }
        }

        private void OnMineralAmountChanged(int amount)
        {
            _amountText.text = amount.ToString();
        }
    }
}