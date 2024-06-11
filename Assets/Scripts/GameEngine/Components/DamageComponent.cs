using System;
using UnityEngine;

namespace Components
{
    [Serializable]
    public class DamageComponent:IComponent
    {
        [SerializeField]
        public int damage;
    }
}