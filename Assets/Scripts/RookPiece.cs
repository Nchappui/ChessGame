using UnityEngine;
using System.Collections.Generic;

public class RookPiece : MonoBehaviour
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
        currentCase = GetComponent<ChessPiece>().getCurrentCase(); // Get the current case from the ChessPiece component
        availableMoves.Clear(); // Clear the list of available moves
        Case tempCase = currentCase; // Temporary variable to traverse the cases
        while (tempCase.N != null)
        {
            tempCase = tempCase.N; // Move to the next case in the north direction
            if (!tempCase.isOccupied)
            {
                availableMoves.Add(tempCase); // Highlight the north square
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
        while (tempCase.S != null)
        {
            tempCase = tempCase.S; // Move to the next case in the south direction
            if (!tempCase.isOccupied)
            {
                availableMoves.Add(tempCase); // Highlight the south square
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
        while (tempCase.E != null)
        {
            tempCase = tempCase.E; // Move to the next case in the east direction
            if (!tempCase.isOccupied)
            {
                availableMoves.Add(tempCase); // Highlight the east square
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
        while (tempCase.W != null)
        {
            tempCase = tempCase.W; // Move to the next case in the west direction
            if (!tempCase.isOccupied)
            {
                availableMoves.Add(tempCase); // Highlight the west square
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
        gameState.SelectPiece(this.gameObject, availableMoves, attackablePieces); // Call the SelectPiece method in GameState to handle the selection
    
    }
}