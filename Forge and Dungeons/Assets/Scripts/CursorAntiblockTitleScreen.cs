using UnityEngine;

public class CursorAntiblockTitleScreen : MonoBehaviour
{
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
