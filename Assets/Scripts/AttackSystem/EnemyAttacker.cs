using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttacker : MonoBehaviour, IAttacker
{
    private const string AttackTrigger = "Attack";
    
    [SerializeField] private float _damage = 5f;
    [SerializeField] private float _attackRange = 1.5f;
    [SerializeField] private float _attackDuration = 2.0f;
    [SerializeField] private AttackZone _attackZone;
    
    private Animator _animator;
    private Coroutine _attackCoroutine;
    
    public event Action AttackEnded;
    
    public float Damage => _damage;
    public float AttackRange => _attackRange;
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    
    private void OnDisable()
    {
        if (_attackCoroutine != null)
        {
            StopCoroutine(_attackCoroutine);
            _attackCoroutine = null;
        }
    }
    
    public bool CanAttack(Vector3 targetPosition)
    {
        if (_attackCoroutine != null)
            return false;

        float distanceToTarget = Vector3.Distance(transform.position, targetPosition);
        return distanceToTarget <= _attackRange;
    }

    public void TryAttack()
    {
        if (_attackCoroutine == null)
        {
            _attackCoroutine = StartCoroutine(AttackRoutine());
        }
    }

    private IEnumerator AttackRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(_attackDuration);
        
        _animator.SetTrigger(AttackTrigger);
        _attackZone.ApplyDamage();
        
        yield return wait;
        
        _attackCoroutine = null;
        AttackEnded?.Invoke();
    }
}
