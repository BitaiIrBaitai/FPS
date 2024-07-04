using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(CharacterGravityController))]
[RequireComponent(typeof(PlayerCrouch))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Params")]
    [SerializeField, Min(0)] private float _walkSpeed = 5f;
    [SerializeField, Min(0)] private float _runSpeed = 10f;
    [SerializeField, Min(0)] private float _crouchSpeed = 2.5f;
    [SerializeField, Min(0)] private float _nonGroundedAcceleration = 5f;

    private CharacterController _characterController;
    private CharacterGravityController _gravityController;
    private PlayerCrouch _crouch;

    private Vector3 _lastHorizontalVelocity;

    private void Awake()
    {
        _characterController = Player.Instance.CharacterController;
        _gravityController = Player.Instance.GravityController;
        _crouch = Player.Instance.Crouch;
	}

	private void Update()
	{
		Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
		Vector3 horizontalVelocity = transform.TransformDirection(input);

        if (_gravityController.IsGrounded)
        {
			if (_crouch.IsCrouching)
                horizontalVelocity *= _crouchSpeed;
			else if (Input.GetButton("Run") && input.z > 0)
				horizontalVelocity *= _runSpeed;
			else
				horizontalVelocity *= _walkSpeed;

			_lastHorizontalVelocity = horizontalVelocity;
		}
		else
		{
			_lastHorizontalVelocity += horizontalVelocity * _nonGroundedAcceleration * Time.deltaTime;
		}

		CollisionFlags collisionFlags = _characterController.Move(_lastHorizontalVelocity * Time.deltaTime);

		if (collisionFlags.HasFlag(CollisionFlags.Sides))
			_lastHorizontalVelocity = Vector3.zero;
	}

#if UNITY_EDITOR

	private void OnValidate()
	{
		if (_crouchSpeed > _walkSpeed)
			_crouchSpeed = _walkSpeed;

        if (_runSpeed < _walkSpeed)
            _runSpeed = _walkSpeed;
	}

#endif
}
