using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ChessPiece : MonoBehaviour
{
    private Case currentCase; // Reference to the Case component of the current square
    private List<Case> availableMoves = new List<Case>(); // Array to hold the available moves for the piece
    private List<GameObject> attackablePieces = new List<GameObject>(); // Array to hold the pieces that can be attacked
    private List<GameObject> rooksToCastle = new List<GameObject>(); // List to hold the rooks for castling

    public char fenChar; // Character representing the piece in FEN notation

    public enum Team
    {
        White,
        Black
    }

    public Team team;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        // Perform a raycast to find the square the piece is on
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit ray))
        {
            currentCase = ray.collider.gameObject.GetComponent<Case>(); // Get the Case component from the square
            //Debug.Log(ray.collider.gameObject + " is the square for " + gameObject.name);
            currentCase.currentPiece = gameObject; // Set the current piece on the square
            currentCase.isOccupied = true; // Mark the square as occupied
        }
        else
        {
            Debug.LogWarning("Raycast n'a rien touché !");
        }

    }
    void Start(){
        if (team == Team.Black){
            this.GetComponent<Renderer>().material.SetColor("_BaseColor", Color.black);
        }
    }

    // Update is called once per frame
    void Update()
    {


    }

    public void selectPiece(List<Case> availableMoves, List<GameObject> attackablePieces, List<GameObject> rooksToCastle = null)
    {
        this.attackablePieces = attackablePieces; // Store the list of attackable pieces
        foreach (GameObject piece in attackablePieces)
        {
            piece.GetComponent<Outline>().enabled = true; // Enable the outline for attackable pieces
            piece.GetComponent<Outline>().OutlineColor = Color.red; // Set the outline color to red for attackable pieces
        }
    
    
        this.availableMoves = availableMoves;
        foreach (Case move in availableMoves)
        {
            move.transform.GetChild(0).gameObject.SetActive(true); // Enable the highlight on the available moves
        }

        this.rooksToCastle = rooksToCastle; // Store the list of rooks for castling
        if (rooksToCastle != null){
            foreach (GameObject rook in rooksToCastle)
            {
                rook.GetComponent<Outline>().enabled = true; // Enable the outline for rooks that can castle
                rook.GetComponent<Outline>().OutlineColor = Color.yellow; // Set the outline color to yellow for castling rooks
            }
        } // If there are no rooks to castle, return
        

        this.gameObject.GetComponent<Outline>().enabled = true;

    }
    

    public void deselectPiece(bool pawnMoved = false)
    {
        foreach (GameObject piece in attackablePieces)
        {
            piece.GetComponent<Outline>().enabled = false; // Disable the outline for attackable pieces
            piece.GetComponent<Outline>().OutlineColor = Color.green;
        }
        if (rooksToCastle != null){
            foreach (GameObject rook in rooksToCastle)
            {
                rook.GetComponent<Outline>().enabled = false; // Enable the outline for rooks that can castle
                rook.GetComponent<Outline>().OutlineColor = Color.green; // Set the outline color to yellow for castling rooks

                if (pawnMoved)
                {
                    rook.SetActive(false);
                    rook.GetComponent<ChessPiece>().getCurrentCase().isOccupied = false; // Mark the square as unoccupied
                    Destroy(rook); // If a pawn has moved, destroy the rook to prevent castling
                }
            }
        }
        attackablePieces.Clear(); // Clear the list of attackable pieces
        foreach (Case move in availableMoves)
        {
            move.transform.GetChild(0).gameObject.SetActive(false); // Disable the highlight on the available moves
        }
        
        GetComponent<Outline>().enabled = false; // Disable the outline of the piece
        availableMoves.Clear(); // Clear the list of available moves
    }

    public void setNewCase()
    {
        Case prevCase = currentCase; // Store the previous case
        currentCase.isOccupied = false; // Mark the current square as unoccupied
        currentCase.currentPiece = null; // Remove the current piece from the square
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit ray))
        {
            currentCase = ray.collider.gameObject.GetComponent<Case>(); // Get the Case component from the square
        }
        currentCase.currentPiece = gameObject; // Set the current piece on the square   
        currentCase.isOccupied = true; // Mark the square as occupied
        if (gameObject.GetComponent<PawnPiece>() != null)
        {
            if (Vector3.Distance(prevCase.transform.position, currentCase.transform.position) > 1.2f)
            {
                gameObject.GetComponent<PawnPiece>().hasJustDoubleMoved = true; // Set the hasJustDoubleMoved flag to true for PawnPiece
            }
            else
            {
                gameObject.GetComponent<PawnPiece>().hasJustDoubleMoved = false; // Reset the hasJustDoubleMoved flag for PawnPiece
            }
            gameObject.GetComponent<PawnPiece>().setMovedToTrue(); // Set the hasMoved flag to true for PawnPiece
            if (gameObject.GetComponent<PawnPiece>().getFacingNorth() && !currentCase.N || (!gameObject.GetComponent<PawnPiece>().getFacingNorth() && !currentCase.S))
            {
                UIUtils UIutils = FindAnyObjectByType<UIUtils>().GetComponent<UIUtils>();
                UIutils.SetPawnToBePromoted(gameObject); // Set the pawn to be promoted in the UI
            }
        }
        if (gameObject.GetComponent<RookPiece>() != null)
        {
            gameObject.GetComponent<RookPiece>().setMovedToTrue(); // Set the hasMoved flag to true for RookPiece
        }
        if (gameObject.GetComponent<KingPiece>() != null)
        {
            gameObject.GetComponent<KingPiece>().setMovedToTrue(); // Set the hasMoved flag to true for KingPiece
        }
    }

    public Case getCurrentCase()
    {
        return currentCase;
    }

    public void setCurrentCase(Case newCase)
    {
        currentCase = newCase; // Set the current case to the new case
        newCase.currentPiece = gameObject; // Set the current piece on the new case
        newCase.isOccupied = true; // Mark the new case as occupied
    }
}


