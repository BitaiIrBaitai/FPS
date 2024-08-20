using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class FullscreenImpact : MonoBehaviour
{
	[Header("Params")]
	[SerializeField, Min(0.001f)] private float _apearCoefficientPerDamageUnit;
	[SerializeField, Min(0.001f)] private float _hideDuration;
	[SerializeField] private AnimationCurve _scaleCurve;
	[SerializeField] private AnimationCurve _alphaCurve;
	[SerializeField] private DamageType _damageType;

	[Header("Components")]
	[SerializeField] private Image _image;
	[SerializeField] private Health _health;

	private float _fillAmount = 0;
	private Color _color;

	private void Start()
	{
		_color = _image.color;
		SetScaleAndColor();
	}

	private void OnEnable()
	{
		_health.HealthDepleted += OnHealthDepleted;
	}

	private void OnDisable()
	{
		_health.HealthDepleted -= OnHealthDepleted;
	}

	private void Update()
	{
		if (_fillAmount <= 0)
			return;

		_fillAmount -= Time.deltaTime / _hideDuration;
		_fillAmount = Mathf.Clamp01(_fillAmount);

		SetScaleAndColor();
	}

	private void OnHealthDepleted(object sender, HealthChangedEventArgs args)
	{
		if (!args.Damage.DamageUnits.Any(u => u.Type == _damageType))
			return;

		_fillAmount += Mathf.Abs(args.Delta) * _apearCoefficientPerDamageUnit;
		_fillAmount = Mathf.Clamp01(_fillAmount);
	}

	private void SetScaleAndColor()
	{
		float scale = _scaleCurve.Evaluate(_fillAmount);
		float alpha = _alphaCurve.Evaluate(_fillAmount);

		transform.localScale = Vector3.one * scale;
		_color.a = alpha;
		_image.color = _color;
	}
}
