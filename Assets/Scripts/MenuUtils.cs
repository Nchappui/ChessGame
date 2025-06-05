using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuUtils : MonoBehaviour
{
    private bool isAIGame = false;
    private bool isMultiplayerGame = true;
    private bool hasTimerGame = false;
    private float timerDuration = 60f; // Default timer duration in seconds
                                       // Start is called once before the first execution of Update after the MonoBehaviour is created

    public List<Button> timerButtons;

    public Button aiButton;
    public Button multiplayerButton;
    void Start()
    {
        multiplayerButton.GetComponent<Image>().color = Color.limeGreen; // Reset multiplayer button color to white
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetAIGame()
    {
        isAIGame = true;
        aiButton.GetComponent<Image>().color = Color.limeGreen; // Change AI button color to green
        multiplayerButton.GetComponent<Image>().color = Color.white; // Reset multiplayer button color to white
        isMultiplayerGame = !isAIGame; // If AI game is enabled, multiplayer is disabled
        Debug.Log("AI Game Mode: " + (isAIGame ? "Enabled" : "Disabled"));
    }
    public void SetMultiplayerGame()
    {
        isMultiplayerGame = true;
        multiplayerButton.GetComponent<Image>().color = Color.limeGreen; // Change multiplayer button color to green
        aiButton.GetComponent<Image>().color = Color.white; // Reset AI button color to white
        isAIGame = !isMultiplayerGame; // If multiplayer game is enabled, AI game is disabled
        Debug.Log("Multiplayer Game Mode: " + (isMultiplayerGame ? "Enabled" : "Disabled"));
    }
    public void SetTimerGame()
    {
        hasTimerGame = !hasTimerGame;
        foreach (Button button in timerButtons)
        {
            button.interactable = hasTimerGame; // Enable or disable buttons based on timer game mode
        }
        if (hasTimerGame)
            switch (timerDuration)
            {
                case 60f:
                    timerButtons[0].GetComponent<Image>().color = Color.limeGreen; // Set 30 seconds button color to green
                    timerButtons[1].GetComponent<Image>().color = Color.white; // Reset 60 seconds button color to white
                    timerButtons[2].GetComponent<Image>().color = Color.white; // Reset 120 seconds button color to white
                    break;
                case 180f:
                    timerButtons[1].GetComponent<Image>().color = Color.limeGreen; // Set 60 seconds button color to green
                    timerButtons[0].GetComponent<Image>().color = Color.white; // Reset 30 seconds button color to white
                    timerButtons[2].GetComponent<Image>().color = Color.white; // Reset 120 seconds button color to white
                    break;
                case 300f:
                    timerButtons[2].GetComponent<Image>().color = Color.limeGreen; // Set 120 seconds button color to green
                    timerButtons[0].GetComponent<Image>().color = Color.white; // Reset 30 seconds button color to white
                    timerButtons[1].GetComponent<Image>().color = Color.white; // Reset 60 seconds button color to white
                    break;
                default:
                    foreach (Button button in timerButtons)
                    {
                        button.GetComponent<Image>().color = Color.white; // Reset timer buttons color to white
                    }
                    break;
            }
        else
            foreach (Button button in timerButtons)
            {
                button.GetComponent<Image>().color = Color.white; // Reset timer buttons color to white
            }
        Debug.Log("Timer Game Mode: " + (hasTimerGame ? "Enabled" : "Disabled") + ", Duration: " + timerDuration + " seconds");
    }
    public void SetTimerDuration(float duration)
    {
        timerDuration = duration;
        switch (timerDuration)
        {
            case 60f:
                timerButtons[0].GetComponent<Image>().color = Color.limeGreen; // Set 30 seconds button color to green
                timerButtons[1].GetComponent<Image>().color = Color.white; // Reset 60 seconds button color to white
                timerButtons[2].GetComponent<Image>().color = Color.white; // Reset 120 seconds button color to white
                break;
            case 180f:
                timerButtons[1].GetComponent<Image>().color = Color.limeGreen; // Set 60 seconds button color to green
                timerButtons[0].GetComponent<Image>().color = Color.white; // Reset 30 seconds button color to white
                timerButtons[2].GetComponent<Image>().color = Color.white; // Reset 120 seconds button color to white
                break;
            case 300f:
                timerButtons[2].GetComponent<Image>().color = Color.limeGreen; // Set 120 seconds button color to green
                timerButtons[0].GetComponent<Image>().color = Color.white; // Reset 30 seconds button color to white
                timerButtons[1].GetComponent<Image>().color = Color.white; // Reset 60 seconds button color to white
                break;
            default:
                foreach (Button button in timerButtons)
                {
                    button.GetComponent<Image>().color = Color.white; // Reset timer buttons color to white
                }
                break;
        }
        Debug.Log("Timer Duration set to: " + timerDuration + " seconds");
    }

    public void startGame()
    {
        // Here you can implement the logic to start the game with the selected settings
        Debug.Log("Starting Game with Settings: AI Game - " + isAIGame + ", Multiplayer Game - " + isMultiplayerGame + ", Timer Game - " + hasTimerGame + ", Timer Duration - " + timerDuration + " seconds");
        // You can also load the game scene or initialize the game state here
    }
}
