using System;
using UnityEngine;

namespace Components
{
    [Serializable]
    public class HealthComponent:IComponent
    {
        [SerializeField]
        public int health;
    }
}