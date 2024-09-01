using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ImpactPointerPool : MonoBehaviour
{
    [Header("Params")]
	[SerializeField] private DamageType[] _allowedDamage;

	[Header("Components")]
	[SerializeField] private ImpactPointer _prefabPointer;
	
	private Health _health;
	private HashSet<DamageType> _allowedDamageSet;
	private HashSet<ImpactPointer> _pointersInUse = new();
	private Queue<ImpactPointer> _pointersPool = new();

	private void Awake()
	{
		_health = Player.Instance.Health;
		_allowedDamageSet = new HashSet<DamageType>(_allowedDamage);
		_allowedDamage = null;
	}

	private void OnEnable()
	{
		_health.HealthDepleted += OnHealthDepleted;
	}

	private void OnDisable()
	{
		_health.HealthDepleted -= OnHealthDepleted;
	}

	private void OnHealthDepleted(object sender, HealthChangedEventArgs args)
	{
		Damage damage = args.Damage;

		if (damage.SourceObject is null)
			return;

		if (damage.DamageUnits.All(d => !_allowedDamageSet.Contains(d.Type)))
			return;

		foreach (ImpactPointer pointer in _pointersInUse)
		{
			if (pointer.Damage.SourceObject == damage.SourceObject)
			{
				pointer.ResetImpact();
				return;
			}
		}

		ImpactPointer newPointer = GetPointer();
		newPointer.ShowImpact(damage, OnPointerHide);
		_pointersInUse.Add(newPointer);
	}

	private void OnPointerHide(ImpactPointer pointer)
	{
		_pointersInUse.Remove(pointer);
		_pointersPool.Enqueue(pointer);
	}

	private ImpactPointer GetPointer()
	{
		return _pointersPool.Count > 0
			? _pointersPool.Dequeue() :
			Instantiate(_prefabPointer, transform);
	}
}
