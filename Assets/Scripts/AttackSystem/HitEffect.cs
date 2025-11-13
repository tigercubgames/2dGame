using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class HitEffect : MonoBehaviour
{
    private const string HitTrigger = "Hit";
    
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void PlayHitEffect()
    {
        _animator.SetTrigger(HitTrigger);
    }
}
