using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadingSceneManager : MonoBehaviour
{
    public UnityEngine.UI.Image loadingBar;
    public UnityEngine.UI.Button button;

    private static int levelIndex = 1;
    public static void LoadLevel(int index)
    {
        levelIndex = index;
        //Application.LoadLevel("s Loading");   //For Unity Older then 5.3
        SceneManager.LoadScene("s Loading");
    }

    public static void LoadLevel(string name)
    {
        levelIndex = SceneManager.GetSceneByName(name).buildIndex;
        //Application.LoadLevel("s Loading");   //For Unity Older then 5.3
        SceneManager.LoadScene("s Loading");
    }

    void Start()
    {
        StartCoroutine(LoadLevelWithBar(levelIndex));
    }

    private IEnumerator LoadLevelWithBar(int index)
    {
        AsyncOperation asy = SceneManager.LoadSceneAsync(index);
        asy.allowSceneActivation = false;

        float loaded = 0;
        while (!asy.isDone)
        {
            loaded = asy.progress / 0.9f;
            loadingBar.fillAmount = loaded;

            if (loaded >= 0.98f)
            {
                button.gameObject.SetActive(true);
                button.onClick.AddListener(() =>
                {
                    asy.allowSceneActivation = true;
                });
            }

            yield return null;
        }

        yield return null;
    }

}
