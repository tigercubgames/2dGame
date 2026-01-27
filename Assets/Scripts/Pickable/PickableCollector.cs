using UnityEngine;

public class PickableCollector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out PickableItem pickable))
        {
            pickable.PickUp(gameObject);
        }
    }
}
