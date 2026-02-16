using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField]
    private Animator fade;
    [SerializeField]
    private string fadeAnimationName;
    public void GoToSeceneWhitFade(string sceneName)
    {
        StartCoroutine (LoadSceneArterFade(sceneName));
    }

    private IEnumerator LoadSceneArterFade(string sceneName)
    {
        fade.Play(fadeAnimationName, 0, 0f);
        yield return new WaitForSeconds(fade.GetCurrentAnimatorStateInfo(0).length);
        SceneManager.LoadScene(sceneName);
    }
}
