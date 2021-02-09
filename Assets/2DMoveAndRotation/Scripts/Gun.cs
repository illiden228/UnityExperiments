using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _center;
    [SerializeField] private float _shootDelay;
    private float _elapsedTime = 0;

    private void Update()
    {
        var mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        var angle = Vector2.Angle(Vector2.right, mousePosition - transform.position);
        transform.eulerAngles = new Vector3(0, 0, transform.position.y < mousePosition.y ? angle : -angle);

        if (Input.GetKey(KeyCode.Mouse0))
        {
            _elapsedTime -= Time.deltaTime;
            if(_elapsedTime <= 0)
            {
                var bullet = Instantiate(_bulletPrefab, _center.position, transform.rotation);
                _elapsedTime = _shootDelay;
            }
        }
    }
}
