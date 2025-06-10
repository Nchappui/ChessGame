using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class KnightPiece : MonoBehaviour
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
            if (tempCase.E != null && tempCase.E.E != null)
            {
                tempCase = tempCase.E.E; // Move to the north-east case
                addCase(tempCase);
            }
            tempCase = currentCase.N;
            if (tempCase.W != null && tempCase.W.W != null)
            {
                tempCase = tempCase.W.W; // Move to the north-west case
                addCase(tempCase);
            }
            tempCase = currentCase.N;
            if (tempCase.N != null){
                tempCase = tempCase.N; // Move to the north-north case
                if (tempCase.E !=null){
                    tempCase = tempCase.E; // Move to the north-north-east case
                    addCase(tempCase);
                }
                tempCase = currentCase.N.N;
                if (tempCase.W != null)
                {
                    tempCase = tempCase.W; // Move to the north-north-west case
                    addCase(tempCase);
                }
            }
        }

        tempCase = currentCase;
        if (tempCase.S != null)
        {
            tempCase = tempCase.S; // Move to the south case
            if (tempCase.E != null && tempCase.E.E != null)
            {
                tempCase = tempCase.E.E; // Move to the south-east case
                addCase(tempCase);
            }
            tempCase = currentCase.S;
            if (tempCase.W != null && tempCase.W.W != null)
            {
                tempCase = tempCase.W.W; // Move to the south-west case
                addCase(tempCase);
            }
            tempCase = currentCase.S;
            if (tempCase.S != null){
                tempCase = tempCase.S; // Move to the south-south case
                if (tempCase.E !=null){
                    tempCase = tempCase.E; // Move to the south-south-east case
                    addCase(tempCase);
                }
                tempCase = currentCase.S.S;
                if (tempCase.W != null)
                {
                    tempCase = tempCase.W; // Move to the south-south-west case
                    addCase(tempCase);
                }
            }
        }
        gameState.SelectPieceAsync(this.gameObject, availableMoves, attackablePieces); // Call the SelectPiece method in GameState to handle the selection

    }
}
