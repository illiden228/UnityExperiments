using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTank : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotateSpeed;

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal") * _moveSpeed * Time.deltaTime;
        float vertical = Input.GetAxisRaw("Vertical") * _moveSpeed * Time.deltaTime;

        var direction = new Vector3(horizontal, vertical);

        var angle = Vector2.Angle(Vector2.right, direction.normalized);
        var vectorToRotation = new Vector3(0, 0, transform.position.y < direction.normalized.y ? angle : (180f + 180f - angle));
        vectorToRotation.z = vectorToRotation.z == 360f ? 0f : vectorToRotation.z;

        var quaternionToRotation = Quaternion.Euler(vectorToRotation);
        
        if (transform.right != direction.normalized && direction != Vector3.zero)
            transform.rotation = Quaternion.RotateTowards(transform.rotation, quaternionToRotation, _rotateSpeed * Time.deltaTime);
        else
            transform.position += direction;
    }
}
