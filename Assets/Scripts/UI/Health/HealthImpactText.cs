using System;
using TMPro;
using UnityEngine;

public class HealthImpactText : MonoBehaviour
{
    [SerializeField, Min(0.5f)] private float _impactTextLifetime = 1.5f;
    [SerializeField, Min(0.1f)] private float _disappearDuration = 0.5f;
	[SerializeField] private TMP_Text _impactText;
    [SerializeField] private Animator _animator;

    private Action<HealthImpactText> _callback;
    private float _impactTime;
    private float _disappearTime;

    public void ShowImpact(string text, Color color, Action<HealthImpactText> callback)
    {
        _callback = callback;
        _impactText.text = text;
        _impactText.color = color;
        _impactTime = _impactTextLifetime - _disappearDuration;
		_disappearTime = _disappearDuration;

        _impactText.gameObject.SetActive(true);
        _animator.SetTrigger("PlayImpact");
    }

    private void Update()
	{
		if (_impactTime > 0)
		{
			_impactTime -= Time.deltaTime;
			return;
		}

		if (_disappearTime > 0)
		{
			_disappearTime -= Time.deltaTime;
			_impactText.color = new Color(
										_impactText.color.r,
										_impactText.color.g,
										_impactText.color.b,
										_disappearTime / _disappearDuration);
			return;
		}

		_impactText.gameObject.SetActive(false);
		_callback?.Invoke(this);
	}
}
