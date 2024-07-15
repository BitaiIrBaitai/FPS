using UnityEngine;

public class PauseButtons : MonoBehaviour
{
    public void OnResumeClick()
    {
        PauseService.Unpause();
	}

	public void OnSettingsClick()
    {
        Debug.Log("Settings");
	}

	public void OnQuitClick()
	{
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
