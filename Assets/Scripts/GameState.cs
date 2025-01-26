using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using Unity.Mathematics;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{   
    public bool EndlessMode = false;
    public int LevelTime = 60;
    public int SoapAmount = 100;

    public int SoapCapacity = 100;

    [SerializeField]
    private int _availableSoapAmount = 0;

    [SerializeField]
    private float _timeLeft;
    private bool _gameOver = false;

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
    public MultiText GameOverText;
    public MultiText LevelCompleteText;

    DeathDetector deathDetector;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _timeLeft = LevelTime;
        LevelTimerText.text = _timeLeft.ToString();
        SoapAmountText.text = SoapAmount.ToString();


        if (EndlessMode)
        {
            LevelTimerText.enabled = false;
        }

        // Find 
        deathDetector = FindFirstObjectByType<DeathDetector>();
        deathDetector.OnPlayerDied += OnGameOver;
    }

    void OnGameOver() {

        GameOverText.SetRandomText();
        
        GameOverText.Show(2);

        // wait
        Invoke("RestartGame", 4);
    }

    void OnLevelComplete() {
        Debug.Log("Level Complete");
        
        LevelCompleteText.SetRandomText();
        LevelCompleteText.Show(2);

        // wait
        Invoke("RestartGame", 4);
    }

    void RestartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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

            if (Stains.Count == 0) {
                OnLevelComplete();
            }
        } 

    }

    public void ConsumeBubble(Bubble bubble) {
        //_availableSoapAmount -= bubble.SoapAmount;
        int consumedSoapAmount = math.max(math.min(bubble.SoapAmount, SoapCapacity - SoapAmount), 0);
        
        _availableSoapAmount -= consumedSoapAmount;

        AddSoap(consumedSoapAmount);

        if (consumedSoapAmount > 0) {
            bubble.Consume(consumedSoapAmount);
        }

        if (bubble.SoapAmount == 0) {
            Bubbles.Remove(bubble);
            bubble.gameObject.SetActive(false);
        }
        
    }

    public void AddSoap(int amount) {
        SoapAmount += amount;
        SoapAmountText.text = SoapAmount.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        // Warn if amount of total soap is less than remaining dirt strength
        if (_availableSoapAmount + SoapAmount < _totalDirtStrength)
        {
            SoapAmountText.color = Color.red;
        }
        else
        {
            SoapAmountText.color = Color.black;
        }


        if (EndlessMode)
        {
            return;
        }

        _timeLeft -= Time.deltaTime;
        LevelTimerText.text = _timeLeft.ToString("0");

        if (_timeLeft <= 0)
        {
            _timeLeft = 0;
            LevelTimerText.text = "0";
            
            if (!_gameOver) {
                _gameOver = true;
                OnGameOver();
            }
        }
    }
}
