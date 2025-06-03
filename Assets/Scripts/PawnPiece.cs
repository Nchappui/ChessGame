using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class PawnPiece : MonoBehaviour
{
    private bool hasMoved = false; // Flag to check if the piece has moved
    private bool facingNorth;// Flag to check if the piece is facing north

    private Case currentCase; // Reference to the Case component of the current square

    private GameState gameState;

    private List<Case> availableMoves = new List<Case>(); // Array to hold the available moves for the piece
    private List<GameObject> attackablePieces = new List<GameObject>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameState = FindAnyObjectByType<GameState>().GetComponent<GameState>();
        facingNorth = GetComponent<ChessPiece>().team == ChessPiece.Team.White; // Check if the piece is facing north based on its team
        

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
        if (facingNorth)
        {   //MOVEMENT FOR WHITE PAWN
            if (currentCase.N && !currentCase.N.isOccupied)
            {
                availableMoves.Add(currentCase.N); // Highlight the north square
                if (!hasMoved && !currentCase.N.N.isOccupied)
                {
                    availableMoves.Add(currentCase.N.N); // Highlight the north square
                }
            }
            //ATTACK FOR WHITE PAWN
            if (currentCase.N && currentCase.N.W && currentCase.N.W.isOccupied && currentCase.N.W.currentPiece.GetComponent<ChessPiece>().team == ChessPiece.Team.Black)
            {
                attackablePieces.Add(currentCase.N.W.currentPiece);
            }
            if (currentCase.N && currentCase.N.E && currentCase.N.E.isOccupied && currentCase.N.E.currentPiece.GetComponent<ChessPiece>().team == ChessPiece.Team.Black)
            {
                attackablePieces.Add(currentCase.N.E.currentPiece); 
            }
        }
        else
        {   //MOVEMENT FOR BLACK PAWN
            if (currentCase.S && !currentCase.S.isOccupied)
            {
                availableMoves.Add(currentCase.S);// Highlight the south square
                if (!hasMoved && !currentCase.S.S.isOccupied)
                {
                    availableMoves.Add(currentCase.S.S); // Highlight the south square
                }
            }
            //ATTACK FOR BLACK PAWN
            if (currentCase.S && currentCase.S.W && currentCase.S.W.isOccupied && currentCase.S.W.currentPiece.GetComponent<ChessPiece>().team == ChessPiece.Team.White)
            {
                attackablePieces.Add(currentCase.S.W.currentPiece);
            }
            if (currentCase.S && currentCase.S.E && currentCase.S.E.isOccupied && currentCase.S.E.currentPiece.GetComponent<ChessPiece>().team == ChessPiece.Team.White)
            {
                attackablePieces.Add(currentCase.S.E.currentPiece);
            }
        }
        gameState.SelectPiece(this.gameObject, availableMoves, attackablePieces);
        
    }
    
    public void setMovedToTrue()
    {
        hasMoved = true; // Set the hasMoved flag to true
    }

    public bool getFacingNorth()
    {
        return facingNorth; // Return the facingNorth flag
    }
}

