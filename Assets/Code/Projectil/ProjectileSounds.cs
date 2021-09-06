using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSounds : MonoBehaviour
{
    [SerializeField]
    private SoundClip _startClip;
    [SerializeField]
    private SoundClip _endClip;

    private bool _first = true;
    
    private void OnEnable()
    {
        if (_first == false && _startClip.Clip != null)
            AudioSource.PlayClipAtPoint(_startClip.Clip, transform.position, _startClip.Volume);
    }
    private void OnDisable()
    {
        if (_first == false && _endClip.Clip != null)
            AudioSource.PlayClipAtPoint(_endClip.Clip, transform.position, _endClip.Volume);

        _first = false;
    }
}
[Serializable]
public class SoundClip
{
    [SerializeField]
    private AudioClip _clip;
    [SerializeField]
    private float _volume;
    [SerializeField]
    private bool _random;

    public AudioClip Clip => _clip;
    public float Volume => _volume;
}
