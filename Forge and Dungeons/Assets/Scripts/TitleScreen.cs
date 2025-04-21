using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("HistoryIntro");
    }

    public void LoadGame()
    {
        Debug.Log("Función de carga aún no implementada");
        // Aquí se carga el sistema de guardado, pendiente de implementar
    }

    public void OpenOptions()
    {
        SceneManager.LoadScene("Options");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
