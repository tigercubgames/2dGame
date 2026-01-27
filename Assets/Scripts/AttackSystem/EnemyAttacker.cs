using System;
using System.Collections;
using UnityEngine;

public class EnemyAttacker : MonoBehaviour, IAttacker
{
    [SerializeField] private float _damage = 5f;
    [SerializeField] private float _attackRange = 1.5f;
    [SerializeField] private float _attackCooldown = 2.0f;
    [SerializeField] private AttackZone _attackZone;
    
    private Coroutine _attackCoroutine;
    private EnemyAnimator _animator;
    
    public event Action AttackStarted;
    public event Action AttackEnded;
    
    public float Damage => _damage;
    public float AttackRange => _attackRange;
    
    private void Awake()
    {
        _animator = GetComponent<EnemyAnimator>();
    }
    
    private void OnEnable()
    {
        if (_animator != null)
        {
            _animator.AttackHit += OnAttackHit;
        }
    }
    
    private void OnDisable()
    {
        if (_animator != null)
        {
            _animator.AttackHit -= OnAttackHit;
        }
        
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

        return IsInAttackRange(targetPosition);
    }

    public void Attack()
    {
        if (_attackCoroutine == null)
        {
            _attackCoroutine = StartCoroutine(AttackCoroutine());
        }
    }
    
    private void OnAttackHit()
    {
        _attackZone.ApplyDamage();
    }

    private bool IsInAttackRange(Vector3 targetPosition)
    {
        float sqrDistance = (targetPosition - transform.position).sqrMagnitude;
        float sqrAttackRange = _attackRange * _attackRange;
        
        return sqrDistance <= sqrAttackRange;
    }

    private IEnumerator AttackCoroutine()
    {
        AttackStarted?.Invoke();
        
        yield return new WaitForSeconds(_attackCooldown);
        
        _attackCoroutine = null;
        AttackEnded?.Invoke();
    }
}
