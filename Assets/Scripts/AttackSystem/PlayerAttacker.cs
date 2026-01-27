using System;
using System.Collections;
using UnityEngine;

public class PlayerAttacker : MonoBehaviour, IAttacker
{
    [SerializeField] private float _damage = 10f;
    [SerializeField] private float _attackDuration = 0.5f;
    [SerializeField] private AttackZone _attackZone;
    
    private Coroutine _attackCoroutine;
    private PlayerAnimator _animator;
    
    public event Action AttackStarted;
    public event Action AttackEnded;
    
    public float Damage => _damage;
    
    private void Awake()
    {
        _animator = GetComponent<PlayerAnimator>();
    }
    
    private void OnEnable()
    {
        if (_animator != null)
        {
            _animator.AttackHit += HandleAttackHit;
        }
    }
    
    private void OnDisable()
    {
        if (_animator != null)
        {
            _animator.AttackHit -= HandleAttackHit;
        }
        
        if (_attackCoroutine != null)
        {
            StopCoroutine(_attackCoroutine);
            _attackCoroutine = null;
        }
    }
    
    public bool CanAttack()
    {
        return _attackCoroutine == null;
    }
    
    public void Attack()
    {
        if (CanAttack())
        {
            _attackCoroutine = StartCoroutine(AttackRoutine());
        }
    }
    
    private void HandleAttackHit()
    {
        _attackZone.ApplyDamage();
    }

    private IEnumerator AttackRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(_attackDuration);
        
        AttackStarted?.Invoke();
        
        yield return wait;
        
        _attackCoroutine = null;
        AttackEnded?.Invoke();
    }
}