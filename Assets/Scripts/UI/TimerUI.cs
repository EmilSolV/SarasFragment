using TMPro;
using UnityEngine;

public class TimerUI : MonoBehaviour
{
    public TimeManager timeManager;
    public TMP_Text timerText;

    void Update()
    {
        if (timeManager != null && timerText != null)
        {
            float time = timeManager.GetTimeRemaining();
            int minutes = Mathf.FloorToInt(time / 60f);
            int seconds = Mathf.FloorToInt(time % 60f);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
}
