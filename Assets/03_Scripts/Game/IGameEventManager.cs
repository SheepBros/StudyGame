using System;

namespace TRTS
{
    public interface IGameEventManager
    {
        void Register<TEvent>(Action<TEvent> callback) where TEvent : class;

        void Unregister<TEvent>(Action<TEvent> callback) where TEvent : class;

        void Fire<TEvent>(TEvent eventData);
    }
}