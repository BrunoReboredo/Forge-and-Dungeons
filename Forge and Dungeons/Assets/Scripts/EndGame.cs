using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame1 : MonoBehaviour
{
    public void GoToTitleScreen()
    {
        SceneManager.LoadScene("TitleScreen");
    }

}
