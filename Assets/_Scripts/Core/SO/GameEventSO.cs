using System;
using UnityEngine;

namespace Game._Scripts.Core.SO
{
    [CreateAssetMenu(fileName = "GameEvent", menuName = "SO/events/game event", order = 0)]
    public class GameEventSO : ScriptableObject
    {
        private event Action _OnInvoke;

        public void Raise()
        {
            _OnInvoke?.Invoke();
        }

        public void Subscribe(Action callback)
        {
            _OnInvoke += callback;
        }
        
        public void Unsubscribe(Action callback)
        {
            _OnInvoke -= callback;
        }
    }
}