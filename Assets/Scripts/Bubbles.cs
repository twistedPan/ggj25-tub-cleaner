using UnityEngine;

public class Bubble : MonoBehaviour
{
    private GameState _gameState;

    public int SoapAmount = 100;
    
    public bool SetSoapAmountBasedOnArea = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _gameState = GameObject.Find("GameState").GetComponent<GameState>();

        // If the soap amount should be set based on the area
        if (SetSoapAmountBasedOnArea) {
            // Set the soap amount based on the area
            SoapAmount = GetArea();
        }

        // Register this bubble with the GameState
        _gameState.RegisterBubble(this);
    }

    public int GetArea() {
        // X and Y are the dimensions of the stain
        float x = transform.localScale.x;
        float y = transform.localScale.y;
        // Return the area
        return (int)(x * y);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other) {
        // If the object that collided with this bubble is the player
        if (other.gameObject.tag == "Player") {
            _gameState.ConsumeBubble(this);
        } 
    }
}
