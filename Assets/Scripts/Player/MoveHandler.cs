using System;
using UnityEngine;

[RequireComponent(typeof(InputReader), typeof(PlayerController), typeof(PlayerAnimator))]
public class MoveHandler : MonoBehaviour
{
    private InputReader _inputReader;
    private PlayerController _playerController;
    private PlayerAnimator _playerAnimator;

    private void Awake()
    {
        _inputReader = GetComponent<InputReader>();
        _playerController = GetComponent<PlayerController>();
        _playerAnimator = GetComponent<PlayerAnimator>();
    }

    private void OnEnable()
    {
        _inputReader.MovePerformed += OnMovePerformed;
        _inputReader.MoveCanceled += OnMoveCanceled;
        _inputReader.JumpPerformed += OnJumpPerformed;
    }

    private void OnDisable()
    {
        _inputReader.MovePerformed -= OnMovePerformed;
        _inputReader.MoveCanceled -= OnMoveCanceled;
        _inputReader.JumpPerformed -= OnJumpPerformed;
    }

    private void Update()
    {
        UpdateAnimations();
    }

    private void OnMovePerformed(float moveInput)
    {
        _playerController.SetMoveInput(moveInput);
    }

    private void OnMoveCanceled()
    {
        _playerController.SetMoveInput(0f);
    }

    private void OnJumpPerformed()
    {
        _playerController.Jump();
    }

    private void UpdateAnimations()
    {
        _playerAnimator.UpdateAnimations(_playerController.MoveInput, _playerController.IsGrounded);
    }
}
