using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputReader))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerAnimator))]
[RequireComponent(typeof(PlayerAttacker))]
[RequireComponent(typeof(Flipper))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Vampirism))]
public class Player : MonoBehaviour
{
    private InputReader _inputReader;
    private PlayerMovement _movement;
    private PlayerAnimator _animator;
    private PlayerAttacker _attacker;
    private Flipper _flipper;
    private Health _health;
    private Vampirism _vampirism;
    
    private float _moveDirection;
    
    private void Awake()
    {
        _inputReader = GetComponent<InputReader>();
        _movement = GetComponent<PlayerMovement>();
        _animator = GetComponent<PlayerAnimator>();
        _attacker = GetComponent<PlayerAttacker>();
        _flipper = GetComponent<Flipper>();
        _health = GetComponent<Health>();
        _vampirism = GetComponent<Vampirism>();
    }
    
    private void OnEnable()
    {
        _inputReader.MovePerformed += OnMovePerformed;
        _inputReader.MoveCanceled += OnMoveCanceled;
        _inputReader.JumpPerformed += OnJumpPerformed;
        _inputReader.AttackPerformed += OnAttackPerformed;
        _inputReader.VampirismPerformed += OnVampirismPerformed;
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
        _inputReader.VampirismPerformed -= OnVampirismPerformed;
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
        _movement.Jump();
    }
    
    private void OnAttackPerformed()
    {
        _attacker.Attack();
    }
    
    private void OnVampirismPerformed()
    {
        _vampirism.TryActivate();
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
        _movement.Move(_moveDirection);
        
        if (_moveDirection != 0)
        {
            _flipper.Flip(_moveDirection);
        }
    }
    
    private void UpdateAnimator()
    {
        bool isRunning = Mathf.Abs(_moveDirection) > 0.1f;
        _animator.SetRunning(isRunning);
        _animator.SetJumping(_movement.IsGrounded == false);
    }
}
