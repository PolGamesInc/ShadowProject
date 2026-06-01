using System.Collections;
using UnityEngine;

public class CameraView : MonoBehaviour
{
    [SerializeField] private GameObject MainCamera;
    private Animator CameraViewAnimator;

    private void Start()
    {
        MainCamera.SetActive(false);
        StartCoroutine(WaitCameraView());
        CameraViewAnimator = GetComponent<Animator>();
        CameraViewAnimator.SetTrigger("Go");
    }

    private IEnumerator WaitCameraView()
    {
        yield return new WaitForSeconds(2.3f);
        MainCamera.SetActive(true);
        Destroy(gameObject);
    }
}
