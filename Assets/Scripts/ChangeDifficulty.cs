using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ChangeDifficulty : MonoBehaviour
{
    private TMP_Text difficultyText;
    public Slider slider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        difficultyText = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public void ChangeDifficultyLevel()
    {
        difficultyText.text = "Niveau de l'IA: " + slider.value.ToString(); // Update the UI text to reflect the new skill level
    }
}
