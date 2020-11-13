using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private CharacterController _characterController;

    [SerializeField]
    private Transform _startPoint;

    [SerializeField]
    private float _moveSpeed = 0.1f;

    [SerializeField]
    private GameProgressEvents _gameProgressEvents;

    [SerializeField]
    private Joystick _joystick;

    [SerializeField]
    private Animator _animator;

    private void Start()
    {
        _gameProgressEvents.AddOnPlayerSpottedHandler(OnPlayerSpotted);
        _gameProgressEvents.AddOnPlayerReachedFinishHandler(OnPlayerFinishReached);
    }

    private void Update()
    {
        var inputVector = new Vector3(-_joystick.Vertical, 0, _joystick.Horizontal);
        
        if (inputVector != Vector3.zero)
        {
            _characterController.Move(inputVector * _moveSpeed);
            transform.LookAt(transform.position + inputVector);
            _animator.SetFloat("movespeed", 1f);
        }
        else
        {
            _animator.SetFloat("movespeed", 0);
        }

        
    }

    private void OnPlayerSpotted()
    {
        ResetToStart();
    }

    private void OnPlayerFinishReached()
    {
        ResetToStart();
    }

    private void ResetToStart()
    {
        _characterController.enabled = false;

        var startPosition = _startPoint.position;
        startPosition.y = transform.position.y;

        gameObject.transform.position = startPosition;
        _characterController.enabled = true;
    }
}
