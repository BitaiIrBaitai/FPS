using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    [field:Header("Components")]
	[field:SerializeField] public CharacterController CharacterController { get; private set; }
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
		View = GetComponentInChildren<PlayerView>();
	}

#endif
}
