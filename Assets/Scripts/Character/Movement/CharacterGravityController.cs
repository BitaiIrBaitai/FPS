using System;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterGravityController : MonoBehaviour
{
    public float VerticalVelocity { get; private set; }
    public bool IsGrounded { get; private set; }

    public event EventHandler<LandEventArgs> Landed;
    public event EventHandler<FallEventArgs> StartedFalling;

    [Header("Params")]
    [SerializeField] private float _gravityScale = 1f;
    [SerializeField] private float _maxVelocity = 10f;
    [SerializeField] private float _minVelocity = -10f;

	[Header("Components")]
	[SerializeField] private CharacterController _characterController;

	private Vector3 _fallStartPosition;

	private void Start()
	{
		_fallStartPosition = transform.position;
	}

	private void FixedUpdate()
	{
		CollisionFlags collisionFlags = _characterController.Move(Vector3.up * VerticalVelocity * Time.fixedDeltaTime);
        bool isGrounded = collisionFlags.HasFlag(CollisionFlags.Below);

		if (collisionFlags.HasFlag(CollisionFlags.Above))
			VerticalVelocity = Mathf.Min(VerticalVelocity, 0f);

		if (!isGrounded && IsGrounded)
		{
			IsGrounded = false;
			_fallStartPosition = transform.position;
			StartedFalling?.Invoke(this, new FallEventArgs(_fallStartPosition));
		}
		else if (isGrounded)
		{
			if (!IsGrounded)
				Landed?.Invoke(this, new LandEventArgs(VerticalVelocity, _fallStartPosition, transform.position));

			IsGrounded = true;
			VerticalVelocity = 0f;
		}

		VerticalVelocity += Physics.gravity.y * _gravityScale * Time.fixedDeltaTime;
		VerticalVelocity = Mathf.Clamp(VerticalVelocity, _minVelocity, _maxVelocity);
	}

	public void SetVelocity(float velocity)
	{
		VerticalVelocity = velocity;
	}

	public void AddVelocity(float velocity)
	{
		VerticalVelocity += velocity;
	}

#if UNITY_EDITOR

	[ContextMenu(nameof(TryGetComponents))]
	private void TryGetComponents()
	{
		_characterController = GetComponent<CharacterController>();
	}

	private void OnValidate()
	{
		if (_maxVelocity < _minVelocity)
			_maxVelocity = _minVelocity;
	}

#endif
}

public class FallEventArgs
{
	public Vector3 FallStartPosition { get; private set; }

	public FallEventArgs(Vector3 fallStartPosition)
    {
		FallStartPosition = fallStartPosition;
	}
}

public class LandEventArgs : FallEventArgs
{
	public Vector3 LandPosition { get; private set; }
	public float Velocity { get; private set; }

	public LandEventArgs(float velocity, Vector3 fallStartPosition, Vector3 landPosition) : base(fallStartPosition)
	{
		LandPosition = landPosition;
		Velocity = velocity;
	}
}
