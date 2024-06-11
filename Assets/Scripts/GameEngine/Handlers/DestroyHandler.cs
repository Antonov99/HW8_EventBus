using System;
using Events;
using GameEngine;
using JetBrains.Annotations;
using Zenject;

namespace Handlers
{
    [UsedImplicitly]
    public class DestroyHandler: IInitializable, IDisposable
    {
        private EventBus _eventBus;
        private QueueManager _queueManager;

        [Inject]
        public void Construct(EventBus eventBus, QueueManager queueManager)
        {
            _eventBus = eventBus;
            _queueManager = queueManager;
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
            _queueManager.OnDead(evt.HeroEntity);
        }
    }
}