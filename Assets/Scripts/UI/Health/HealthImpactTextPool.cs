using System.Collections.Generic;
using UnityEngine;

public class HealthImpactTextPool : MonoBehaviour
{
    [Header("Params")]
    [SerializeField] private Color _positiveColor = Color.green;
    [SerializeField] private Color _negativeColor = Color.red;
    [SerializeField, Min(0)] private float _minDeltaToShow = 1f;
    [SerializeField] private string _positiveTextformat = "+{0}";
    [SerializeField] private string _negativeTextformat = "{0}";

    [Header("Components")]
    [SerializeField] private HealthImpactText _impactTextPrefab;
    [SerializeField] private Health _health;

    private Queue<HealthImpactText> _impactTexts = new();
    private float _delta;

	private void OnEnable()
	{
		_health.HealthRestored += OnHealthRestored;
		_health.HealthDepleted += OnHealthDepleted;
	}

	private void OnDisable()
	{
		_health.HealthRestored -= OnHealthRestored;
		_health.HealthDepleted -= OnHealthDepleted;
	}

	private void OnHealthRestored(object sender, HealthChangedEventArgs args)
	{
		_delta += args.Delta;

		if (Mathf.Abs(_delta) < _minDeltaToShow)
			return;

		HealthImpactText impactText = GetImpactText();
		impactText.ShowImpact(  string.Format(_positiveTextformat, _delta),
								_positiveColor,
								_impactTexts.Enqueue);

		_delta = 0;
	}

	private void OnHealthDepleted(object sender, HealthChangedEventArgs args)
	{
		_delta += args.Delta;

		if (Mathf.Abs(_delta) < _minDeltaToShow)
			return;

		HealthImpactText impactText = GetImpactText();
		impactText.ShowImpact(  string.Format(_negativeTextformat, _delta),
								_negativeColor,
								_impactTexts.Enqueue);

		_delta = 0;
	}

	private HealthImpactText GetImpactText()
	{
		if (_impactTexts.Count > 0)
			return _impactTexts.Dequeue();
		else 
			return Instantiate(_impactTextPrefab, transform);
	}
}
