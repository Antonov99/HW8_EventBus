using GameEngine;

namespace Events
{
    public struct AttackResolveEvent
    {
        public readonly HeroEntity TargetEntity;
        public readonly HeroEntity AttackerEntity;

        public AttackResolveEvent(HeroEntity targetEntity, HeroEntity attackerEntity)
        {
            TargetEntity = targetEntity;
            AttackerEntity = attackerEntity;
        }
    }
}