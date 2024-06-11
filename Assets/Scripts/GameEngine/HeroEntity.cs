using System.Collections.Generic;
using Components;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace GameEngine
{
    public class HeroEntity : SerializedMonoBehaviour
    {
        [OdinSerialize]
        private List<IComponent> _components = new();

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