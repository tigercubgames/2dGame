using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class DamageButton : ButtonBase
{
    protected override void ApplyValue(float amount)
    {
        Health.TakeDamage(amount);
    }
}
