using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputReader))]
[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(PlayerAnimator))]
[RequireComponent(typeof(PlayerAttacker))]
[RequireComponent(typeof(Flipper))]
[RequireComponent(typeof(Health))]
public class Player : MonoBehaviour
{
    private InputReader _inputReader;
    private PlayerController _controller;
    private PlayerAnimator _animator;
    private PlayerAttacker _attacker;
    private Flipper _flipper;
    private Health _health;
    
    private float _moveDirection;
    
    private void Awake()
    {
        _inputReader = GetComponent<InputReader>();
        _controller = GetComponent<PlayerController>();
        _animator = GetComponent<PlayerAnimator>();
        _attacker = GetComponent<PlayerAttacker>();
        _flipper = GetComponent<Flipper>();
        _health = GetComponent<Health>();
    }
    
    private void OnEnable()
    {
        _inputReader.MovePerformed += OnMovePerformed;
        _inputReader.MoveCanceled += OnMoveCanceled;
        _inputReader.JumpPerformed += OnJumpPerformed;
        _inputReader.AttackPerformed += OnAttackPerformed;
        _attacker.AttackStarted += OnAttackStarted;
        _health.DamageTaken += OnDamageTaken;
        _health.Died += OnDied;
    }
    
    private void OnDisable()
    {
        _inputReader.MovePerformed -= OnMovePerformed;
        _inputReader.MoveCanceled -= OnMoveCanceled;
        _inputReader.JumpPerformed -= OnJumpPerformed;
        _inputReader.AttackPerformed -= OnAttackPerformed;
        _attacker.AttackStarted -= OnAttackStarted;
        _health.DamageTaken -= OnDamageTaken;
        _health.Died -= OnDied;
    }
    
    private void FixedUpdate()
    {
        HandleMovement();
        UpdateAnimator();
    }
    
    private void OnMovePerformed(float direction)
    {
        _moveDirection = direction;
    }
    
    private void OnMoveCanceled()
    {
        _moveDirection = 0f;
    }
    
    private void OnJumpPerformed()
    {
        _controller.Jump();
    }
    
    private void OnAttackPerformed()
    {
        _attacker.Attack();
    }
    
    private void OnAttackStarted()
    {
        _animator.PlayAttackAnimation();
    }
    
    private void OnDamageTaken()
    {
        _animator.PlayHitAnimation();
    }
    
    private void OnDied()
    {
        Debug.Log($"{gameObject.name} погиб!");
        gameObject.SetActive(false);
    }
    
    private void HandleMovement()
    {
        _controller.Move(_moveDirection);
        
        if (_moveDirection != 0)
        {
            _flipper.Flip(_moveDirection);
        }
    }
    
    private void UpdateAnimator()
    {
        bool isRunning = Mathf.Abs(_moveDirection) > 0.1f;
        _animator.SetRunning(isRunning);
        _animator.SetJumping(_controller.IsGrounded == false);
    }
}
