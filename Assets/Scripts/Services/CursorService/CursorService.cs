using UnityEngine;

public class CursorService : MonoBehaviour
{
    public bool Visible
#if UNITY_EDITOR
		=> _visible;
#else
		=> Cursor.visible;
#endif

	private bool _shownByPause = false;

#if UNITY_EDITOR
	// Field to avoid bugs in the editor
	private bool _visible = false;
#endif

	private void Start()
	{
		if (PauseService.IsPaused)
			ShowOnPause();
		else
			Hide();
	}

	private void OnEnable()
	{
		PauseService.Paused += ShowOnPause;
		PauseService.Unpaused += HideOnUnpause;
	}

	private void OnDisable()
	{
		PauseService.Paused -= ShowOnPause;
		PauseService.Unpaused -= HideOnUnpause;
	}

	private void HideOnUnpause()
	{
		if (_shownByPause)
		{
			_shownByPause = false;
			Hide();
		}
	}

	private void ShowOnPause()
	{
		if (Visible)
			return;

		_shownByPause = true;
		Show();
	}

	public void Show()
	{
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.Confined;

#if UNITY_EDITOR
		_visible = true;
#endif
	}
	
	public void Hide()
	{
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;

#if UNITY_EDITOR
		_visible = false;
#endif
	}
}
