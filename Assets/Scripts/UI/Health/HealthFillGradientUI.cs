using UnityEngine;
using UnityEngine.UI;

public class HealthFillGradientUI : MonoBehaviour
{
    [SerializeField] private Gradient _gradient;

    [Header("Components")]
    [SerializeField] private Health _health;
    [SerializeField] private Image _fill;

	private void Start()
	{
		SetGradient(_health.CurrentHealth / _health.MaxHealth);
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
		SetGradient(args.Percent);
	}

	private void SetGradient(float amount)
	{
		_fill.color = _gradient.Evaluate(amount);
	}
}
