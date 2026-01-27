using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyAnimator : MonoBehaviour
{
    private const string AttackTrigger = "Attack";
    private const string HitTrigger = "Hit";
    
    private Animator _animator;
    
    public event Action AttackHit;
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    
    public void PlayAttackAnimation()
    {
        _animator.SetTrigger(AttackTrigger);
    }
    
    public void PlayHitAnimation()
    {
        _animator.SetTrigger(HitTrigger);
    }

    public void OnAttack()
    {
        AttackHit?.Invoke();
    }
}