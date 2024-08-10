using System.Collections.Generic;
using UnityEngine;

public class PeriodicDamageZone : MonoBehaviour
{
    [SerializeField, Min(0)] private float _damagePerPeriod;
    [SerializeField, Min(0)] private float _period;
    [SerializeField] private DamageType _damageType;

    private DamageUnit[] _damageUnits = new DamageUnit[1];
	private HashSet<PeriodElement> _elements = new();

	private void Awake()
	{
		_damageUnits[0].Type = _damageType;
		_damageUnits[0].Amount = _damagePerPeriod;
	}

	private void Update()
	{
		if (_elements.Count == 0)
			return;

		Damage damage = null;

		foreach (var element in _elements)
		{
			element.TimeToNextDamage -= Time.deltaTime;
			if (element.TimeToNextDamage <= 0)
			{
				damage ??= new Damage(_damageUnits, transform.position, gameObject);
				element.DamageProvider.Hit(damage);
				element.TimeToNextDamage += _period;
			}
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.TryGetComponent(out DamageProviderBase provider))
			if (provider.MainProvider is not null)
				_elements.Add(new()
				{ 
					DamageProvider = provider.MainProvider,
					TimeToNextDamage = _period
				});
	
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.TryGetComponent(out DamageProviderBase provider))
				_elements.Remove(new() { DamageProvider = provider?.MainProvider });
	}

	private class PeriodElement
	{
		public DamageProviderBase DamageProvider;
		public float TimeToNextDamage;

		public override bool Equals(object obj)
		{
			if (obj is PeriodElement other)
				return DamageProvider == other.DamageProvider;

			if (obj is DamageProviderBase provider)
				return DamageProvider == provider;

			return false;
		}

		public override int GetHashCode() => DamageProvider.GetHashCode();
	}
}
