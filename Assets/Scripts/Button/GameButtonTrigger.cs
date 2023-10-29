using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameButtonTrigger : MonoBehaviour
{
    public Action OnEnter;
    public Action OnExit;

    [SerializeField] private string _colliderTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_colliderTag))
        {
            OnEnter();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(_colliderTag))
        {
            OnExit();
        }
    }
}
