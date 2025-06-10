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

    private List<GameObject> pawnPriseEnPassant = new List<GameObject>(); // List to hold the pawns that can be taken en passant

    public bool hasJustDoubleMoved = false; // Flag to check if the piece has double moved

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
        pawnPriseEnPassant.Clear(); // Clear the list of pawns that can be taken en passant
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
            //PRISE EN PASSANT FOR WHITE PAWN
            if (currentCase.N && currentCase.N.E && currentCase.E.isOccupied && currentCase.E.currentPiece == gameState.lastMovedPiece && currentCase.E.currentPiece.GetComponent <ChessPiece>().team == ChessPiece.Team.Black
            && currentCase.N.E.isOccupied == false && currentCase.E.currentPiece.GetComponent<PawnPiece>() != null && currentCase.E.currentPiece.GetComponent<PawnPiece>().hasJustDoubleMoved) 
            {
                availableMoves.Add(currentCase.N.E); // Highlight the east square
                pawnPriseEnPassant.Add(currentCase.E.currentPiece); // Add the pawn that can be taken en passant
            }
            if (currentCase.N && currentCase.N.W && currentCase.W.isOccupied && currentCase.W.currentPiece == gameState.lastMovedPiece && currentCase.W.currentPiece.GetComponent <ChessPiece>().team == ChessPiece.Team.Black
            && currentCase.N.W.isOccupied == false && currentCase.W.currentPiece.GetComponent<PawnPiece>() != null && currentCase.W.currentPiece.GetComponent<PawnPiece>().hasJustDoubleMoved)
            {
                availableMoves.Add(currentCase.N.W); // Highlight the west square
                pawnPriseEnPassant.Add(currentCase.W.currentPiece); // Add the pawn that can be taken en passant
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
            //PRISE EN PASSANT FOR BLACK PAWN
            if (currentCase.S && currentCase.S.E && currentCase.E.isOccupied && currentCase.E.currentPiece == gameState.lastMovedPiece && currentCase.E.currentPiece.GetComponent <ChessPiece>().team == ChessPiece.Team.White
            && currentCase.S.E.isOccupied == false && currentCase.E.currentPiece.GetComponent<PawnPiece>() != null && currentCase.E.currentPiece.GetComponent<PawnPiece>().hasJustDoubleMoved)
            {
                availableMoves.Add(currentCase.S.E); // Highlight the east square
                pawnPriseEnPassant.Add(currentCase.E.currentPiece); // Add the pawn that can be taken en passant
            }
            if (currentCase.S && currentCase.S.W && currentCase.W.isOccupied && currentCase.W.currentPiece == gameState.lastMovedPiece && currentCase.W.currentPiece.GetComponent <ChessPiece>().team == ChessPiece.Team.White
            && currentCase.S.W.isOccupied == false && currentCase.W.currentPiece.GetComponent<PawnPiece>() != null && currentCase.W.currentPiece.GetComponent<PawnPiece>().hasJustDoubleMoved)
            {
                availableMoves.Add(currentCase.S.W); // Highlight the west square
                pawnPriseEnPassant.Add(currentCase.W.currentPiece); // Add the pawn that can be taken en passant
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
        gameState.SelectPieceAsync(this.gameObject, availableMoves, attackablePieces, pawnPriseEnPassant);
        
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

