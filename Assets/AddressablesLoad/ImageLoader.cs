using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class ImageLoader : MonoBehaviour
{
    [SerializeField] private ImageCreator _server;
    [SerializeField] private GameObject _prefab;
    [SerializeField] private Transform _content;
    private Dictionary<AssetReferenceSprite, AsyncOperationHandle> _sprites = new Dictionary<AssetReferenceSprite, AsyncOperationHandle>();
    private Image _currentImage;

    private void Start()
    {
        StartCoroutine(_server.Get(result => OnCallback(result)));
    }

    private void OnCallback(Item[] items)
    {
        Debug.Log("process ");
        foreach (var item in items)
        {
            var prefGO = Instantiate(_prefab, _content);

            if(item.Icon.OperationHandle.IsValid())
            {
                if(item.Icon.OperationHandle.IsDone)
                {
                    prefGO.GetComponent<Image>().sprite = item.Icon.OperationHandle.Result as Sprite;
                }
                else
                {
                    item.Icon.OperationHandle.Completed += obj => prefGO.GetComponent<Image>().sprite = obj.Result as Sprite;
                }
            }
            else
            {
                item.Icon.LoadAssetAsync().Completed += obj => prefGO.GetComponent<Image>().sprite = obj.Result;
            }
            prefGO.GetComponentInChildren<Text>().text = item.Label;
        }
    }

    private void ImageLoaded(AsyncOperationHandle<Sprite> obj, Image image)
    {
        if (obj.Status == AsyncOperationStatus.Succeeded)
        {
            image.sprite = obj.Result;
        }
        //Addressables.Release(obj);
    }

    //private IEnumerator LoadImage(Item item, Image image)
    //{
    //    Sprite sprite = null;
    //    while (sprite != null)
    //    {
    //        if (_loadedSprites.ContainsKey(item.Sprite))
    //        {
    //            sprite = _loadedSprites[item.Sprite];
    //            image.sprite = sprite;
    //        }
    //        else
    //        {
    //            Debug.Log("LoadImage");
    //            AsyncOperationHandle<Sprite> handle = item.Sprite.LoadAssetAsync<Sprite>();
    //            yield return handle;
    //            if (handle.Status == AsyncOperationStatus.Succeeded)
    //            {
    //                sprite = handle.Result;
    //                _loadedSprites.Add(item.Sprite, sprite);
    //                image.sprite = sprite;
    //            }
    //        }
    //    }
    //}

}
