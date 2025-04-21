using UnityEngine;

public class OptionsMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject optionsMenuUI;

    private void Start()
    {
        optionsMenuUI.SetActive(false);
    }

    public void OpenOptionsMenu()
    {
        optionsMenuUI.SetActive(true);
    }

    public void CloseOptionsMenu()
    {
        optionsMenuUI.SetActive(false);
        PauseMenuManager.Instance?.PauseGame();
    }

    public bool IsOptionsOpen()
    {
        return optionsMenuUI.activeSelf;
    }
}
