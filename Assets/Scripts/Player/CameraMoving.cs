using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoving : MonoBehaviour
{
    [SerializeField] private Transform _cameraTransform;

    private void Update()
    {
        transform.position = _cameraTransform.position;
    }
}
