using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagment : MonoBehaviour
{
    private AudioSource ButtonAudioSource;

    private void Start()
    {
        ButtonAudioSource = GetComponent<AudioSource>();

        if(SceneManager.GetActiveScene().name == "Prev")
        {
            StartCoroutine(WaitLoadSceneMenu(0));
        }
    }

    public void SceneLoader(int sceneIndex)
    {
       ButtonAudioSource.Play();
        StartCoroutine(WaitButton(sceneIndex));
    }

    public void ExitApp()
    {
        Application.Quit();
    }

    private IEnumerator WaitLoadSceneMenu(int indexScene)
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(indexScene);
    }

    private IEnumerator WaitButton(int indexScene)
    {
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene(indexScene);
    }
}
