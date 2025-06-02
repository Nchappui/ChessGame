using UnityEngine;

public class Case : MonoBehaviour
{
    public Case N;
    public Case S;
    public Case E;
    public Case W;

    public bool isOccupied = false; // Flag to check if the case is occupied
    public GameObject currentPiece; // The piece currently occupying the case
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (Physics.Raycast(transform.position, Vector3.forward, out RaycastHit ray))
        {
            N = ray.collider.gameObject.GetComponent<Case>();
        }
        if (Physics.Raycast(transform.position, -Vector3.forward, out RaycastHit ray2))
        {
            S = ray2.collider.gameObject.GetComponent<Case>();
        }
        if (Physics.Raycast(transform.position, Vector3.right, out RaycastHit ray3))
        {
            E = ray3.collider.gameObject.GetComponent<Case>();
        }
        if (Physics.Raycast(transform.position, Vector3.left, out RaycastHit ray4))
        {
            W = ray4.collider.gameObject.GetComponent<Case>();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
