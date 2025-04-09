using TMPro;
using UnityEngine;

public class LevelTimer : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI timerText;

    private float timeElapsed = 0f;

    void Start()
    {
        timeElapsed = 0f; // Se asegura de reiniciar al iniciar el nivel
    }

    void Update()
    {
        timeElapsed += Time.deltaTime;
        UpdateTimerUI();
    }

    void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(timeElapsed / 60);
        int seconds = Mathf.FloorToInt(timeElapsed % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}