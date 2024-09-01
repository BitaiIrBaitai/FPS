using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(CharacterGravityController))]
[RequireComponent(typeof(PlayerCrouch))]
public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
	
	public bool IsBlocked { get; private set; } = false;

	[field: SerializeField] public LayerMask PlayerLayerMask { get; private set; }

    [field:Header("Components")]
	[field:SerializeField] public CharacterController CharacterController { get; private set; }
	[field:SerializeField] public CharacterGravityController GravityController { get; private set; }
	[field:SerializeField] public PlayerCrouch Crouch { get; private set; }
	[field:SerializeField] public PlayerMovement Movement { get; private set; }
	[field:SerializeField] public PlayerView View { get; private set; }
	[field:SerializeField] public Health Health { get; private set; }

	private bool _blockedByPause = false;

	private void Awake()
	{
		Instance = this;

		PauseService.Paused += BlockOnPause;
		PauseService.Unpaused += UnblockOnUnpause;
	}

	private void OnDestroy()
	{
		PauseService.Paused -= BlockOnPause;
		PauseService.Unpaused -= UnblockOnUnpause;
	}

	public void Block()
	{
		GravityController.enabled = false;
		Crouch.enabled = false;
		Movement.enabled = false;
		View.enabled = false;

		IsBlocked = true;
	}

	public void Unblock()
	{
		GravityController.enabled = true;
		Crouch.enabled = true;
		Movement.enabled = true;
		View.enabled = true;

		IsBlocked = false;
	}

	private void BlockOnPause()
	{
		if (IsBlocked)
			return;

		_blockedByPause = true;
		Block();
	}

	private void UnblockOnUnpause()
	{
		if (!_blockedByPause)
			return;

		_blockedByPause = false;
		Unblock();
	}

#if UNITY_EDITOR

	[ContextMenu(nameof(TryGetComponents))]
    private void TryGetComponents()
    {
		CharacterController = GetComponent<CharacterController>();
		GravityController = GetComponent<CharacterGravityController>();
		Crouch = GetComponent<PlayerCrouch>();
		Movement = GetComponent<PlayerMovement>();
		View = GetComponentInChildren<PlayerView>();
		Health = GetComponent<Health>();
	}

#endif
}
