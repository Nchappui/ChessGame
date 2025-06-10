using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class BishopPiece : MonoBehaviour
{
    private Case currentCase; // Reference to the Case component of the current square

    private GameState gameState;

    private List<Case> availableMoves = new List<Case>(); // Array to hold the available moves for the piece
    private List<GameObject> attackablePieces = new List<GameObject>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameState = FindAnyObjectByType<GameState>().GetComponent<GameState>();

    }

    // Update is called once per frame
    void Update()
    {


    }

    public void OnMouseDown()
    {
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject()){

            return; // Le clic était sur l'UI, on ignore
        }
        currentCase = GetComponent<ChessPiece>().getCurrentCase(); // Get the current case from the ChessPiece component
        availableMoves.Clear(); // Clear the list of available moves
        Case tempCase = currentCase; // Temporary variable to traverse the cases
        while (tempCase.N != null && tempCase.E != null)
        {
            tempCase = tempCase.N.E; // Move to the next case in the north-east direction
            if (!tempCase.isOccupied)
            {
                availableMoves.Add(tempCase); // Highlight the north-east square
            }
            else
            {
                if (tempCase.currentPiece.GetComponent<ChessPiece>().team != GetComponent<ChessPiece>().team)
                {
                    attackablePieces.Add(tempCase.currentPiece); // Add the piece to the attackable pieces list
                }
                break; // Stop traversing if a piece is found
            }
        }
        tempCase = currentCase; // Reset the temporary variable to the current case
        while (tempCase.N != null && tempCase.W != null)
        {
            tempCase = tempCase.N.W; // Move to the next case in the north-west direction
            if (!tempCase.isOccupied)
            {
                availableMoves.Add(tempCase); // Highlight the north-west square
            }
            else
            {
                if (tempCase.currentPiece.GetComponent<ChessPiece>().team != GetComponent<ChessPiece>().team)
                {
                    attackablePieces.Add(tempCase.currentPiece); // Add the piece to the attackable pieces list
                }
                break; // Stop traversing if a piece is found
            }
        }
        
        tempCase = currentCase; // Reset the temporary variable to the current case
        while (tempCase.S != null && tempCase.E != null)
        {
            tempCase = tempCase.S.E; // Move to the next case in the south-east direction
            if (!tempCase.isOccupied)
            {
                availableMoves.Add(tempCase); // Highlight the south-east square
            }
            else
            {
                if (tempCase.currentPiece.GetComponent<ChessPiece>().team != GetComponent<ChessPiece>().team)
                {
                    attackablePieces.Add(tempCase.currentPiece); // Add the piece to the attackable pieces list
                }
                break; // Stop traversing if a piece is found
            }
        }
        tempCase = currentCase; // Reset the temporary variable to the current case
        while (tempCase.S != null && tempCase.W != null)
        {
            tempCase = tempCase.S.W; // Move to the next case in the south-east direction
            if (!tempCase.isOccupied)
            {
                availableMoves.Add(tempCase); // Highlight the south-east square
            }
            else
            {
                if (tempCase.currentPiece.GetComponent<ChessPiece>().team != GetComponent<ChessPiece>().team)
                {
                    attackablePieces.Add(tempCase.currentPiece); // Add the piece to the attackable pieces list
                }
                break; // Stop traversing if a piece is found
            }
        }
        gameState.SelectPieceAsync(this.gameObject, availableMoves, attackablePieces); // Call the SelectPiece method in GameState to handle the selection
    
    }
}
