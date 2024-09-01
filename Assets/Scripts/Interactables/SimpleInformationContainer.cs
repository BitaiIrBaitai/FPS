using System;
using UnityEngine;

public class SimpleInformationContainer : MonoBehaviour, IInformationContainer
{
	public event EventHandler StateChanged;

    [SerializeField] private string _mainInformation;
	[SerializeField] private string _secondaryInformation;

	public string GetMainInformation()
	{
		return _mainInformation;
	}

	public string GetSecondaryInformation()
	{
		return _secondaryInformation;
	}
}
