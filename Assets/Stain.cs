using UnityEngine;

public class Stain : MonoBehaviour
{
    // Get global GameState object
    private GameState gameState;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {   
        gameState = GameObject.Find("GameState").GetComponent<GameState>();
        // Register this stain with the GameState
        gameState.RegisterStain(this);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Get the area of the stain
    public int GetArea() {
        // X and Z are the dimensions of the stain
        float x = transform.localScale.x;
        float z = transform.localScale.z;
        // Return the area
        return (int)(x * z);
    }

    void OnTriggerEnter(Collider other) {

        // Remove this stain from the GameState
        gameState.RemoveStain(this);
        // Delete this object
        Destroy(gameObject);
    }
}
