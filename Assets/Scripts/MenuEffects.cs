using System.Collections.Generic;
using UnityEngine;

public class MenuEffects : MonoBehaviour
{
    public List<GameObject> prefabs; // List of prefabs to instantiate
    

    private List<Color> colors = new List<Color>
    {
        Color.white,
        Color.black
    };
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Spawn a random prefab every .2 seconds
        if (Time.time % 0.2f < 0.01f)
        {
            SpawnRandomPrefab();
        }
    }

    private void SpawnRandomPrefab()
    {
        if (prefabs.Count == 0) return; // Check if there are any prefabs to spawn

        int randomIndex = Random.Range(0, prefabs.Count); // Get a random index from the list
        GameObject prefabToSpawn = prefabs[randomIndex]; // Get the prefab to spawn

        Vector3 randomPosition = new Vector3(Random.Range(-10f, 10f), Random.Range(-10f, 10f), Random.Range(-10f, 10f)); // Generate a random position
        Quaternion randomRotation = Quaternion.Euler(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f)); // Generate a random rotation
        GameObject spawnObj = Instantiate(prefabToSpawn, randomPosition, randomRotation); // Instantiate the prefab at the random position
        int randomColorIndex = Random.Range(0, colors.Count); // Get a random color index
        spawnObj.GetComponent<Renderer>().material.color = colors[randomColorIndex]; // Set a random color for the prefab
        spawnObj.transform.localScale = Vector3.one * Random.Range(10f, 50f); // Set a random scale for the prefab
        spawnObj.GetComponent<Rigidbody>().AddForce(Random.insideUnitSphere * 5f, ForceMode.Impulse); // Add a random force to the prefab


    }
}
