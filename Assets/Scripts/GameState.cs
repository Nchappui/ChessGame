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

    public void SelectPiece(GameObject piece)
    {
        if (currentlySelectedPiece == null)
        {
            currentlySelectedPiece = piece;
        }
        else
        {
            currentlySelectedPiece.GetComponent<ChessPiece>().deselectPiece();
            currentlySelectedPiece = piece;
        }
        currentlySelectedPiece.GetComponent<Outline>().enabled = true;
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
