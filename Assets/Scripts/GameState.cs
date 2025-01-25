using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using Unity.Mathematics;

public class GameState : MonoBehaviour
{   
    public int LevelTime = 60;
    public int SoapAmount = 100;

    [SerializeField]
    private int _availableSoapAmount = 0;

    [SerializeField]
    private float _timeLeft;

    // List of stains
    [SerializeField]
    private List<Stain> Stains = new List<Stain>();

    [SerializeField]
    private List<Bubble> Bubbles = new List<Bubble>();
    
    [SerializeField]
    private int _totalDirtStrength = 0;

    // Text object to display the number of stains
    public Text StainCountText;
    public Text TotalDirtStrengthText;
    public Text LevelTimerText;
    public Text SoapAmountText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _timeLeft = LevelTime;
        LevelTimerText.text = _timeLeft.ToString();
        SoapAmountText.text = SoapAmount.ToString();
    }

    public void RegisterStain(Stain stain) {
        Stains.Add(stain);

        // Update strength of stains
        _totalDirtStrength += stain.DirtStrength;

        // Update the text object
        StainCountText.text = Stains.Count.ToString();
        TotalDirtStrengthText.text = _totalDirtStrength.ToString();
    }

    public void RegisterBubble(Bubble bubble) {
        Bubbles.Add(bubble);
        _availableSoapAmount += bubble.SoapAmount;
    }

    public void RemoveStain(Stain stain) {
        if (SoapAmount > 0) {
            // Reduce the soap amount
            int soapAmount = SoapAmount;
            int dirtStrength = stain.DirtStrength;
            
            SoapAmount = math.max(0, soapAmount - dirtStrength);
            stain.DirtStrength = math.max(0, dirtStrength - soapAmount);
            _totalDirtStrength -= dirtStrength - stain.DirtStrength;
            

            Debug.Log("Soap amount: " + SoapAmount);
            Debug.Log("New Stain dirt strength: " + stain.DirtStrength);

            if (stain.DirtStrength == 0) {
                // Delete this object
                Stains.Remove(stain);
                stain.gameObject.SetActive(false);
            }
            
            // Update the text object (convert int to string)
            StainCountText.text = Stains.Count.ToString();
            TotalDirtStrengthText.text = _totalDirtStrength.ToString();
            SoapAmountText.text = SoapAmount.ToString();
        } 

    }

    public void ConsumeBubble(Bubble bubble) {
        Bubbles.Remove(bubble);
        _availableSoapAmount -= bubble.SoapAmount;
        AddSoap(bubble.SoapAmount);
        bubble.gameObject.SetActive(false);
    }

    public void AddSoap(int amount) {
        SoapAmount += amount;
        SoapAmountText.text = SoapAmount.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        _timeLeft -= Time.deltaTime;
        LevelTimerText.text = _timeLeft.ToString("0");

        if (_timeLeft <= 0)
        {
            _timeLeft = 0;
            LevelTimerText.text = "0";
        }

        // Warn if amount of total soap is less than remaining dirt strength
        if (_availableSoapAmount + SoapAmount < _totalDirtStrength) {
            SoapAmountText.color = Color.red;
        } else {
            SoapAmountText.color = Color.black;
        }
       
    }
}
