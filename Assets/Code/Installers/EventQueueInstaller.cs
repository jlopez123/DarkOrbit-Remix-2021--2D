using UnityEngine;

public class EventQueueInstaller : Installer
{
    [SerializeField]
    private EventQueueImpl _eventQueueService;

    public override void Install(ServiceLocator serviceLocator)
    {
        DontDestroyOnLoad(_eventQueueService.gameObject);
        serviceLocator.RegisterService<IEventQueue>(_eventQueueService);
    }

}