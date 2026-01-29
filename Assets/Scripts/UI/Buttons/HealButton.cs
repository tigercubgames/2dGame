using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class HealButton : ButtonBase
{
    protected override void ApplyValue(float amount)
    {
        Health.Heal(amount);
    }
}