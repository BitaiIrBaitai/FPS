using UnityEngine;
using UnityEngine.UI;

public class HealthSliderUI : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private Image _fill;

    private void Start()
	{
		SetFill(_health.CurrentHealth / _health.MaxHealth);
	}

	private void OnEnable()
	{
		_health.HealthChanged += OnHealthChanged;
	}

	private void OnDisable()
	{
		_health.HealthChanged -= OnHealthChanged;
	}

	private void OnHealthChanged(object _, HealthChangedEventArgs args)
	{
		SetFill(args.Percent);
	}

	private void SetFill(float amount)
	{
		_fill.fillAmount = amount;
	}
}
