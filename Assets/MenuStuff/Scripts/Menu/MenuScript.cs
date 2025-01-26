using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    public GameValues GameValues;
    [SerializeField] private GameObject mainView;
    [SerializeField] private GameObject creditsView;
    [SerializeField] private Toggle endlessToggle;
    private MenuSponge menuSponge;
    
    void Awake() 
    {
        Cursor.visible = true;
        menuSponge = FindFirstObjectByType<MenuSponge>();
    }
    
    public void PlayGame(int level)
    {
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

    public void LoadSceneName(string sceneName)
    {
        Debug.Log("Loading scene: " + sceneName);
        Cursor.visible = false;
        menuSponge.GoGoSpongeBoy(() => SceneManager.LoadScene(sceneName));
    }

    public void EndlessMode(Toggle value)
    {
        Debug.Log("Endless mode: " + value.isOn);
        GameValues.EndlessMode = value.isOn;
    }
}
