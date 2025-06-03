using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameState : MonoBehaviour
{
    private GameObject currentlySelectedPiece = null;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public GameObject getCurrentlySelectedPiece()
    {
        return currentlySelectedPiece;
    }

    public void SelectPiece(GameObject piece, List<Case> availableMoves, List<GameObject> attackablePieces)
    {
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
            if (piece.GetComponent<Outline>().OutlineColor == Color.red)
            {
                // If the piece is attackable, move it

                //HANDLE SCORE HERE
                MovePiece(piece.GetComponent<ChessPiece>().getCurrentCase().transform);
                Destroy(piece.gameObject);
                return;
            }
            else
            {
            currentlySelectedPiece.GetComponent<ChessPiece>().deselectPiece();
            currentlySelectedPiece = piece;
            }
            
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
            currentlySelectedPiece.transform.position = new Vector3(
            newPlace.position.x,
            currentlySelectedPiece.transform.position.y,
            newPlace.position.z
            );
            currentlySelectedPiece.GetComponent<ChessPiece>().setNewCase();
            currentlySelectedPiece.GetComponent<ChessPiece>().deselectPiece();
            currentlySelectedPiece = null;
        }
    }
}
