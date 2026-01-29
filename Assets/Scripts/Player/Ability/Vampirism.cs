using System;
using System.Collections;
using UnityEngine;

public class Vampirism : MonoBehaviour
{
    private const float DamageTickInterval = 0.1f;
    private const int MaxOverlapCount = 3;
    
    [SerializeField] private float _duration = 6f;
    [SerializeField] private float _cooldown = 4f;
    [SerializeField] private float _damagePerSecond = 5f;
    [SerializeField] private float _detectionRadius = 5f;
    [SerializeField] private LayerMask _enemyLayer;

    [SerializeField] private Health _playerHealth;
    [SerializeField] private SpriteRenderer _radiusVisual;
    
    private Collider2D[] _overlapBuffer = new Collider2D[MaxOverlapCount];
    private Coroutine _abilityCoroutine;
    private WaitForSeconds _damageTickWait;
    private float _currentCooldown;
    private bool _isActive;
    private bool _isOnCooldown;
    
    public event Action<float, float> DurationChanged;
    public event Action<float, float> CooldownChanged;
    public event Action AbilityActivated;
    public event Action AbilityDeactivated;
    public event Action CooldownFinished;

    private void Awake()
    {
        _damageTickWait = new WaitForSeconds(DamageTickInterval);
        InitializeRadiusVisual();
    }

    private void Update()
    {
        if (_isOnCooldown)
        {
            UpdateCooldown();
        }
    }

    private void OnDisable()
    {
        if (_abilityCoroutine != null)
        {
            StopCoroutine(_abilityCoroutine);
            _abilityCoroutine = null;
        }
        
        if (_isActive)
        {
            Deactivate();
        }
    }

    public bool TryActivate()
    {
        if (_isActive || _isOnCooldown)
            return false;
        
        _abilityCoroutine = StartCoroutine(AbilityCoroutine());
        return true;
    }

    private void InitializeRadiusVisual()
    {
        if (_radiusVisual == null)
            return;
        
        _radiusVisual.gameObject.SetActive(false);

        float spriteRadius = _radiusVisual.bounds.extents.x;
        float scale = _detectionRadius / spriteRadius;
        _radiusVisual.transform.localScale = Vector3.one * scale;
    }

    private void UpdateCooldown()
    {
        _currentCooldown -= Time.deltaTime;
        
        if (_currentCooldown <= 0)
        {
            FinishCooldown();
        }
        
        CooldownChanged?.Invoke(_currentCooldown, _cooldown);
    }

    private void FinishCooldown()
    {
        _isOnCooldown = false;
        _currentCooldown = 0;
        CooldownFinished?.Invoke();
    }

    private IEnumerator AbilityCoroutine()
    {
        _isActive = true;
        float elapsedTime = 0f;
        
        if (_radiusVisual != null)
            _radiusVisual.gameObject.SetActive(true);
        
        AbilityActivated?.Invoke();
        
        while (elapsedTime < _duration)
        {
            if (!_playerHealth.IsAlive)
            {
                Deactivate();
                yield break;
            }
            
            Transform nearestEnemy = FindNearestEnemy();
            
            if (nearestEnemy != null)
            {
                ApplyVampirism(nearestEnemy);
            }
            
            elapsedTime += DamageTickInterval;
            DurationChanged?.Invoke(_duration - elapsedTime, _duration);
            
            yield return _damageTickWait;
        }
        
        Deactivate();
    }

    private Transform FindNearestEnemy()
    {
        int count = Physics2D.OverlapCircleNonAlloc(
            transform.position,
            _detectionRadius,
            _overlapBuffer,
            _enemyLayer
        );
        
        if (count == 0)
            return null;
        
        Transform nearest = null;
        float minDistance = float.MaxValue;
        Vector3 playerPosition = transform.position;
        
        for (int i = 0; i < count; i++)
        {
            if (_overlapBuffer[i] == null)
                continue;
            
            if (!_overlapBuffer[i].TryGetComponent(out Health health) || !health.IsAlive)
                continue;
            
            float distance = Vector2.SqrMagnitude(_overlapBuffer[i].transform.position - playerPosition);
            
            if (distance < minDistance)
            {
                minDistance = distance;
                nearest = _overlapBuffer[i].transform;
            }
        }
        
        return nearest;
    }

    private void ApplyVampirism(Transform enemy)
    {
        if (enemy.TryGetComponent(out Health enemyHealth))
        {
            float damage = _damagePerSecond * DamageTickInterval;
            enemyHealth.TakeDamage(damage);
            _playerHealth.Heal(damage);
        }
    }

    private void Deactivate()
    {
        _isActive = false;
        
        if (_radiusVisual != null)
            _radiusVisual.gameObject.SetActive(false);
        
        AbilityDeactivated?.Invoke();
        
        _isOnCooldown = true;
        _currentCooldown = _cooldown;
        CooldownChanged?.Invoke(_currentCooldown, _cooldown);
    }
}
