using System;
using UnityEngine;

public static class PauseService
{
    public static bool IsPaused { get; private set; } = false;

    public static event Action Paused;
    public static event Action Unpaused;

    public static void Pause()
    {
        if (IsPaused)
			return;

        IsPaused = true;
		Time.timeScale = 0f;
		Paused?.Invoke();
    }

    public static void Unpause()
    {
        if (!IsPaused)
            return;

        IsPaused = false;
        Time.timeScale = 1f;
        Unpaused?.Invoke();
    }
}
