using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Signalisation : MonoBehaviour
{
    [SerializeField] private SignalPlace _enter;
    [SerializeField] private SignalPlace _exit;
    [SerializeField] private float _audioSense;
    
    private AudioSource _signal;

    void Start()
    {
        _signal = GetComponent<AudioSource>();
        _signal.volume = 0f;
        _enter.Init(this);
        _enter.Enter += SignalPlay;
        _exit.Init(this);
        _exit.Enter += SignalStop;
    }

    private void SignalPlay()
    {
        StopAllCoroutines();
        StartCoroutine(Volume(1f));
    }
    
    private void SignalStop()
    {
        StopAllCoroutines();
        StartCoroutine(Volume(0f));
    }

    private IEnumerator Volume(float volume)
    {
        Debug.Log("St");
        while (_signal.volume != volume)
        {
            Debug.Log(_signal.volume);
            _signal.volume = Mathf.MoveTowards(_signal.volume, volume, 1 / _audioSense * Time.deltaTime);
            Debug.Log(_signal.volume);
            yield return null;
        }
        Debug.Log("End");
    }
}
