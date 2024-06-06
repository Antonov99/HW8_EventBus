using System;
using Components;
using DefaultNamespace;
using Events;
using JetBrains.Annotations;
using UI;
using UnityEngine;
using Zenject;

namespace Handlers
{
    [UsedImplicitly]
    public class AttackHandler : IInitializable, IDisposable
    {
        private EventBus _eventBus;

        [Inject]
        public void Construct(EventBus eventBus)
        {
            _eventBus = eventBus;
        }

        void IInitializable.Initialize()
        {
            _eventBus.Subscribe<AttackEvent>(OnAttack);
        }

        void IDisposable.Dispose()
        {
            _eventBus.Unsubscribe<AttackEvent>(OnAttack);
        }

        private void OnAttack(AttackEvent evt)
        {
            var targetEntity = evt.TargetEntity;
            var attackerEntity = evt.AttackerView.GetComponent<HeroEntity>();

            if (targetEntity.TryGetComponent(out TeamComponent teamComponent1) &&
                attackerEntity.TryGetComponent(out TeamComponent teamComponent2))
            {
                if (teamComponent1.team != teamComponent2.team)
                {
                    if (
                        targetEntity.TryGetComponent(out HealthComponent healthComponent1) &&
                        attackerEntity.TryGetComponent(out HealthComponent healthComponent2) &&
                        healthComponent1.health > 0 &&
                        healthComponent2.health > 0
                    )
                    {
                        evt.AttackerView.AnimateAttack(evt.TargetEntity.GetComponent<HeroView>());
                        _eventBus.RaiseEvent(new AttackResolveEvent(targetEntity, attackerEntity));
                    }
                    else
                    {
                        Debug.Log("You cant attack dead unit");
                    }
                }
                else
                {
                    Debug.Log("You cant attack teammate");
                }
            }
        }
    }
}