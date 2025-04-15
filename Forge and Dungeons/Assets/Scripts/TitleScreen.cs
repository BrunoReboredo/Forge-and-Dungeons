using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("TestScene");
    }

    public void LoadGame()
    {
        Debug.Log("Función de carga aún no implementada");
        // Aquí se carga el sistema de guardado, pendiente de implementar
    }

    public void OpenOptions()
    {
        Debug.Log("Abrir opciones");
        // Aquí se carga las opciones, pendiente de implementar
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
