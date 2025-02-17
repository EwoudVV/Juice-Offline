using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SessionTimer : MonoBehaviour
{
    public GameObject startScreen;
    public GameObject sessionScreen;

    public GameObject pausedUI;
    public GameObject resumedUI;

    public Button startButton;
    public Button pauseButton;
    public Button resumeButton;
    public Button cancelButton;
    public TMP_Text timerText;

    private bool isTracking = false;
    private float startTime;
    private float elapsedTime = 0f;

    void Start()
    {
        startButton.onClick.AddListener(StartTracking);
        pauseButton.onClick.AddListener(PauseTracking);
        resumeButton.onClick.AddListener(ResumeTracking);
        cancelButton.onClick.AddListener(CancelTracking);


        startScreen.SetActive(true);
        sessionScreen.SetActive(false);

        pausedUI.SetActive(false);
        resumedUI.SetActive(false);

        timerText.text = "Current Session: 00:00";
    }

    void StartTracking()
    {
        if (!isTracking)
        {
            isTracking = true;
            startTime = Time.time - elapsedTime;

            startScreen.SetActive(false);
            sessionScreen.SetActive(true);

            resumedUI.SetActive(true);
            pausedUI.SetActive(false);
        }
    }

    void PauseTracking()
    {
        if (isTracking)
        {
            isTracking = false;
            elapsedTime = Time.time - startTime;

            resumedUI.SetActive(false);
            pausedUI.SetActive(true);
        }
    }

    void ResumeTracking()
    {
        if (!isTracking)
        {
            isTracking = true;
            startTime = Time.time - elapsedTime;

            resumedUI.SetActive(true);
            pausedUI.SetActive(false);
        }
    }

    void CancelTracking()
    {
        isTracking = false;
        elapsedTime = 0f;
        timerText.text = "Current Session: 00:00";
        startScreen.SetActive(true);
        sessionScreen.SetActive(false);
        
        pausedUI.SetActive(false);
        resumedUI.SetActive(false);
    }

    void Update()
    {
        if (isTracking)
        {
            float currentTime = Time.time - startTime;
            int minutes = (int)currentTime / 60;
            int seconds = (int)currentTime % 60;
            timerText.text = $"Current Session: {minutes:00}:{seconds:00}";
        }
    }
}
