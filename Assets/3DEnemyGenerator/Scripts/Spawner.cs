using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float _delay;
    [SerializeField] private Player _player;
    [SerializeField] private GameObject _enemyExample;
    [SerializeField] private Text _timeText;

    private float _lastSpawn;
    private Transform[] _spawnPoints;
    
    void Start()
    {
        _spawnPoints = GetComponentsInChildren<Transform>();
    }

    void Update()
    {
        _timeText.text = Mathf.Ceil(Time.time).ToString();
        if (_lastSpawn >= _delay)
        {
            foreach(Transform spawnDot in _spawnPoints)
            {
                var enemy = Instantiate(_enemyExample, spawnDot).GetComponent<Enemy>();
                enemy.SetTarget(_player);
            }
            _lastSpawn = 0;
        }
        _lastSpawn += Time.deltaTime;
    }
}
