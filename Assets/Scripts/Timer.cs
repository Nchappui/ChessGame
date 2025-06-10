using UnityEngine;
using TMPro;
public class Timer : MonoBehaviour
{
    private GameState gameState;
    private TMP_Text timerText;

    public bool isWhiteTimer = true; // Flag to check if the timer is for white or black

    private float timerDuration = 300f; // Default timer duration in seconds
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameState = FindAnyObjectByType<GameState>().GetComponent<GameState>();
        timerText = GetComponent<TMP_Text>();
        string timerMins = Mathf.Floor(timerDuration / 60).ToString("00"); // Calculate minutes and format to 2 digits
        string timerSecs = Mathf.Floor(timerDuration % 60).ToString("00"); // Calculate seconds and format to 2 digits
        string timerMillis = Mathf.Floor((timerDuration * 10) % 10).ToString("0"); // Calculate milliseconds and format to 1 digits
        timerText.text = $"{timerMins}:{timerSecs}:{timerMillis}"; // Update the timer text with formatted time
    }

    // Update is called once per frame
    void Update()
    {
        if (gameState.isWhiteTurn != isWhiteTimer) // Check if it's the turn for the current timer
        {
            return; // If not, skip updating the timer
        }
        timerDuration -= Time.deltaTime; // Decrease the timer duration by the time passed since last frame
        string timerMins = Mathf.Floor(timerDuration / 60).ToString("00"); // Calculate minutes and format to 2 digits
        string timerSecs = Mathf.Floor(timerDuration % 60).ToString("00"); // Calculate seconds and format to 2 digits
        string timerMillis = Mathf.Floor((timerDuration * 10) % 10).ToString("0"); // Calculate milliseconds and format to 1 digits
        timerText.text = $"{timerMins}:{timerSecs}:{timerMillis}"; // Update the timer text with formatted time
    }
}
