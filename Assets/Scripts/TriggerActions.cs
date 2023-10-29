using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerActions : MonoBehaviour
{
    [SerializeField] private UnityEvent _onTriggerEnter;
    [SerializeField] private UnityEvent _onTriggerStay;
    [SerializeField] private UnityEvent _onTriggerExit;

    [SerializeField] private string _colliderTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_colliderTag))
        {
            _onTriggerEnter?.Invoke();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(_colliderTag))
        {
            _onTriggerStay?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(_colliderTag))
        {
            _onTriggerExit?.Invoke();

        }
    }
}
