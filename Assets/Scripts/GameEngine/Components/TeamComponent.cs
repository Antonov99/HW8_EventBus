using System;
using UnityEngine;

namespace Components
{
    [Serializable]
    public struct TeamComponent:IComponent
    {
        [SerializeField]
        public bool team;
    }
}