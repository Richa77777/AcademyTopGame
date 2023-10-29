using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewSceneLoading : MonoBehaviour
{
    [SerializeField] private string _sceneName;

    public void LoadScene()
    {
        StartCoroutine(LoadSceneCor());
    }

    private IEnumerator LoadSceneCor()
    {
        FadeController.Instance.FadeIn();
        yield return new WaitForSeconds(FadeController.Instance.FadeTime + 0.15f);
        SceneManager.LoadScene(_sceneName);
    }
}
