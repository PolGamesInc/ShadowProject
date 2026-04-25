using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagment : MonoBehaviour
{
    public void SceneLoader(int sceneIndex)
    {
       SceneManager.LoadScene(sceneIndex); 
    }

    public void ExitApp()
    {
        Application.Quit();
    }
}
