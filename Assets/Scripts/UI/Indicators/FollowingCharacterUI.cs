using UnityEngine;

public class FollowingCharacterUI : MonoBehaviour
{
    [SerializeField] private Transform _character;
    [SerializeField] private Vector3 _offset = new Vector3(0, 1, 0);
    [SerializeField] private bool _usePooling = false;
    
    private Health _health;

    private void Awake()
    {
        if (_character != null)
        {
            _health = _character.GetComponent<Health>();
        }
    }

    private void OnEnable()
    {
        if (_health != null)
        {
            _health.Died += OnCharacterDied;
        }
    }

    private void OnDisable()
    {
        if (_health != null)
        {
            _health.Died -= OnCharacterDied;
        }
    }

    private void LateUpdate()
    {
        if (_character != null)
        {
            transform.position = _character.position + _offset;
            transform.rotation = Quaternion.identity;
        }
    }
    
    private void OnCharacterDied()
    {
        if (_usePooling)
        {
            gameObject.SetActive(false);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
