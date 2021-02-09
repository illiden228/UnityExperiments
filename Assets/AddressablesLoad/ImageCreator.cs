using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class ImageCreator : MonoBehaviour
{
    [SerializeField] private List<AssetReferenceSprite> loadableSprite;

    public IEnumerator Get(System.Action<Item[]> callback)
    {
        while(true)
        {
            var result = new Item[3];

            for (int i = 0; i < result.Length; i++)
            {
                var res = loadableSprite[Random.Range(0, loadableSprite.Count)];
                result[i] = new Item(res, Random.Range(0, 100) + " - " + i);
            }

            callback(result);
            Debug.Log("server yes");
            yield return new WaitForSeconds(3f);
        }
    }
}
