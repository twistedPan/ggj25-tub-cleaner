using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using Unity.Mathematics;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{
    public GameValues GameValues;
    public int CurrentSoapAmount = 100;

    [SerializeField]
    private float _timeLeft;
    [SerializeField]
    private int _availableSoapAmount = 0;
    [SerializeField]
    private int _totalDirtStrength = 0;

    private bool _gameOver = false;

    // List of stains
    [SerializeField]
    private List<Stain> Stains = new List<Stain>();

    [SerializeField]
    private List<Bubble> Bubbles = new List<Bubble>();


    // Text object to display the number of stains
    [Header("UI Elements")]
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
        _timeLeft = GameValues.TimeLimitInSeconds;
        LevelTimerText.text = _timeLeft.ToString();
        SoapAmountText.text = CurrentSoapAmount.ToString();


        if (GameValues.EndlessMode)
        {
            LevelTimerText.gameObject.SetActive(false);
        }

        // Find 
        deathDetector = FindFirstObjectByType<DeathDetector>();
        deathDetector.OnPlayerDied += OnGameOver;
    }

    void Update()
    {
        // Warn if amount of total soap is less than remaining dirt strength
        if (_availableSoapAmount + CurrentSoapAmount < _totalDirtStrength)
        {
            SoapAmountText.color = Color.red;
        }
        else
        {
            SoapAmountText.color = Color.black;
        }


        if (GameValues.EndlessMode)
        {
            return;
        }

        _timeLeft -= Time.deltaTime;
        LevelTimerText.text = _timeLeft.ToString("0");

        if (_timeLeft <= 0)
        {
            _timeLeft = 0;
            LevelTimerText.text = "0";

            if (!_gameOver)
            {
                _gameOver = true;
                OnGameOver();
            }
        }
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
        if (CurrentSoapAmount > 0) {
            // Reduce the soap amount
            int soapAmount = CurrentSoapAmount;
            int dirtStrength = stain.DirtStrength;
            
            CurrentSoapAmount = math.max(0, soapAmount - dirtStrength);
            stain.DirtStrength = math.max(0, dirtStrength - soapAmount);
            _totalDirtStrength -= dirtStrength - stain.DirtStrength;
            

            //Debug.Log("Soap amount: " + CurrentSoapAmount);
            //Debug.Log("New Stain dirt strength: " + stain.DirtStrength);

            if (stain.DirtStrength == 0) {
                // Delete this object
                Stains.Remove(stain);
                stain.gameObject.SetActive(false);
            }
            
            // Update the text object (convert int to string)
            StainCountText.text = Stains.Count.ToString();
            TotalDirtStrengthText.text = _totalDirtStrength.ToString();
            SoapAmountText.text = CurrentSoapAmount.ToString();

            if (Stains.Count == 0) {
                OnLevelComplete();
            }
        } 

    }

    public void ConsumeBubble(Bubble bubble) {
        //_availableSoapAmount -= bubble.SoapAmount;
        int consumedSoapAmount = math.max(math.min(bubble.SoapAmount, GameValues.MaxSoapCapacity - CurrentSoapAmount), 0);
        
        _availableSoapAmount -= consumedSoapAmount;

        AddSoap(consumedSoapAmount);

        if (consumedSoapAmount > 0) {
            bubble.Consume(consumedSoapAmount);
        }

        if (bubble.SoapAmount == 0) {
            Bubbles.Remove(bubble);
        }
        
    }

    public void AddSoap(int amount) {
        CurrentSoapAmount += amount;
        SoapAmountText.text = CurrentSoapAmount.ToString();
    }
}
