using TMPro;
using UnityEngine;

public class HealthTextUI : MonoBehaviour
{
    [Header("Params")]
    [Tooltip("{0} - current health\n" +
             "{1} - max health\n" +
             "{2} - current health in percent 0-100%\n" +
             "{3} - current health in percent 0-1")]
    [SerializeField] private string _format = "{0:0} / {1:0}";

    [Header("Components")]
    [SerializeField] private Health _health;
    [SerializeField] private TMP_Text _text;

	private void Start()
	{
		SetText(_health.CurrentHealth, _health.MaxHealth);
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
		SetText(args.CurrentHealth, args.MaxHealth);
	}

	private void SetText(float current, float max)
	{
		_text.text = string.Format( _format,
									current,
									max,
									current / max * 100,
									current / max);
	}
}
