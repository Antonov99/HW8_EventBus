using System.Collections.Generic;
using Events;
using Sirenix.OdinInspector;
using UI;
using UnityEngine;
using Zenject;

namespace DefaultNamespace
{
    public class GameController:MonoBehaviour
    {
        private EventBus _eventBus;
        
        [ShowInInspector]
        private IReadOnlyList<HeroView> _blueTeam;
        
        [ShowInInspector]
        private IReadOnlyList<HeroView> _redTeam;

        [SerializeField]
        private HeroEntity target;

        [SerializeField]
        private UIService uiService;

        private int _currentStep=0;
        private string _currentTeam="red";

        [Inject]
        public void Construct(EventBus eventBus)
        {
            _eventBus = eventBus;
        }

        private void Start()
        {
            _blueTeam=uiService.GetBluePlayer().GetViews();
            _redTeam = uiService.GetRedPlayer().GetViews();
        }

        [Button]
        private void Attack()
        {
            if (_currentTeam=="red")
                _eventBus.RaiseEvent(new AttackEvent(target,_redTeam[_currentStep]));
        }
    }
}