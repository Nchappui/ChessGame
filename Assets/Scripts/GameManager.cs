using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool isAIGame = false; // Flag to check if the game is an AI game
    public bool hasTimerGame = false; // Flag to check if the game has a timer
    public float timerDuration = 60f; // Default timer duration in seconds

    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this; // Set the singleton instance
            DontDestroyOnLoad(gameObject); // Keep this instance across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }

}
