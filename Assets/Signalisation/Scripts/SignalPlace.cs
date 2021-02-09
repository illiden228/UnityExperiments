using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SignalPlace : MonoBehaviour
{
    private Signalisation _signalisation;
    public event UnityAction Enter;

    public void Init(Signalisation signalisation)
    {
        _signalisation = signalisation;
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.gameObject.GetComponent<PlayerMove>();
        if (player != null)
        {
            Enter?.Invoke();
        }
    }
}
