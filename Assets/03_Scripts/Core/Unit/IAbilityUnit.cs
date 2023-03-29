using System.Collections.Generic;
using JetBrains.Annotations;
using TRTS.Ability;

namespace TRTS.Unit
{
    public interface IAbilityUnit
    {
        List<IAbility> Abilities { get; }

        [CanBeNull]
        TAbility GetAbility<TAbility>() where TAbility : class, IAbility;
    }
}