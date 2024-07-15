using UnityEngine;

public class PauseUI : MonoBehaviour
{
    [SerializeField] private GameObject _pausePanel;

	private void Start()
	{
		if (PauseService.IsPaused)
			Show();
		else
			Hide();
	}

	private void OnEnable()
	{
		PauseService.Paused += Show;
		PauseService.Unpaused += Hide;
	}

	private void OnDisable()
	{
		PauseService.Paused -= Show;
		PauseService.Unpaused -= Hide;
	}

	private void Show()
    {
        _pausePanel.SetActive(true);
    }

    private void Hide()
	{
		_pausePanel.SetActive(false);
	}
}
