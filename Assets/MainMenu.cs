using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private const string MAIN_SCENE_NAME = "SampleScene";
    private const string MENU_SCENE_NAME = "MainMenu";

    [SerializeField] private GameObject _loadingText;

    private void Start()
    {
        _loadingText.SetActive(false);
    }

    public void LoadMainScene()
    {
        ManagerScene.LoadScene(MAIN_SCENE_NAME);
        _loadingText.SetActive(true);
    }

    public void QuitApp()
    {
        ManagerScene.QuitGame();
    }
}
