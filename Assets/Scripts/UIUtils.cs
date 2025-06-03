using UnityEngine;
using TMPro;

public class UIUtils : MonoBehaviour
{
    private GameState gameState;
    public GameObject pawnPromote;
    private GameObject pawnToBePromoted;

    public GameObject knightPrefab;
    public GameObject bishopPrefab;
    public GameObject rookPrefab;
    public GameObject queenPrefab;

    public GameObject winnerPanel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameState = FindAnyObjectByType<GameState>().GetComponent<GameState>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPawnToBePromoted(GameObject pawn)
    {
        pawnToBePromoted = pawn; // Store the pawn to be promoted
        pawnPromote.SetActive(true); // Activate the pawn promotion UI
        Time.timeScale = 0f;

    }

    public void PromoteToKnight()
    {
        Promote(knightPrefab);
    }
    public void PromoteToBishop()
    {
        Promote(bishopPrefab);
    }
    public void PromoteToRook()
    {
        Promote(rookPrefab);
    }
    public void PromoteToQueen()
    {
        Promote(queenPrefab);
    }

    private void Promote(GameObject piecePrefab)
    {
        if (pawnToBePromoted != null)
        {
            
            pawnToBePromoted.SetActive(false); // Deactivate the pawn being promoted
            Destroy(pawnToBePromoted); // Destroy the pawn being promoted
            ChessPiece.Team teamForCreation = pawnToBePromoted.GetComponent<ChessPiece>().team; // Get the team of the pawn being promoted
            GameObject newPiece;
            if (teamForCreation == ChessPiece.Team.White)
            {
                newPiece = Instantiate(piecePrefab, pawnToBePromoted.transform.position + piecePrefab.transform.position - new Vector3 (0f,0.3f,0f), Quaternion.Euler(0f, 0f, 0f));
            }
            else
            {
                newPiece = Instantiate(piecePrefab, pawnToBePromoted.transform.position + piecePrefab.transform.position - new Vector3 (0f,0.3f,0f), Quaternion.Euler(0f, 180f, 0f));
            }
            newPiece.GetComponent<ChessPiece>().team = teamForCreation;
            
            pawnPromote.SetActive(false); // Deactivate the pawn promotion UI
            Time.timeScale = 1f;

        }
    }
    public void ShowWinnerPanel(ChessPiece.Team winningTeam)
    {
        winnerPanel.SetActive(true); // Activate the winner panel
        if (winningTeam == ChessPiece.Team.White)
        {
            winnerPanel.transform.GetChild(0).GetComponent<TextMeshPro>().text = "BLANCS GAGNENT !";
        }
        else
        {
            winnerPanel.transform.GetChild(0).GetComponent<TextMeshPro>().text = "NOIRS GAGNENT !";
        }
    }
}
