using DefaultNamespace;

namespace Events
{
    public struct DealDamageEvent
    {
        public readonly HeroEntity TargetEntity;
        public readonly int Damage;

        public DealDamageEvent(HeroEntity targetEntity, int damage)
        {
            TargetEntity = targetEntity;
            Damage = damage;
        }
    }
}