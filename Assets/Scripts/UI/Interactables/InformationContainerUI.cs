using System;
using TMPro;
using UnityEngine;

public class InformationContainerUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _mainText;
	[SerializeField] private TMP_Text _secondaryText;
	[SerializeField] private PlayerInteractor _playerInteractor;

	private IInformationContainer _informationContainer;

	private void Awake()
	{
		_mainText.text = string.Empty;
		_secondaryText.text = string.Empty;
	}

	private void OnEnable()
	{
		_playerInteractor.InformationContainerChanged += OnInformationContainerChanged;
	}

	private void OnDisable()
	{
		_playerInteractor.InformationContainerChanged -= OnInformationContainerChanged;
	}

	private void OnInformationContainerChanged(IInformationContainer informationContainer)
	{
		if (_informationContainer is not null)
			_informationContainer.StateChanged -= ShowInformation;

		_informationContainer = informationContainer;
		if (_informationContainer is not null)
			_informationContainer.StateChanged += ShowInformation;
		
		ShowInformation();
	}

	private void ShowInformation(object sender = null, EventArgs e = null)
	{
		if (_informationContainer is null)
		{
			_mainText.text = string.Empty;
			_secondaryText.text = string.Empty;
		}
		else
		{
			_mainText.text = _informationContainer.GetMainInformation();
			_secondaryText.text = _informationContainer.GetSecondaryInformation();
		}
	}
}
