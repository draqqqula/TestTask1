using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCameraController : MonoBehaviour
{
    [SerializeField] private Transform _head;
    [SerializeField] private float _FOVLimitX;
    [SerializeField] private float _sensitivity;
    private float _yRotation;
    private float _xRotation;

    private float DeltaX => GetDelta("Mouse X");
    private float DeltaY => GetDelta("Mouse Y");

    private float GetDelta(string axis)
    {
        return Input.GetAxis(axis) * _sensitivity;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        _xRotation = Mathf.Clamp(_xRotation - DeltaY, -_FOVLimitX, _FOVLimitX);
        _yRotation += DeltaX;
        transform.rotation = Quaternion.Euler(0, _yRotation, 0);
        _head.localRotation = Quaternion.Euler(_xRotation, 0, 0);
    }
}
