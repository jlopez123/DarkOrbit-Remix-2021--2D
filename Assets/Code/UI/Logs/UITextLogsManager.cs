using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class UITextLogsManager : MonoBehaviour
{
    [SerializeField]
    private PooledMonoBehaviour _prefab;
    [SerializeField]
    private Transform _container;
    [SerializeField]
    private int _maxLogsAtSameTime = 10;

    private float _delay = .3f;

    private Queue<LogTextData> _logsToShow = new Queue<LogTextData>();

    private float _timer;
    public async void AddLog(LogTextData log)
    {
        await Task.Delay(100);
        _logsToShow.Enqueue(log);
    }
    private void Update()
    {
        _timer += Time.unscaledDeltaTime;

        if (_timer < _delay)
            return;

        SpawnLog();
    }
    private void SpawnLog()
    {
        if (_container.childCount > _maxLogsAtSameTime)
            return;

        if (_logsToShow.Count <= 0)
            return;


        var newLog = _prefab.Get<PooledMonoBehaviour>();
        newLog.transform.SetParent(_container, false);
        // cambiar a pool
        //var newLog = Instantiate(_prefab, _container);
        //newLog.Configure(_logsToShow.Dequeue());
        _timer = 0f;
    }
}
