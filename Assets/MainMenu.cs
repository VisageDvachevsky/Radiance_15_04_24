using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private const string MAIN_SCENE_NAME = "SampleScene";
    private const string MENU_SCENE_NAME = "MainMenu";

    public void LoadMainScene()
    {
        ManagerScene.LoadScene(MAIN_SCENE_NAME);
    }

    public void QuitApp()
    {
        ManagerScene.QuitGame();
    }
}
