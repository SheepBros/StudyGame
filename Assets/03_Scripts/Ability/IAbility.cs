using System;
using SB;
using TRTS.Unit;

namespace TRTS.Ability
{
    public interface IAbility : IUpdatable
    {
        IUnit Owner { get; }
        
        bool IsAvailable();
        
        void Use();
    }
}