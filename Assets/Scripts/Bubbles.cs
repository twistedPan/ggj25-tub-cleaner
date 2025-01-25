using UnityEngine;
using System.Collections;
public class Bubble : MonoBehaviour
{
    private GameState _gameState;

    public int SoapAmount = 100;
    
    public bool SetSoapAmountBasedOnArea = true;
    private AudioSource _soapSFX;
    private int _startingSoapAmount;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _gameState = GameObject.Find("GameState").GetComponent<GameState>();

        // If the soap amount should be set based on the area
        if (SetSoapAmountBasedOnArea) {
            // Set the soap amount based on the area
            SoapAmount = GetArea();
        }

        _startingSoapAmount = SoapAmount;

        // Register this bubble with the GameState
        _gameState.RegisterBubble(this);

        _soapSFX = GameObject.Find("SoapSound").GetComponent<AudioSource>();
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
            _soapSFX.Play();
            _gameState.ConsumeBubble(this);
        } 
    }

    public void Consume(int amount) {
        SoapAmount -= amount;
        
        // Resize the bubble based on the remaining soap amount
        float scale = (float)SoapAmount / _startingSoapAmount;

        float oldArea = GetArea();
        float newArea = oldArea * scale;

        // Workaround because I can't think of a better way right now
        float newEdge = Mathf.Sqrt(newArea);
        //transform.localScale = new Vector3(newEdge, newEdge, transform.localScale.z);
        Vector3 destinationScale = new Vector3(newEdge, newEdge, transform.localScale.z);
        StartCoroutine(ScaleOverTime(1, destinationScale));

    }

    private IEnumerator ScaleOverTime(float time, Vector3 scale)
    {
        Vector3 originalScale = transform.localScale;
        Vector3 destinationScale = scale;

        float currentTime = 0.0f;

        do
        {
            transform.localScale = Vector3.Lerp(originalScale, destinationScale, currentTime / time);
            currentTime += Time.deltaTime;
            yield return null;
        } while (currentTime <= time);

        transform.localScale = destinationScale;
    }
}
