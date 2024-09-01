using System;
using UnityEngine;

public class Timer : MonoBehaviour, IInformationContainer
{
	public event EventHandler StateChanged;

	private int _seconds;

	public string GetMainInformation()
	{
		return "Game timer";
	}

	public string GetSecondaryInformation()
	{
		return $"{(int)Time.timeSinceLevelLoad/60:D2}:{_seconds:D2}";
	}

	private void Update()
	{
		if (_seconds != (int)Time.timeSinceLevelLoad)
		{
			_seconds = (int)Time.timeSinceLevelLoad;
			StateChanged?.Invoke(this, EventArgs.Empty);
		}
	}
}
