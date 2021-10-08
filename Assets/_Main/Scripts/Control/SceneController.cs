using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    public Animator faderAnimator;
    public AnimationClip fadingClip;

    public Image loadingBarMask;

    public const string LOADING_FADER_VISIBLE = "visible";

    private const string MAINMENU_SCENE_NAME = "UIHomeScene";
    private const string GAMEPLAY_SCENE_NAME = "GameplayScene";

    private static SceneController instance;

    private void Awake()
    {
        #region SINGLETON
        if (instance != null)
        {
            Destroy(gameObject); //Delete if another instance already exists and is assigned.
            return;
        }
        instance = this; //set this as the singleton
        DontDestroyOnLoad(gameObject); //to make it persist between levels
        #endregion
    }

    public void GoToMenu()
    {
        StartCoroutine(LoadSceneTransition(MAINMENU_SCENE_NAME));
    }

    private IEnumerator LoadSceneTransition(string sceneName)
    {
        faderAnimator.SetBool(LOADING_FADER_VISIBLE, true);
        loadingBarMask.fillAmount = 0;

        yield return new WaitForEndOfFrame();

        yield return StartCoroutine(WaitForRealSeconds(fadingClip.length));

        AsyncOperation load = SceneManager.LoadSceneAsync(sceneName);

        while (!load.isDone)
        {
            loadingBarMask.fillAmount = load.progress;
            yield return null;
        }

        faderAnimator.SetBool(LOADING_FADER_VISIBLE, false);
        loadingBarMask.fillAmount = load.progress;

        PauseController.Instance.ForceUnpause();
    }

    public static IEnumerator WaitForRealSeconds(float time)
    {
        float start = Time.unscaledTime;
        while (Time.unscaledTime < start + time)
        {
            yield return null;
        }
    }

    #region properties
    public static SceneController Instance { get => instance; }
    #endregion
}
