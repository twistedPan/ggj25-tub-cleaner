using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    [SerializeField] private GameObject mainView;
    [SerializeField] private GameObject creditsView;
    
    void Awake() 
    {
        Cursor.visible = true;
    }
    
    public void PlayGame()
    {
        //Debug.Log("Play Game");
        Cursor.visible = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    public void QuitGame()
    {
        //Debug.Log("QUIT!");
        Application.Quit();
    }

    public void ShowCredits()
    {
        mainView.SetActive(false);
        creditsView.SetActive(true);
    }

    public void GoBack()
    {
        mainView.SetActive(true);
        creditsView.SetActive(false);
    }
}
