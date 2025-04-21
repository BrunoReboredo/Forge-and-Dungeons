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
        Cursor.lockState = CursorLockMode.None;  // Desbloquea el cursor
        Cursor.visible = true;  // Hace visible el cursor

        // Desactiva la interfaz del jugador (como la barra de vida, energía, etc.)
        if (playerUI != null)
        {
            playerUI.SetActive(false);
        }
    }

    public void CloseOptionsMenu()
    {
        optionsMenuUI.SetActive(false);  // Desactiva el menú de opciones
        Time.timeScale = 1f;  // Restaura el tiempo normal
        Cursor.lockState = CursorLockMode.Locked;  // Bloquea el cursor (ya que vamos de nuevo al menú de pausa)
        Cursor.visible = false;  // Oculta el cursor

        // Comprobamos si estamos en la escena del título
        if (SceneManager.GetActiveScene().name != "TitleScreen")
        {
            Cursor.lockState = CursorLockMode.Locked;  // Bloquea el cursor si no estamos en la escena del título
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;  // No bloqueamos el cursor si estamos en el título
        }

        // Vuelve a mostrar la interfaz del jugador cuando se cierra el menú de opciones
        if (playerUI != null)
        {
            playerUI.SetActive(true);
        }

        PauseMenuManager.Instance.ResumeGame();  // Vuelve al estado del juego en pausa
    }
    public bool IsOptionsOpen()
    {
        return optionsMenuUI.activeSelf;
    }

    // Método para cambiar a la pestaña General
    public void ShowGeneralPanel()
    {
        generalPanel.SetActive(true);  // Muestra el panel de General
        controlsPanel.SetActive(false);  // Oculta el panel de Controles
    }

    // Método para cambiar a la pestaña Controles
    public void ShowControlsPanel()
    {
        controlsPanel.SetActive(true);  // Muestra el panel de Controles
        generalPanel.SetActive(false);  // Oculta el panel de General
    }
}
