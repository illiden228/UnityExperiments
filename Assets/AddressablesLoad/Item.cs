using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class Item
{
    public AssetReferenceSprite Icon;
    public string Label;

    public Item(AssetReferenceSprite icon, string text)
    {
        Icon = icon;
        Label = text;
    }
}
