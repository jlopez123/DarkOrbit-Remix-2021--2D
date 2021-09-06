public interface IEventQueue
{
    void Subscribe(EventIds eventId, IEventObserver observer);

    void Unsubscribe(EventIds eventId, IEventObserver observer);

    void EnqueueEvent(EventData eventData);

}
