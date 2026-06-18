using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagment : MonoBehaviour
{
    private AudioSource ButtonAudioSource;

    private void Start()
    {
        ButtonAudioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if(Input.anyKeyDown && SceneManager.GetActiveScene().name == "Prev")
        {
            SceneManager.LoadScene(0);
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

    private IEnumerator WaitButton(int indexScene)
    {
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene(indexScene);
    }
}
