using UnityEngine;

public class UIBattleLogsManager : MonoBehaviour, IEventObserver
{
    [SerializeField]
    private UITextLogsManager _textLogs;
    //[SerializeField]
    //private UIVisualLogsManger _visualLogs;   

    private IEventQueue _eventQueue;

    private void Start()
    {
        _eventQueue = ServiceLocator.Instance.GetService<IEventQueue>();


        _eventQueue.Subscribe(EventIds.PlayerShipSpawned, this);
        _eventQueue.Subscribe(EventIds.PlayerShipOutOfRange, this);
        // Subscribirse a : ShipDestroyed(si el player lo destruye)  Mostar Rewards  - Nivel cargado(nuevo mapa) -  Recoger recursos/boxes - etc
    }

    private void OnDestroy()
    {
        _eventQueue.Unsubscribe(EventIds.PlayerShipSpawned, this);
        _eventQueue.Unsubscribe(EventIds.PlayerShipOutOfRange, this);
    }

    private void SendOutOfRangeLog()
    {
        //_textLogs.AddLog(log);
    }

    private void SendPlayerShipSpawnedLog()
    {
        //_textLogs.AddLog(log);
    }
    public void Process(EventData eventData)
    {
        if (eventData.EventId != EventIds.PlayerShipSpawned && eventData.EventId != EventIds.PlayerShipOutOfRange)
            return;

        if (eventData.EventId == EventIds.PlayerShipSpawned)
        {
            SendPlayerShipSpawnedLog();
            return;
        }
        if(eventData.EventId == EventIds.PlayerShipOutOfRange)
        {
            SendOutOfRangeLog();
            return;
        }

    }
}