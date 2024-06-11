using System;
using System.Collections.Generic;
using Components;
using Sirenix.OdinInspector;
using UI;
using UnityEngine;
using Zenject;

namespace GameEngine
{
    [Serializable]
    public sealed class QueueManager
    {
        private IReadOnlyList<HeroView> _redTeam;
        private IReadOnlyList<HeroView> _blueTeam;

        [ShowInInspector]
        private List<HeroView> _redTeamList;
        [ShowInInspector]
        private List<HeroView> _blueTeamList;
        
        private bool _currentTeamRed=true;

        [Inject]
        public void Construct(UIService uiService)
        {
            _redTeam = uiService.GetRedPlayer().GetViews();
            _blueTeam=uiService.GetBluePlayer().GetViews();

            _redTeamList = new List<HeroView>(_redTeam);
            _blueTeamList = new List<HeroView>(_blueTeam);
        }
        
        public HeroView CalculateCurrentHero()
        {
            if (_currentTeamRed)
                return _redTeamList[0];
            else
                return _blueTeamList[0];
        }

        public void SwitchTurn()
        {
            if (_currentTeamRed && _redTeamList.Count>1)
            {
                var firstHero = _redTeamList[0];
                _redTeamList = _redTeamList.GetRange(1, _redTeamList.Count - 1);
                _redTeamList.Add(firstHero);
            }
            else if(!_currentTeamRed && _blueTeamList.Count>1)
            {
                var firstHero = _blueTeamList[0];
                _blueTeamList = _blueTeamList.GetRange(1, _blueTeamList.Count - 1);
                _blueTeamList.Add(firstHero);
            }

            _currentTeamRed = !_currentTeamRed;
        }

        public void OnDead(HeroEntity dead)
        {
            if (!dead.TryGetComponent(out TeamComponent teamComponent))
                return;

            var heroView = dead.GetComponent<HeroView>();
            
            if (teamComponent.team)
            {
                _blueTeamList.Remove(heroView);
                if (_blueTeamList.Count==0)
                    Debug.Log("Победа Красных");
            }
            else
            {
                _redTeamList.Remove(heroView);
                if (_blueTeamList.Count==0)
                    Debug.Log("Победа Синих");
            }
        }
    }
}