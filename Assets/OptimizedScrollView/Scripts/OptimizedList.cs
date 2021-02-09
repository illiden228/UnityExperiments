using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class OptimizedList : PoolObject
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private RectTransform _content;
    [SerializeField] private ScrollRect _scroll;
    [SerializeField] private GridLayoutGroup _grid;
    [SerializeField] private int _itemCount;
    [SerializeField] private int _bufferSize;
    private List<ItemData> _items = new List<ItemData>();

    private int _currentMinIndex;
    private int _currentMaxIndex;
    private float _previousDragDetection = 0;

    private int _topItemOutOfViewCount { get { return Mathf.CeilToInt(_content.anchoredPosition.y / _grid.cellSize.y); } }
    private int _targetVisibleItemCount { get { return Mathf.Max(Mathf.CeilToInt(_scroll.viewport.rect.height / _grid.cellSize.y), 0); } }
    

    private void Start()
    {
        CreateList();
        Init(_prefab);
        _scroll.onValueChanged.AddListener(OnDragDirectionPositionChange);
        _scroll.content.sizeDelta = new Vector2(_scroll.content.sizeDelta.x, _items.Count * _grid.cellSize.y);
        SetStartItems();
    }

    private void OnDragDirectionPositionChange(Vector2 dragNormalizedPosition)
    {
        float dragDelta = _scroll.content.anchoredPosition.y - _previousDragDetection;
        _content.anchoredPosition = new Vector2(_content.anchoredPosition.x, _content.anchoredPosition.y + dragDelta);
        UpdateContent();
        _previousDragDetection = _scroll.content.anchoredPosition.y;
    }

    private void CreateList()
    {
        for (int i = 0; i < _itemCount; i++)
        {
            var newItem = new ItemData();
            _items.Add(newItem);
        }
    }

    private void SetStartItems()
    {
        
        for (int i = 0; i < _targetVisibleItemCount + _bufferSize; i++)
        {
            if (TryGetObject(out GameObject newItem))
            {
                newItem.transform.SetParent(_content);
                newItem.SetActive(true);
                newItem.GetComponent<ItemUI>().Init(_items[_currentMaxIndex], _currentMaxIndex);
                _currentMaxIndex++;
            }
            else break;
        }
    }

    private void UpdateContent()
    {
        if(_topItemOutOfViewCount > _bufferSize)
        {
            if (_currentMaxIndex >= _items.Count) return;

            Transform firstChild = _content.GetChild(0);
            firstChild.SetSiblingIndex(_content.childCount - 1);
            firstChild.gameObject.GetComponent<ItemUI>().Init(_items[_currentMaxIndex], _currentMaxIndex);
            _content.anchoredPosition = new Vector2(_content.anchoredPosition.x, _content.anchoredPosition.y - _grid.cellSize.y);
            _currentMaxIndex++;
            _currentMinIndex++;
        }
        else if(_topItemOutOfViewCount < _bufferSize)
        {
            if (_currentMinIndex <= 0) return;

            Transform lastChild = _content.GetChild(_content.childCount - 1);
            lastChild.SetSiblingIndex(0);
            _currentMaxIndex--;
            _currentMinIndex--;
            lastChild.gameObject.GetComponent<ItemUI>().Init(_items[_currentMinIndex], _currentMinIndex);
            _content.anchoredPosition = new Vector2(_content.anchoredPosition.x, _content.anchoredPosition.y + _grid.cellSize.y);
        }
    }
}
