using GameEngine;

namespace Events
{
    public struct DestroyEvent
    {
        public readonly HeroEntity HeroEntity;

        public DestroyEvent(HeroEntity heroEntity)
        {
            HeroEntity = heroEntity;
        }
    }
}