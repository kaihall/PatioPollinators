using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour
{
    public ObjectFadinator fadeSphere;
    public string sceneName;
    public float transitionWaitTime = 5;
    private bool ending = false;

    public void Transition()
    {
        if (!ending)
        {
            ending = true;
            fadeSphere.FadeIn();
            StartCoroutine("ChangeScene");
        }
    }

    IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(transitionWaitTime);
        SceneManager.LoadScene(sceneName);
    }
}
