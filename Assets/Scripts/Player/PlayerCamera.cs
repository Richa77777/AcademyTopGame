using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private float _sensX;
    [SerializeField] private float _sensY;

    private float _xRotation;
    private float _yRotation;

    [SerializeField] private Transform _orientation;

    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * _sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * _sensY;

        _yRotation += mouseX;
        _xRotation -= mouseY;

        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

        transform.rotation = Quaternion.Euler(_xRotation, _yRotation, 0f);
        _orientation.rotation = Quaternion.Euler(0f, _yRotation, 0f);
    }
}
