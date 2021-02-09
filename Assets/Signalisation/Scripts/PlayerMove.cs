using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotationSpeed;

    private Rigidbody _rigidbody;
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float mouse = Input.GetAxis("Mouse X");
        transform.Rotate(0, _rotationSpeed * mouse * Time.deltaTime, 0);
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward * _moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position -= transform.forward * _moveSpeed * Time.deltaTime;
        }
    }
}
