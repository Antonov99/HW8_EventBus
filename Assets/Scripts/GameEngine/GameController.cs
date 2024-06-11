using Components;
using Events;
using Sirenix.OdinInspector;
using UI;
using UnityEngine;
using Zenject;

namespace GameEngine
{
    public class GameController : MonoBehaviour
    {
        private EventBus _eventBus;

        [SerializeField]
        private HeroEntity target;

        [SerializeReference]
        private QueueManager _queueManager;

        [Inject]
        public void Construct(EventBus eventBus, QueueManager queueManager)
        {
            _eventBus = eventBus;
            _queueManager = queueManager;
        }

        [Button]
        private void Attack()
        {
            if (target is null)
                return;
            
            var attackerView = _queueManager.CalculateCurrentHero();
            var attackerEntity = attackerView.GetComponent<HeroEntity>();
            
            if (target.TryGetComponent(out TeamComponent teamComponent1) &&
                attackerEntity.TryGetComponent(out TeamComponent teamComponent2))
            {
                if (teamComponent1.team != teamComponent2.team)
                {
                    _eventBus.RaiseEvent(new AttackEvent(target, attackerView));
                    _queueManager.SwitchTurn();
                    attackerView.SetActive(false);
                    _queueManager.CalculateCurrentHero().SetActive(true);
                }
                else
                {
                    Debug.Log("You cant attack teammate");
                }
            }
        }
    }
}