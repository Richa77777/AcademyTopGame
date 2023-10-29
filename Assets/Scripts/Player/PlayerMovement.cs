using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _jumpCooldown;
    [SerializeField] private float _airMultiplier;
    [SerializeField] private float _sprintMultiplier = 1.5f;
    [SerializeField] private float _coyoteTime = 0.1f;

    [SerializeField] private Transform _orientation;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private KeyCode _jumpKey = KeyCode.Space;
    [SerializeField] private KeyCode _sprintKey = KeyCode.LeftShift;

    private float _horizontalInput;
    private float _verticalInput;
    private float _currentMoveSpeed;
    private float coyoteTimeCounter;

    private bool _readyToJump = true;
    private bool _isGrounded;

    private Vector3 _moveDirection;

    private Rigidbody _rigidbody;
    private Collider _collider;
    private Camera _mainCamera;

    private Coroutine _sprintViewCor;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();

        _mainCamera = Camera.main;
        _rigidbody.freezeRotation = true;
        _currentMoveSpeed = _moveSpeed;

    }

    private void Update()
    {
        _isGrounded = Physics.Raycast(transform.position, Vector3.down, _collider.bounds.size.y * 0.5f + 0.2f, _groundLayer);

        PlayerInput();
    }

    private void FixedUpdate()
    {
        PlayerMove();
    }

    private void PlayerInput()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(_sprintKey) && _isGrounded == true)
        {
            _currentMoveSpeed = _moveSpeed * _sprintMultiplier;

            if (_sprintViewCor != null)
            {
                StopCoroutine(_sprintViewCor);
            }

            _sprintViewCor = StartCoroutine(SprintViewCor(70f));
        }

        if (Input.GetKeyUp(_sprintKey))
        {
            _currentMoveSpeed = _moveSpeed;

            if (_sprintViewCor != null)
            {
                StopCoroutine(_sprintViewCor);
            }

            _sprintViewCor = StartCoroutine(SprintViewCor(60f));
        }

        if (_isGrounded == true)
        {
            coyoteTimeCounter = _coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (Input.GetKeyDown(_jumpKey) && (_isGrounded || coyoteTimeCounter > 0) && _readyToJump)
        {
            Jump();
        }
    }

    private void PlayerMove()
    {
        _moveDirection = _orientation.forward * _verticalInput + _orientation.right * _horizontalInput;

        Vector3 velocity = _moveDirection.normalized * _currentMoveSpeed;

        if (!_isGrounded)
        {
            velocity = _moveDirection.normalized * _currentMoveSpeed * _airMultiplier;
        }

        _rigidbody.velocity = new Vector3(velocity.x, _rigidbody.velocity.y, velocity.z);
    }

    private void Jump()
    {
        if (_readyToJump)
        {
            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, 0f, _rigidbody.velocity.z);
            _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            _readyToJump = false;
            StartCoroutine(ResetJumpCor());
        }
    }

    private IEnumerator ResetJumpCor()
    {
        yield return new WaitForSeconds(_jumpCooldown);
        _readyToJump = true;
    }

    private IEnumerator SprintViewCor(float targetValue)
    {
        float startValue = _mainCamera.fieldOfView;

        for (float t = 0; t < 0.15f; t += Time.deltaTime)
        {
            _mainCamera.fieldOfView = Mathf.Lerp(startValue, targetValue, t / 0.15f);
            yield return null;
        }

        _mainCamera.fieldOfView = targetValue;
    }
}