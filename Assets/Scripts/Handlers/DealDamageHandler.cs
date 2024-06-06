using System;
using Components;
using Events;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace Handlers
{
    [UsedImplicitly]
    public class DealDamageHandler: IInitializable, IDisposable
    {
        private EventBus _eventBus;

        [Inject]
        public void Construct(EventBus eventBus)
        {
            _eventBus = eventBus;
        }

        void IInitializable.Initialize()
        {
            _eventBus.Subscribe<DealDamageEvent>(OnDealDamage);
        }

        void IDisposable.Dispose()
        {
            _eventBus.Unsubscribe<DealDamageEvent>(OnDealDamage);
        }

        private void OnDealDamage(DealDamageEvent evt)
        {
            if (!evt.TargetEntity.TryGetComponent(out HealthComponent healthComponent))
                return;
            
            healthComponent.health -= evt.Damage;
            
            Debug.Log(healthComponent.health);
            
            if (healthComponent.health<=0)
                _eventBus.RaiseEvent(new DestroyEvent(evt.TargetEntity));
        }
    }
}