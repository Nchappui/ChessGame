using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Cinemachine;
//using Cinemachine;

public class GameState : MonoBehaviour
{
    private GameObject currentlySelectedPiece = null;
    private bool isWhiteTurn = true; // Flag to check if it's white's turn
    public GameObject playerOneCamera;
    public GameObject playerTwoCamera;

    public ParticleSystem fireWorks;

    private bool rotateCamera = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SceneManager.LoadScene("UIScene", LoadSceneMode.Additive);
    }

    // Update is called once per frame
    void Update()
    {
        if (rotateCamera)
        {
            playerOneCamera.transform.RotateAround(new Vector3(3.5f,0.7f,3.5f), Vector3.up, 20 * Time.deltaTime); 
            playerTwoCamera.transform.RotateAround(new Vector3(3.5f,0.7f,3.5f), Vector3.up, 20 * Time.deltaTime);
        }


    }

    public GameObject getCurrentlySelectedPiece()
    {
        return currentlySelectedPiece;
    }

    public void SelectPiece(GameObject piece, List<Case> availableMoves, List<GameObject> attackablePieces)
    {
        if (piece.GetComponent<Outline>().OutlineColor == Color.red)
            {
                // If the piece is attackable, move it

                if (piece.GetComponent<KingPiece>() !=null){
                    UIUtils UIutils = FindAnyObjectByType<UIUtils>().GetComponent<UIUtils>();
                    if(piece.GetComponent<ChessPiece>().team == ChessPiece.Team.White){
                        UIutils.ShowWinnerPanel(ChessPiece.Team.Black);
                    } else {
                        UIutils.ShowWinnerPanel(ChessPiece.Team.White);
                        
                    }
                    fireWorks.Play();
                    rotateCamera = true;
                }
                piece.gameObject.SetActive(false);
                Destroy(piece.gameObject);
                MovePiece(piece.GetComponent<ChessPiece>().getCurrentCase().transform);
                return;
            }
        if (piece.GetComponent<ChessPiece>().team == ChessPiece.Team.Black && isWhiteTurn || 
        piece.GetComponent<ChessPiece>().team == ChessPiece.Team.White && !isWhiteTurn)
        {
            return; // If it's white's turn and the piece is black, do nothing
        }
        if (currentlySelectedPiece == null)
        {
            currentlySelectedPiece = piece;
        }
        else if (currentlySelectedPiece == piece)
        {
            return;
        }
        else
        {
  
            currentlySelectedPiece.GetComponent<ChessPiece>().deselectPiece();
            currentlySelectedPiece = piece;
            
        }
        
        currentlySelectedPiece.GetComponent<ChessPiece>().selectPiece(availableMoves, attackablePieces);
    }

    public void MovePiece(Transform newPlace)
    {
        if (currentlySelectedPiece == null)
        {
            return;
        }
        else
        {
    
            currentlySelectedPiece.GetComponent<ChessPiece>().setCurrentCase(newPlace.GetComponent<Case>());
            currentlySelectedPiece.transform.position = new Vector3(
            newPlace.position.x,
            currentlySelectedPiece.transform.position.y,
            newPlace.position.z
);
                        
            currentlySelectedPiece.GetComponent<ChessPiece>().setNewCase();
            currentlySelectedPiece.GetComponent<ChessPiece>().deselectPiece();
            currentlySelectedPiece = null;
            isWhiteTurn = !isWhiteTurn; // Switch turns after a move
            if (!rotateCamera){
                playerOneCamera.SetActive(isWhiteTurn); // Activate player one camera if it's white's turn
                playerTwoCamera.SetActive(!isWhiteTurn); // Activate player two camera if it's black's turn
            }

        }
    }
}
