using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float CurrentHealth { get; private set; }
    [field:SerializeField, Min(0.001f)] public float MaxHealth { get; private set; }

	public event EventHandler<HealthChangedEventArgs> HealthChanged;
	public event EventHandler<HealthChangedEventArgs> HealthRestored;
	public event EventHandler<HealthChangedEventArgs> HealthDepleted;
	public event EventHandler<HealthChangedEventArgs> HealthEnded;

    private void Awake()
	{
		CurrentHealth = MaxHealth;
	}

	public float Hit(float amount, Damage damage)
	{
		if (CurrentHealth <= 0)
			return 0;

		if (amount <= 0)
			return 0;

		float delta = Mathf.Min(CurrentHealth, amount);
		CurrentHealth -= delta;

		HealthChangedEventArgs args = new(CurrentHealth, MaxHealth, -delta, damage);
		HealthChanged?.Invoke(this, args);
		HealthDepleted?.Invoke(this, args);

		if (CurrentHealth <= 0)
			HealthEnded?.Invoke(this, args);

		return delta;
	}

	public float Heal(float amount)
	{
		if (CurrentHealth >= MaxHealth)
			return 0;

		if (amount <= 0)
			return 0;

		float delta = Mathf.Min(MaxHealth - CurrentHealth, amount);
		CurrentHealth += delta;

		HealthChangedEventArgs args = new(CurrentHealth, MaxHealth, delta);
		HealthChanged?.Invoke(this, args);
		HealthRestored?.Invoke(this, args);

		return delta;
	}

	public float Kill()
	{
		DamageUnit unit = new()
		{
			Type = DamageType.Kill,
			Amount = CurrentHealth
		};
		Damage damage = new(
			new[] { unit },
			transform.position,
			gameObject);

		return Hit(CurrentHealth, damage);
	}
}

public class HealthChangedEventArgs
{
	public float CurrentHealth { get; private set; }
    public float MaxHealth { get; private set; }
	public float Delta { get; private set; }
	public float Percent => CurrentHealth / MaxHealth;
	public Damage Damage { get; private set; }

	public HealthChangedEventArgs(float currentHealth, float maxHealth, float delta, Damage damage = null)
	{
		CurrentHealth = currentHealth;
		MaxHealth = maxHealth;
		Delta = delta;
		Damage = damage;
	}
}
