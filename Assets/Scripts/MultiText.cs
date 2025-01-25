using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MultiText : MonoBehaviour
{
    public List<string> Texts = new List<string>();
    
    private Text TextObject;
    private CanvasGroup CanvasGroupObject;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Find the Text object in the children of this object
        TextObject = GetComponentInChildren<Text>();

        CanvasGroupObject = GetComponent<CanvasGroup>();

    }

    // Update is called once per frame
    public void SetRandomText()
    {
        int randomIndex = Random.Range(0, Texts.Count);
        TextObject.text = Texts[randomIndex];
    }

    public void Show(int fadeInTime = 0)
    {
        if (fadeInTime > 0)
        {
            StartCoroutine(FadeIn(fadeInTime));
        }
        else
        {
            CanvasGroupObject.alpha = 1;
        }
    }

    IEnumerator FadeIn(int fadeInTime)
    {
        float elapsedTime = 0;
        while (elapsedTime < fadeInTime)
        {
            CanvasGroupObject.alpha = elapsedTime / fadeInTime;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    public void Hide()
    {
        CanvasGroupObject.alpha = 0;
    }
    
}
