using UnityEngine;
using UnityEngine.Rendering.Universal;
using Unity.Mathematics;

public class Stain : MonoBehaviour
{
    public int MaxDirtStrength = 1;
    public int DirtStrength = 1;

    public bool SetDirtStrengthBasedOnArea = true;
    // Get global GameState object
    private GameState _gameState;

    private DecalProjector _decalProjector;

    private DriftRubSFX _driftRubSFX;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {   
        _gameState = GameObject.Find("GameState").GetComponent<GameState>();
        // Register this stain with the GameState

        // If the dirt strength should be set based on the area
        if (SetDirtStrengthBasedOnArea) {
            // Set the dirt strength based on the area
            DirtStrength = GetArea();
            MaxDirtStrength = DirtStrength;
        }

        _gameState.RegisterStain(this);

        // Get the decal material
        _decalProjector = GetComponent<DecalProjector>();

        _driftRubSFX = FindFirstObjectByType<DriftRubSFX>();
    }

    // Update is called once per frame
    void Update()
    {
     // Update DecalProjector Opacity based on remaining dirt strength


    }

    // Get the area of the stain
    public int GetArea() {
        // X and Y are the dimensions of the stain
        float x = transform.localScale.x;
        float y = transform.localScale.y;
        // Return the area
        return (int)(x * y);
    }

    void OnTriggerEnter(Collider other) {
        // If the object that collided with this stain is the player
        Debug.Log("Collision detected with: " + other.gameObject.tag);
        if (other.gameObject.tag == "Player") {

            _driftRubSFX.PlayRandomSqueeekSFX();

            // Remove this stain from the GameState
            _gameState.RemoveStain(this);
        } 

        Debug.Log("Remaing dirt of max dirt: " + DirtStrength + " " + MaxDirtStrength +" Name: " + gameObject.name);
        _decalProjector.fadeFactor = math.max((float)DirtStrength / MaxDirtStrength, 0.3f);
    }
}
