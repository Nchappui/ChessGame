using UnityEngine;

public class EmptyPieceLifetime : MonoBehaviour
{
    private float lifetime = 5f; // Lifetime of the empty piece in seconds
    private float timer = 0f; // Timer to track the lifetime

    private bool isReducing = false; // Flag to check if the piece is reducing its size
    private float reduceTimer = 5f; 

    private float initialScale; // Initial scale of the piece

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initialScale = transform.localScale.x; // Store the initial scale of the piece
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime; // Increment the timer by the time since the last frame
        if (timer >= lifetime && !isReducing)
        {
            isReducing = true; // Start reducing the size of the piece
        }
        if (isReducing)
        {
            reduceTimer -= Time.deltaTime; // Decrement the reduce timer
            if (reduceTimer <= 0f)
            {
                Destroy(gameObject); // Destroy the piece when the reduce timer reaches zero
            }
            else
            {
                float scale = Mathf.Lerp(initialScale, 0.1f, 1 - (reduceTimer / 5f)); // Calculate the new scale based on the reduce timer
                transform.localScale = Vector3.one * scale; // Apply the new scale to the piece
            }
        }
    }
}
