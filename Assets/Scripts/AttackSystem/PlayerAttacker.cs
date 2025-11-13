using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacker : MonoBehaviour, IAttacker
{
    [SerializeField] private float _damage = 10f;
    [SerializeField] private float _attackDuration = 0.5f;
    [SerializeField] private AttackZone _attackZone;
    
    private PlayerAnimator _playerAnimator;
    private InputReader _inputReader;
    private Coroutine _attackCoroutine;
    
    public float Damage => _damage;
    
    private void Awake()
    {
        _inputReader = GetComponent<InputReader>();
        _playerAnimator = GetComponent<PlayerAnimator>();
    }

    private void OnEnable()
    {
        _inputReader.AttackPerformed += OnAttackPerformed;
    }

    private void OnDisable()
    {
        _inputReader.AttackPerformed -= OnAttackPerformed;
        
        if (_attackCoroutine != null)
        {
            StopCoroutine(_attackCoroutine);
        }
    }
    
    private void OnAttackPerformed()
    {
        if (_attackCoroutine == null)
        {
            _attackCoroutine = StartCoroutine(AttackRoutine());
        }
    }

    private IEnumerator AttackRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(_attackDuration);
        
        _playerAnimator.PlayAttackAnimation();
        _attackZone.ApplyDamage();
        
        yield return wait;
        
        _attackCoroutine = null;
    }
}
