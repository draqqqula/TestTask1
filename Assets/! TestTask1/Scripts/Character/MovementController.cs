using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class MovementController : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private float _speed;
    [SerializeField] private float _gravity;

    private Vector3 Forward => transform.TransformDirection(Vector3.forward);
    private Vector3 Right => transform.TransformDirection(Vector3.right);

    private void Reset()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        var velocity = GetHorizontalMovement() + GetGravity();
        _characterController.Move(velocity);
    }

    private Vector3 GetHorizontalMovement()
    {
        var vertical = Input.GetAxis("Vertical");
        var horizontal = Input.GetAxis("Horizontal");
        return (vertical * Forward + horizontal * Right) * _speed;
    }

    private Vector3 GetGravity()
    {
        return Vector3.down * _gravity;
    }
}
