using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    public static PauseMenuManager Instance;

    [SerializeField] GameObject pauseMenuUI;

    private bool isPaused = false;

    private void Awake()
    {
        // Aseguramos que solo exista una instancia de PauseMenuManager
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        pauseMenuUI.SetActive(false);
    }

    public void TogglePause()
    {
        if (isPaused)
            ResumeGame();
        else
            PauseGame();
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;
        pauseMenuUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public bool IsPaused()
    {
        return isPaused;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}