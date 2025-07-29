using static System.Convert;
using System.Collections;
using UnityEngine;
using TMPro;

public class TimerController : MonoBehaviour
{
    public bool timerActive;
    [HideInInspector] public bool showTimer;
    public TextMeshProUGUI timerText;
    public float passedTime;
    [HideInInspector] public string best="";

    void Start()
    {
        // Initialize
        passedTime = 0;
        timerActive = ToBoolean(PlayerPrefs.GetInt("Timer"));
        if (timerActive)
        {
            print("Starting timer");
            timerText.gameObject.SetActive(true);
        } else {
            timerText.gameObject.SetActive(false);
        }

        // Get the players best time
        if (!PlayerPrefs.HasKey("bestTime"))
        {
            PlayerPrefs.SetFloat("bestTime", 0);
        }
        int bestSeconds = Mathf.FloorToInt(PlayerPrefs.GetFloat("bestTime")%60);
        int bestMinutes = Mathf.FloorToInt(PlayerPrefs.GetFloat("bestTime")/60);
        best = string.Format("Best: {0:00}:{1:00}", bestMinutes, bestSeconds);
    }

    void Update()
    {
        timerText.gameObject.SetActive(showTimer);

        if (timerActive && showTimer)
        {
            passedTime += Time.deltaTime;
            updateTimer();
        }
    }

    public void updateTimer()
    {
        int seconds = Mathf.FloorToInt(passedTime%60);
        int minutes = Mathf.FloorToInt(passedTime/60);
        string time = string.Format("{0:00}:{1:00}", minutes, seconds);
        timerText.text = $"Timer: {time}\n{best}";
    }
}