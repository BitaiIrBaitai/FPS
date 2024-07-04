using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(CharacterGravityController))]
[RequireComponent(typeof(PlayerCrouch))]
public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

	[field: SerializeField] public LayerMask PlayerLayerMask { get; private set; }

    [field:Header("Components")]
	[field:SerializeField] public CharacterController CharacterController { get; private set; }
	[field:SerializeField] public CharacterGravityController GravityController { get; private set; }
	[field:SerializeField] public PlayerCrouch Crouch { get; private set; }
	[field:SerializeField] public PlayerMovement Movement { get; private set; }
	[field:SerializeField] public PlayerView View { get; private set; }

	private void Awake()
	{
		Instance = this;
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
	}

#endif
}
