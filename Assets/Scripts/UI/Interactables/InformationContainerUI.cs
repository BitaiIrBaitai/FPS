using TMPro;
using UnityEngine;

public class InformationContainerUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _mainText;
	[SerializeField] private TMP_Text _secondaryText;
	[SerializeField] private PlayerInteractor _playerInteractor;

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
		if (informationContainer is null)
		{
			_mainText.text = string.Empty;
			_secondaryText.text = string.Empty;
		}
		else
		{
			_mainText.text = informationContainer.GetMainInformation();
			_secondaryText.text = informationContainer.GetSecondaryInformation();
		}
	}
}
