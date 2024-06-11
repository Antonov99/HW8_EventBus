using GameEngine;
using UI;

namespace Events
{
    public struct AttackEvent
    {
        public readonly HeroEntity TargetEntity;
        public readonly HeroView AttackerView;

        public AttackEvent(HeroEntity targetEntity, HeroView attackerView)
        {
            TargetEntity = targetEntity;
            AttackerView = attackerView;
        }
    }
}