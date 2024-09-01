using System;
using UnityEngine;
using UnityEngine.UI;

public class ImpactPointer : MonoBehaviour
{
	public Damage Damage { get; private set; }

	[Header("Params")]
	[SerializeField, Min(0.1f)] private float _hideDuration = 1.5f;
	[SerializeField] private AnimationCurve _hideCurve;

	[Header("Components")]
	[SerializeField] private RectTransform _pointer;
	[SerializeField] private Image _pointerImage;

	private float _percentage;
	private Color _color;
	private Action<ImpactPointer> _callback;

	private void Awake()
	{
		_color = _pointerImage.color;
	}

	public void ShowImpact(Damage damage, Action<ImpactPointer> callback)
	{
		Damage = damage;
		_callback = callback;
		_percentage = 1;

		_pointer.gameObject.SetActive(true);
	}

	public void ResetImpact()
	{
		_percentage = 1;
	}

	private void Update()
	{
		_color.a = _hideCurve.Evaluate(_percentage);
		_pointerImage.color = _color;

		Vector3 playerForward = Player.Instance.transform.forward;
		Vector3 playerPos = Player.Instance.transform.position;
		Vector3 sourcePos = Damage.SourceObject.transform.position;

		Vector3 direction = sourcePos - playerPos;
		direction.y = 0;

		float angle = Vector3.SignedAngle(direction, playerForward, Vector3.up);
		_pointer.rotation = Quaternion.Euler(0, 0, angle);

		_percentage -= Time.deltaTime / _hideDuration;

		if (_percentage <= 0)
		{
			_pointer.gameObject.SetActive(false);
			_callback?.Invoke(this);
		}
	}
}
