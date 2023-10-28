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

    private Camera _mainCamera;

    private void Awake()
    {
        _mainCamera = Camera.main;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        _xRotation = _mainCamera.transform.rotation.eulerAngles.x;
        _yRotation = _mainCamera.transform.rotation.eulerAngles.y;
    }

    private void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * _sensX * Time.fixedDeltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * _sensY * Time.fixedDeltaTime;

        _yRotation += mouseX;
        _xRotation -= mouseY;

        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

        _mainCamera.transform.rotation = Quaternion.Euler(_xRotation, _yRotation, 0f);
        _orientation.rotation = Quaternion.Euler(0f, _yRotation, 0f);
    }
}
