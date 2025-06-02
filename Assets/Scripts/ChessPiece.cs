using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChessPiece : MonoBehaviour
{
    private Case currentCase; // Reference to the Case component of the current square
    private List<Case> availableMoves = new List<Case>(); // Array to hold the available moves for the piece

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
        }
        currentCase.currentPiece = gameObject; // Set the current piece on the square
        currentCase.isOccupied = true; // Mark the square as occupied
    }

    // Update is called once per frame
    void Update()
    {


    }

    public void selectPiece(List<Case> availableMoves)
    {
        this.availableMoves = availableMoves;
        foreach (Case move in availableMoves)
        {
            move.transform.GetChild(0).gameObject.SetActive(true); // Enable the highlight on the available moves
        }

    }

    public void deselectPiece()
    {
        foreach (Case move in availableMoves)
        {
            move.transform.GetChild(0).gameObject.SetActive(false); // Disable the highlight on the available moves
        }
        GetComponent<Outline>().enabled = false; // Disable the outline of the piece
        availableMoves.Clear(); // Clear the list of available moves
    }

    public void setNewCase()
    {
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
            gameObject.GetComponent<PawnPiece>().setMovedToTrue(); // Set the hasMoved flag to true for PawnPiece
        }
    }

    public Case getCurrentCase()
    {
        return currentCase;
    }
}


