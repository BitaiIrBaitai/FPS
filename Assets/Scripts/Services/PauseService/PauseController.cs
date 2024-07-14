using UnityEngine;

public class PauseController : MonoBehaviour
{
    private void Update()
	{
		// ToDo: Check if player is alive
		if (Input.GetButtonDown("Pause"))
		{
			if (PauseService.IsPaused)
				PauseService.Unpause();
			else
				PauseService.Pause();
		}
	}
}
