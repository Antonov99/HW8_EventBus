using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace DefaultNamespace
{
    public class HeroEntity : SerializedMonoBehaviour
    {
        [OdinSerialize]
        private List<object> _components = new();

        public new bool TryGetComponent<T>(out T component)
        {
            foreach (var obj in _components)
            {
                if (obj is T comp)
                {
                    component = comp;
                    return true;
                }
            }

            component = default(T);
            return false;
        }
    }
}