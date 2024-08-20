using UnityEngine;

public abstract class DamageProviderBase : MonoBehaviour
{
    public Health Health => GetHealth();
    public abstract DamageProviderBase MainProvider { get; }

    public float Hit(Damage damage)
    {
        float totalDamage = CalculateDamage(damage);
        float delta = Health.Hit(totalDamage, damage);
        return delta;
    }

    protected abstract Health GetHealth();
    protected abstract float CalculateDamage(Damage damage);
}
