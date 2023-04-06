using System;
using System.Collections.Generic;

namespace TRTS
{
    public class GameEventManager : IGameEventManager
    {
        private readonly Dictionary<Type, object> _eventTable = new ();

        public void Register<TEvent>(Action<TEvent> callback) where TEvent : class
        {
            Type type = typeof(TEvent);
            if (!_eventTable.TryGetValue(type, out object eventCallbackObject) ||
                eventCallbackObject is not Action<TEvent> eventCallback)
            {
                eventCallback = callback;
            }
            else
            {
                eventCallback += callback;
            }

            _eventTable[type] = eventCallback;
        }

        public void Unregister<TEvent>(Action<TEvent> callback) where TEvent : class
        {
            Type type = typeof(TEvent);
            if (!_eventTable.TryGetValue(type, out object eventCallbackObject) ||
                eventCallbackObject is not Action<TEvent> eventCallback)
            {
                return;
            }

            eventCallback -= callback;
            _eventTable[type] = eventCallback;
        }

        public void Fire<TEvent>(TEvent eventData)
        {
            Type type = typeof(TEvent);
            if (!_eventTable.TryGetValue(type, out object eventCallbackObject) ||
                eventCallbackObject is not Action<TEvent> eventCallback)
            {
                return;
            }
            
            eventCallback.Invoke(eventData);
        }
    }
}