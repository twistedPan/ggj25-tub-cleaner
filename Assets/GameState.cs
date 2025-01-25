using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameState : MonoBehaviour
{

    // List of stains
    [SerializeField]
    protected List<Stain> stains = new List<Stain>();
    
    [SerializeField]
    protected int StainSquare = 0;

    // Text object to display the number of stains
    [SerializeField]
    public Text stainCountText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void RegisterStain(Stain stain) {
        stains.Add(stain);

        // Update area of stains
        StainSquare += stain.GetArea();

        // Update the text object
        stainCountText.text = stains.Count.ToString();
    }

    public void RemoveStain(Stain stain) {
        stains.Remove(stain);

        // Update area of stains
        StainSquare -= stain.GetArea();

        // Update the text object (convert int to string)
        stainCountText.text = stains.Count.ToString();


    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
