using TMPro;
using UnityEngine;

public class UIPlayerInGame : TargetableInGameUI, IEventObserver
{
    [SerializeField]
    private CanvasGroup _canvasGroup;
    [SerializeField]
    private TextMeshProUGUI _shipSpeedText;

    protected override  void Awake()
    {
        base.Awake();
        ServiceLocator.Instance.GetService<IEventQueue>().Subscribe(EventIds.PlayerShipSpawned, this);
    }
    private void OnDestroy()
    {
        ServiceLocator.Instance.GetService<IEventQueue>().Unsubscribe(EventIds.PlayerShipSpawned, this);
    }
    public void Player_OnChangedShip(IShip playerShip)
    {
        _healthBar.Configure(playerShip.Health, this);
        _shipSpeedText.SetText(playerShip.CurrentSpeed.ToString());
    }
    public override void Disable()
    {
        _canvasGroup.HideCanvasGroup();
    }

    public void Process(EventData eventData)
    {
        if (eventData.EventId != EventIds.PlayerShipSpawned)
            return;

        Player_OnChangedShip( ((PlayerShipSpawnedEventData)eventData).PlayerShip);
    }
}