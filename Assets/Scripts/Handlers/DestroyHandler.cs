using System;
using Events;
using JetBrains.Annotations;
using UI;
using UnityEngine;
using Zenject;

namespace Handlers
{
    [UsedImplicitly]
    public class DestroyHandler: IInitializable, IDisposable
    {
        private EventBus _eventBus;

        [Inject]
        public void Construct(EventBus eventBus)
        {
            _eventBus = eventBus;
        }

        void IInitializable.Initialize()
        {
            _eventBus.Subscribe<DestroyEvent>(OnDestroy);
        }

        void IDisposable.Dispose()
        {
            _eventBus.Unsubscribe<DestroyEvent>(OnDestroy);
        }
        
        private void OnDestroy(DestroyEvent evt)
        {
            evt.HeroEntity.gameObject.SetActive(false);
        }
    }
}