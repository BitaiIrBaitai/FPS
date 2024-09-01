using System;
using UnityEngine;
using UnityEngine.UI;

public class InteractIconUI : MonoBehaviour
{
	[Header("Params")]
	[SerializeField] private Color _activeColor = Color.green;
	[SerializeField] private Color _inactiveColor = Color.gray;

	[Header("Components")]
	[SerializeField] private Image _icon;
	[SerializeField] private PlayerInteractor _interactor;

	private IInteractable _interactable;

	private void Awake()
	{
		ShowIconState();
	}

	private void OnEnable()
	{
		_interactor.InteractableChanged += OnInteractableChanged;
	}

	private void OnDisable()
	{
		_interactor.InteractableChanged -= OnInteractableChanged;
	}

	private void OnInteractableChanged(IInteractable interactable)
	{
		if (_interactable is not null)
			_interactable.StateChanged -= ShowIconState;

		_interactable = interactable;
		if (_interactable is not null)
			_interactable.StateChanged += ShowIconState;

		ShowIconState();
	}

	private void ShowIconState(object sender = null, EventArgs e = null)
	{
		if (_interactable is null)
			_icon.gameObject.SetActive(false);
		else
		{
			_icon.gameObject.SetActive(true);
			_icon.color = _interactable.Active ? _activeColor : _inactiveColor;
		}
	}
}
