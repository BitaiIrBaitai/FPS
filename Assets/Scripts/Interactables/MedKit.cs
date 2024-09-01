using System;
using UnityEngine;

public class MedKit : MonoBehaviour, IInteractable
{
	public bool Active => _healAmount > 0
		&& Player.Instance.Health.CurrentHealth < Player.Instance.Health.MaxHealth;

	[SerializeField, Min(1)] private float _healAmount = 25;

	public event EventHandler StateChanged;

	public string GetMainInformation()
	{
		return "Med Kit";
	}

	public string GetSecondaryInformation()
	{
		return $"Heals {_healAmount:F0} health points";
	}

	public void Interact()
	{
		float taken = Player.Instance.Health.Heal(_healAmount);

		if (taken > 0)
		{
			_healAmount -= taken;
			StateChanged?.Invoke(this, EventArgs.Empty);
		}
	}
}
