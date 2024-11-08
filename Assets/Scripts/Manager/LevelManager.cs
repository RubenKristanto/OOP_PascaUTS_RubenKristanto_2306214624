using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Animator animator;     

    private void Awake()
    {
        animator.enabled = false;
    }

    public void LoadScene(string sceneName)
    {
            animator.enabled = true;
            StartCoroutine(LoadSceneAsync(sceneName));
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        animator.enabled = true;
        if (animator != null)
        {
            animator.SetTrigger("StartTransition");
        }
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        if (animator != null)
        {
            animator.SetTrigger("EndTransition");
        }

        
    }
}
