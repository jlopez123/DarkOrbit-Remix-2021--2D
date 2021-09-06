using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

//  temporal...
public class UIOutOfRangeIndicator : MonoBehaviour, IEventObserver
{
    [SerializeField]
    private Image _outRangeImage;
    [SerializeField]
    private AudioClip _outOfRangeSound;

    private IShip _myShip;

    private bool _currentInRange = true;
    private IEnumerator _coroutine;
    private void Awake()
    {
        _outRangeImage.enabled = false;
        ServiceLocator.Instance.GetService<IEventQueue>().Subscribe(EventIds.PlayerShipSpawned, this);
    }
    private void OnDestroy()
    {
        ServiceLocator.Instance.GetService<IEventQueue>().Unsubscribe(EventIds.PlayerShipSpawned, this);
    }

    public void Process(EventData eventData)
    {
        if (eventData.EventId != EventIds.PlayerShipSpawned)
            return;

        _myShip = ((PlayerShipSpawnedEventData)eventData).PlayerShip;
    }
    //
    private void Update()
    {
        if (_myShip == null)
            return;
        if (_myShip.CurrentTarget == null)
            return;

        //Debug.LogError(": " + _myShip.Weapons.IsAttacking);
        var inRange = _myShip.Weapons.MainModule.WeaponsOnRange();

        if (_myShip.Weapons.IsAttacking == false)
        {
            Cancel();
            return;
        }

        if (_currentInRange != inRange)
        {
            InRangeChanged(inRange);
        }
        _currentInRange = inRange;
    }

    private void Cancel()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _outRangeImage.enabled = false;
    }

    private void InRangeChanged(bool inRange)
    {

        if (inRange == false)
        {
            // pasar eso al mismo ship del player y recibir este evento en este script para mostrar la imagen ....
            ServiceLocator.Instance.GetService<IEventQueue>().EnqueueEvent(new EventData(EventIds.PlayerShipOutOfRange));

            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _coroutine = OutOfRangeCoroutine();
            StartCoroutine(_coroutine);
        }
        else
        {
            Cancel();
        }
    }
    private   void ShowImage()
    {
        _outRangeImage.enabled = true;
        var angle = Utils.AngleBetweenVectors(_myShip.CurrentTarget.transform.position, _myShip.transform.position);

        // cambiar a convertir posicion del player a screen pooints(si esta en ScrenSpace Overlay) + offset(en la direccion)
        _outRangeImage.transform.rotation = Quaternion.AngleAxis(90 - angle, Vector3.forward);
        _outRangeImage.transform.localPosition = Vector3.zero + (_outRangeImage.transform.up * 300f);

    }
    private IEnumerator OutOfRangeCoroutine()
    {
        while(true)
        {
            for (int i = 0; i < 3; i++)
            {
                ShowImage();
                AudioSource.PlayClipAtPoint(_outOfRangeSound, Camera.main.transform.position, 1f);
                yield return new WaitForSeconds(0.3f);
            }
            yield return new WaitForSeconds(0.2f);
            _outRangeImage.enabled = false;
            yield return new WaitForSeconds(1.8f);
        }
    }
}
