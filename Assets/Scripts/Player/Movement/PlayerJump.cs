using UnityEngine;

[RequireComponent(typeof(CharacterGravityController))]
public class PlayerJump : MonoBehaviour
{
	[Header("Params")]
	[SerializeField, Min(0.5f)] private float _jumpHeight = 1f;
	[SerializeField, Min(1)] private int _maxJumps = 2;
	[SerializeField] private float _minVelocityToJump = -0.5f;

	private CharacterGravityController _gravityController;

	private int _jumpsLeft;
	private float _jumpVelocity;

	private void Awake()
	{
		_gravityController = Player.Instance.GravityController;
	}

	private void Start()
	{
		RefreshJumpsCount();
		// Calculate the velocity required to reach the jump height.
		_jumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(Physics.gravity.y) * _jumpHeight);
	}

	private void OnEnable()
	{
		_gravityController.Landed += RefreshJumpsCount;
	}

	private void OnDisable()
	{
		_gravityController.Landed -= RefreshJumpsCount;
	}

	private void Update()
	{
		if (   Input.GetButtonDown("Jump")
			&& _jumpsLeft > 0
			&& _gravityController.VerticalVelocity > _minVelocityToJump)
		{
			_gravityController.SetVelocity(_jumpVelocity);
			_jumpsLeft--;
		}
	}

	private void RefreshJumpsCount(object sender = null, LandEventArgs e = null)
	{
		_jumpsLeft = _maxJumps;
	}
}
