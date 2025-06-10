using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class KingPiece : MonoBehaviour
{
    private Case currentCase; // Reference to the Case component of the current square

    private GameState gameState;

    private List<Case> availableMoves = new List<Case>(); // Array to hold the available moves for the piece
    private List<GameObject> attackablePieces = new List<GameObject>();
    private List<GameObject> rooksToCastle = new List<GameObject>(); // List to hold the rooks for castling
    private bool hasMoved = false; // Flag to check if the king has moved
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameState = FindAnyObjectByType<GameState>().GetComponent<GameState>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addCase(Case caseToAdd)
    {
            if (caseToAdd == null) return; // Check if the case is null to avoid NullReferenceException
            if (caseToAdd.isOccupied && caseToAdd.currentPiece.GetComponent<ChessPiece>().team == GetComponent<ChessPiece>().team)
            {
                return; // If the case is occupied by a piece of the same team, do not add it
            }
            
            // Add the case to available moves or attackable pieces based on its occupation status
        if (!caseToAdd.isOccupied)
            {
                availableMoves.Add(caseToAdd); 
            }
            else
            {
                if (caseToAdd.currentPiece.GetComponent<ChessPiece>().team != GetComponent<ChessPiece>().team)
                {
                    attackablePieces.Add(caseToAdd.currentPiece); // Add the piece to the attackable pieces list
                }
            }
    }
    public void OnMouseDown()
    {
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject()){

            return; // Le clic était sur l'UI, on ignore
        }

        currentCase = GetComponent<ChessPiece>().getCurrentCase(); // Get the current case from the ChessPiece component
        availableMoves.Clear(); // Clear the list of available moves
        Case tempCase = currentCase; // Temporary variable to traverse the cases
        if (tempCase.N != null)
        {
            tempCase = tempCase.N; // Move to the north case
            addCase(tempCase);
            if (tempCase.E != null)
            {
                tempCase = tempCase.E; // Move to the north-east case
                addCase(tempCase);
            }
            tempCase = currentCase.N;
            if (tempCase.W != null)
            {
                tempCase = tempCase.W; // Move to the north-west case
                addCase(tempCase);
            }
        }

        tempCase = currentCase;
        if (tempCase.S != null)
        {
            tempCase = tempCase.S; // Move to the south case
            addCase(tempCase);
            if (tempCase.E != null)
            {
                tempCase = tempCase.E; // Move to the north-east case
                addCase(tempCase);
            }
            tempCase = currentCase.S;
            if (tempCase.W != null)
            {
                tempCase = tempCase.W; // Move to the north-west case
                addCase(tempCase);
            }
        }

        tempCase = currentCase;
        if (tempCase.E != null)
        {
            tempCase = tempCase.E; // Move to the east case
            addCase(tempCase);
        }

        tempCase = currentCase;
        if (tempCase.W != null)
        {
            tempCase = tempCase.W; // Move to the west case
            addCase(tempCase);
        }
        rooksToCastle.Clear(); // Clear the list of rooks for castling
        if (!hasMoved){
            if (!currentCase.E.isOccupied && !currentCase.E.E.isOccupied && currentCase.E.E.E.currentPiece.GetComponent<RookPiece>() != null && !currentCase.E.E.E.currentPiece.GetComponent<RookPiece>().getHasMoved())
            {
                GameObject rookPiece = currentCase.E.E.E.currentPiece; // Get the rook case for castling
                rooksToCastle.Add(rookPiece); // Add the rook to the list of rooks for castling
                availableMoves.Add(currentCase.E.E); // Add the square to the available moves for castling
            }
            if (!currentCase.W.isOccupied && !currentCase.W.W.isOccupied && !currentCase.W.W.W.isOccupied && currentCase.W.W.W.W.currentPiece.GetComponent<RookPiece>() != null && !currentCase.W.W.W.W.currentPiece.GetComponent<RookPiece>().getHasMoved())
            {
                GameObject rookPiece = currentCase.W.W.W.W.currentPiece; // Get the rook case for castling
                rooksToCastle.Add(rookPiece); // Add the rook to the list of rooks for castling
                availableMoves.Add(currentCase.W.W); // Add the square to the available moves for castling
            }
        }
        gameState.SelectPieceAsync(this.gameObject, availableMoves, attackablePieces, rooksToCastle); // Call the SelectPiece method in GameState to handle the selection

    }

    public void setMovedToTrue()
    {
        hasMoved = true; // Set the hasMoved flag to true
    }
    public bool getHasMoved()
    {
        return hasMoved; // Return the hasMoved flag
    }
}
