using System;
using Components;
using Events;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace Handlers
{
    [UsedImplicitly]
    public class AttackResolveHandler : IInitializable, IDisposable
    {
        private EventBus _eventBus;

        [Inject]
        public void Construct(EventBus eventBus)
        {
            _eventBus = eventBus;
        }
        
        public void Initialize()
        {
            _eventBus.Subscribe<AttackResolveEvent>(OnAttackResolve);
        }

        public void Dispose()
        {
            _eventBus.Unsubscribe<AttackResolveEvent>(OnAttackResolve);
        }

        private void OnAttackResolve(AttackResolveEvent evt)
        {
            if (!evt.TargetEntity.TryGetComponent(out DamageComponent damageComponent1) || !evt.AttackerEntity.TryGetComponent(out DamageComponent damageComponent2))
                return;
            _eventBus.RaiseEvent(new DealDamageEvent(evt.TargetEntity, damageComponent2.damage));
            _eventBus.RaiseEvent(new DealDamageEvent(evt.AttackerEntity, damageComponent1.damage));
        }
    }
}