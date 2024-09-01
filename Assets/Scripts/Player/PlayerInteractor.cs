using System;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    public event Action<IInformationContainer> InformationContainerChanged;

	[SerializeField, Range(1, 10f)] private float _interactionRange = 2f;

    private IInformationContainer _currentInformationContainer;

    private Transform _camera;
    private LayerMask _playerLayerMask;

	private void Start()
	{
		_camera = Player.Instance.Camera.transform;
		_playerLayerMask = Player.Instance.PlayerLayerMask;
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

		if (newContainer != _currentInformationContainer)
		{
			_currentInformationContainer = newContainer;
			InformationContainerChanged?.Invoke(_currentInformationContainer);
		}
	}
}
