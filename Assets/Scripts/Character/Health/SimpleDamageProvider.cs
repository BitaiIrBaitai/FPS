using UnityEngine;

public class SimpleDamageProvider : DamageProviderBase
{
	public override DamageProviderBase MainProvider => _mainProvider;

    [SerializeField] private Health _health;
	[SerializeField] private DamageProviderBase _mainProvider;

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

#if UNITY_EDITOR

	private void Reset()
	{
		_mainProvider ??= this;
	}

#endif
}
