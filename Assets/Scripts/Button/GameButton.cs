using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameButton : MonoBehaviour
{
    [SerializeField] private UnityEvent _clickActions;

    [SerializeField] private GameButtonTrigger _buttonTrigger;
    [SerializeField] private MeshRenderer _buttonUpMeshRenderer;
    [SerializeField] private Material _buttonOnMaterial;
    [SerializeField] private float _buttonOnOffset = 0.03f;

    [SerializeField] private GameObject _interactTab;
    [SerializeField] private KeyCode _interactKey = KeyCode.E;

    private bool _mightInteract;
    private float _clickDuration = 0.1f;

    private void Start()
    {
        _interactTab.SetActive(true);
        _interactTab.SetActive(false);
    }

    private void OnEnable()
    {
        _buttonTrigger.OnEnter += ShowInteractTab;
        _buttonTrigger.OnExit += HideInteractTab;
    }

    private void OnDisable()
    {
        _buttonTrigger.OnEnter -= ShowInteractTab;
        _buttonTrigger.OnExit -= HideInteractTab;
    }

    private void Update()
    {
        if (_mightInteract == true)
        {
            if (Input.GetKeyUp(_interactKey))
            {
                ActivateButton();
            }
        }
    }

    private void ActivateButton()
    {
        _interactTab.SetActive(false);

        StartCoroutine(ClickButton());

        _clickActions?.Invoke();

        _buttonTrigger.gameObject.SetActive(false);
        this.enabled = false;
    }

    private void ShowInteractTab()
    {
        _mightInteract = true;
        _interactTab.SetActive(true);
    }

    private void HideInteractTab()
    {
        _mightInteract = false;
        _interactTab.SetActive(false);
    }

    private IEnumerator ClickButton()
    {
        Vector3 startPos = _buttonUpMeshRenderer.gameObject.transform.position;
        Vector3 targetPos = startPos;

        targetPos.y -= _buttonOnOffset;

        for (float t = 0; t < _clickDuration; t += Time.deltaTime)
        {
            _buttonUpMeshRenderer.gameObject.transform.position = Vector3.Lerp(startPos, targetPos, t / _clickDuration);
            yield return null;
        }

        _buttonUpMeshRenderer.gameObject.transform.position = targetPos;
        _buttonUpMeshRenderer.material = _buttonOnMaterial;
    }
}
