using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    [SerializeField] private Text _index;
    [SerializeField] private Text _number;
    [SerializeField] private Image _back;

    public event UnityAction Deactivated;

    public void Init(ItemData item, int index)
    {
        _index.text = index.ToString();
        _number.text = item.Number.ToString();
        _back.color = item.Color;
    }
}
