using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(CharacterGravityController))]
public class PlayerCrouch : MonoBehaviour
{
    public bool IsCrouching { get; private set; }

    [field:Header("Params")]
    [field: SerializeField, Min(0)] public float CameraOffset { get; private set; } = 0.05f;
    [SerializeField] private float _crouchHeight = 1f;
    [SerializeField] private float _standHeight = 1.8f;
    [SerializeField, Range(0.1f, 10f)] private float _crouchSpeed = 5f;
    [SerializeField] private float _headBlockedCheckDistance = 0.1f;
    
    private CharacterController _characterController;
    private CharacterGravityController _gravityController;
    private Transform _viewTransform;

    private float _currentHeight;

	private void Awake()
	{
		_characterController = Player.Instance.CharacterController;
        _gravityController = Player.Instance.GravityController;
        _viewTransform = Player.Instance.View.transform;
	}

	private void Start()
	{
		SetStandParams();
	}

	private void Update()
	{
		bool isCrouchPressed = Input.GetButton("Crouch");

        if (isCrouchPressed && _gravityController.IsGrounded)
        {
            IsCrouching = true;

            if (_currentHeight > _crouchHeight)
            {
				_currentHeight -= _crouchSpeed * Time.deltaTime;
                _currentHeight = Mathf.Max(_currentHeight, _crouchHeight);
				SetHeight(_currentHeight);
			}
        }
        else if (IsCrouching)
        {
            if (IsHeadBlocked())
                return;

			_currentHeight += _crouchSpeed * Time.deltaTime;
			_currentHeight = Mathf.Min(_currentHeight, _standHeight);
            SetHeight(_currentHeight);

            if (_currentHeight == _standHeight)
				IsCrouching = false;
		}
	}

    private bool IsHeadBlocked()
    {
		Vector3 castCenter = transform.position + Vector3.up * (_currentHeight - _characterController.radius);
        RaycastHit[] hits = Physics.SphereCastAll(castCenter,
                                                  _characterController.radius,
                                                  Vector3.up,
                                                  _headBlockedCheckDistance,
                                                  ~Player.Instance.PlayerLayerMask,
                                                  QueryTriggerInteraction.Ignore);

        return hits.Length > 0;
	}

	private void SetStandParams()
    {
        IsCrouching = false;
        _currentHeight = _standHeight;
        SetHeight(_currentHeight);
    }

    private void SetHeight(float height)
    {
        _characterController.height = height;
        _characterController.center = new Vector3(0, height / 2, 0);
        _viewTransform.localPosition = new Vector3(0, height - CameraOffset, 0);
    }

#if UNITY_EDITOR

	private void OnValidate()
	{
		if (_crouchHeight > _standHeight)
			_crouchHeight = _standHeight;

		if (CameraOffset > _crouchHeight)
			CameraOffset = _crouchHeight;
	}

#endif
}
