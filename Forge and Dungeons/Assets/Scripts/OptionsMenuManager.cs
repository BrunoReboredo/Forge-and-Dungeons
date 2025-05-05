using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject optionsMenuUI;
    [SerializeField] private GameObject playerUI;

    [SerializeField] private GameObject generalPanel;
    [SerializeField] private GameObject controlsPanel;
    private void Start()
    {
        optionsMenuUI.SetActive(false);
        ShowGeneralPanel();
    }

    public void OpenOptionsMenu()
    {
        optionsMenuUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Desactiva la interfaz del jugador
        if (playerUI != null)
        {
            playerUI.SetActive(false);
        }
    }

    public void CloseOptionsMenu()
    {
        optionsMenuUI.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;  // Bloquea el cursor de vuelta
        Cursor.visible = false;

        // Compprueba si esta en la escena del título
        if (SceneManager.GetActiveScene().name != "TitleScreen")
        {
            Cursor.lockState = CursorLockMode.Locked;  // Bloquea el cursor si no estamos en la escena del título
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }

        // Vuelve a mostrar la interfaz del jugador cuando se cierra el menú de opciones
        if (playerUI != null)
        {
            playerUI.SetActive(true);
        }

        PauseMenuManager.Instance.ResumeGame();
    }
    public bool IsOptionsOpen()
    {
        return optionsMenuUI.activeSelf;
    }

    // Método para cambiar a la pestaña General
    public void ShowGeneralPanel()
    {
        generalPanel.SetActive(true);
        controlsPanel.SetActive(false);
    }

    // Método para cambiar a la pestaña Controles
    public void ShowControlsPanel()
    {
        controlsPanel.SetActive(true);
        generalPanel.SetActive(false);
    }
}
