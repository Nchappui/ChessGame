using UnityEngine;

using TMPro;
public class TestScript : MonoBehaviour
{
    public GameObject winnerPanel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        winnerPanel.transform.GetChild(0).GetComponent<TMP_Text>().text = "BLANCS GAGNENT !";
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
