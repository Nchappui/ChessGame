using UnityEngine;

public class ChessPiece : MonoBehaviour
{
    private bool hasMoved = false; // Flag to check if the piece has moved
    public bool facingNorth = true; // Flag to check if the piece is facing north

    private Case currentCase; // Reference to the Case component of the current square

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
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

    void OnMouseDown()
    {
        // Check if the piece is on a square
        if (currentCase != null)
        {   
            if (facingNorth){
                if (!currentCase.N.isOccupied)
                {
                    currentCase.N.GetComponent<Renderer>().material.color = Color.green; // Highlight the north square
                    if (!hasMoved){
                       currentCase.N.N.GetComponent<Renderer>().material.color = Color.green; // Highlight the north square 
                    }
                }
            }
            else
            {
                if (!currentCase.S.isOccupied)
                {
                    currentCase.S.GetComponent<Renderer>().material.color = Color.green; // Highlight the south square
                    if (!hasMoved){
                       currentCase.S.S.GetComponent<Renderer>().material.color = Color.green; // Highlight the south square 
                    }
                }
            }

        }
    }
}

