using System;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    public event Action<IInformationContainer> InformationContainerChanged;
	public event Action<IInteractable> InteractableChanged;

	[SerializeField, Range(1, 10f)] private float _interactionRange = 2f;

    private IInformationContainer _currentInformationContainer;
	private IInteractable _currentInteractable;

	private Transform _camera;
    private LayerMask _playerLayerMask;

	private void Start()
	{
		_camera = Player.Instance.Camera.transform;
		_playerLayerMask = Player.Instance.PlayerLayerMask;
	}

	private void Update()
	{
		if (Input.GetButtonDown("Interact") && _currentInteractable is { Active: true })
			_currentInteractable.Interact();
	}

	private void FixedUpdate()
	{
		Physics.Raycast(_camera.position,
						_camera.forward,
						out RaycastHit hit,
						_interactionRange,
						~_playerLayerMask,
						QueryTriggerInteraction.Ignore);

		IInformationContainer newContainer = hit.collider?.GetComponent<IInformationContainer>();
		_currentInteractable = newContainer as IInteractable;

		if (newContainer != _currentInformationContainer)
		{
			_currentInformationContainer = newContainer;
			InformationContainerChanged?.Invoke(_currentInformationContainer);
			InteractableChanged?.Invoke(_currentInteractable);
		}
	}
}
