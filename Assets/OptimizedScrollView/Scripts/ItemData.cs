using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData
{
    public Color Color;
    public int Number;
    private static int _id = 0;

    public ItemData()
    {
        Color = new Color();
        Color.r = Random.Range(0f, 1f); 
        Color.g = Random.Range(0f, 1f); 
        Color.b = Random.Range(0f, 1f);
        Color.a = 1f;
        Number = ++_id;
    }
}
