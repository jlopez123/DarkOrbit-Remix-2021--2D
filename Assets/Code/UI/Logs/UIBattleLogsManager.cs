using UnityEngine;

public class UIBattleLogsManager : MonoBehaviour, IEventObserver
{
    [SerializeField]
    private UITextLogsManager _textLogs;
    //[SerializeField]
    //private UIVisualLogsManger _visualLogs;   

    private IEventQueue _eventQueue;
    private RewardsConfiguration _rewardsConfiguration;
    private void Start()
    {
        _eventQueue = ServiceLocator.Instance.GetService<IEventQueue>();
        _rewardsConfiguration = ServiceLocator.Instance.GetService<ExtraConfigurationsService>().RewardsConfiguration;

        _eventQueue.Subscribe(EventIds.PlayerShipSpawned, this);
        _eventQueue.Subscribe(EventIds.PlayerShipOutOfRange, this);
        _eventQueue.Subscribe(EventIds.NpcDestroyed, this);
        // Recoger recursos/boxes - etc
    }

    private void OnDestroy()
    {
        _eventQueue.Unsubscribe(EventIds.PlayerShipSpawned, this);
        _eventQueue.Unsubscribe(EventIds.PlayerShipOutOfRange, this);
        _eventQueue.Unsubscribe(EventIds.NpcDestroyed, this);
    }
    private void SendRewardsTextLog(ITargetable npc)
    {
        var rewards = _rewardsConfiguration.GetRewards(npc.Id);
        _textLogs.AddLog(new LogTextData($"Has destruido un alienígena del tipo {npc.TargetInfo.Name}.", Color.white, 3f));

        _textLogs.AddLog(new LogTextData($"Has obtenido {rewards.Exp} puntos de Experiencia", Color.white,  3f));
        _textLogs.AddLog(new LogTextData($"Has obtenido {rewards.Honor} puntos de Honor", Color.white, 3f));
        _textLogs.AddLog(new LogTextData($"Has obtenido {rewards.Credits} Creditos", Color.white, 3f));
        _textLogs.AddLog(new LogTextData($"Has obtenido {rewards.Uridium} Uridium", Color.white, 3f));
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
        if (eventData.EventId != EventIds.PlayerShipSpawned && eventData.EventId != EventIds.PlayerShipOutOfRange && eventData.EventId != EventIds.NpcDestroyed)
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
        if(eventData.EventId == EventIds.NpcDestroyed)
        {
            var npcId = ((NpcDestroyedEventData)eventData).Ship;
            SendRewardsTextLog(npcId);
            return;
        }

    }
}
