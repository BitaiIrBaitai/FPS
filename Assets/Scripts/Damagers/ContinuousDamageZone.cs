using System.Collections.Generic;
using UnityEngine;

public class ContinuousDamageZone : MonoBehaviour
{
    [SerializeField, Min(0)] private float _damagePerSecond;
    [SerializeField] private DamageType _damageType;

	private DamageUnit[] _damageUnits = new DamageUnit[1];
	private HashSet<DamageProviderBase> _damageProviders = new();

	private void Update()
	{
		if (_damageProviders.Count == 0)
			return;

		_damageUnits[0].Amount = _damagePerSecond * Time.deltaTime;
		_damageUnits[0].Type = _damageType;

		Damage damage = new (_damageUnits, transform.position, gameObject);

		foreach (DamageProviderBase damageProvider in _damageProviders)
			damageProvider.Hit(damage);
	}

	private void OnTriggerEnter(Collider other)
	{
		DamageProviderBase damageProvider = other.GetComponent<DamageProviderBase>();
		if (damageProvider?.MainProvider is not null)
			_damageProviders.Add(damageProvider.MainProvider);
	}

	private void OnTriggerExit(Collider other)
	{
		DamageProviderBase damageProvider = other.GetComponent<DamageProviderBase>();
		if (damageProvider?.MainProvider is not null)
			_damageProviders.Remove(damageProvider.MainProvider);
	}
}
