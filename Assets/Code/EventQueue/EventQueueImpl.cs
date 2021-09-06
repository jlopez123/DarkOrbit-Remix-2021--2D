using System.Collections.Generic;
using UnityEngine;

public class EventQueueImpl : MonoBehaviour, IEventQueue
{
    private Queue<EventData> _currentEvents;
    private Queue<EventData> _nextEvents;

    private Dictionary<EventIds, List<IEventObserver>> _observers;
    private void Awake()
    {
        _currentEvents = new Queue<EventData>();
        _nextEvents = new Queue<EventData>();

        _observers = new Dictionary<EventIds, List<IEventObserver>>();
    }
    public void Subscribe(EventIds eventId, IEventObserver observer)
    {

        if (_observers.TryGetValue(eventId, out var eventObservers) == false)
        {
            eventObservers = new List<IEventObserver>();
        }
        eventObservers.Add(observer);
        _observers[eventId] = eventObservers;
    }
    public void Unsubscribe(EventIds eventId, IEventObserver observer)
    {
        if (_observers.ContainsKey(eventId))
            _observers[eventId].Remove(observer);
    }
    public void EnqueueEvent(EventData eventData)
    {
        _nextEvents.Enqueue(eventData);
    }
    private void LateUpdate()
    {
        ProcessEvents();
    }
    private void ProcessEvents()
    {
        var tempCurrentEvents = _currentEvents;
        _currentEvents = _nextEvents;
        _nextEvents = new Queue<EventData>();

        foreach (var currentEvent in _currentEvents)
        {
            ProcessEvent(currentEvent);
        }
        _currentEvents.Clear();
    }
    private void ProcessEvent(EventData eventData)
    {
        if (_observers.ContainsKey(eventData.EventId) == false)
            return;

        foreach (var observer in _observers[eventData.EventId])
        {
            observer.Process(eventData);
        }
    }
}
