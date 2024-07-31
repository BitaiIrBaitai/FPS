using UnityEngine;

public class SimpleDamageProvider : DamageProviderBase
{
    [SerializeField] private Health _health;

    protected override Health GetHealth()
	{
		return _health;
	}

    protected override float CalculateDamage(Damage damage)
	{
		float totalDamage = 0;
		foreach (var damageUnit in damage.DamageUnits)
			totalDamage += damageUnit.Amount;

		return totalDamage;
	}
}
