using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure
{
  public class SceneLoader
  {
    private readonly ICoroutineRunner _coroutineRunner;
    private readonly HUDLoading hudLoading;

    public SceneLoader(ICoroutineRunner coroutineRunner, HUDLoading hudLoading)
    {
      this.hudLoading = hudLoading;
      _coroutineRunner = coroutineRunner;
    }

    public void Load(string name, Action onLoaded = null) =>
      _coroutineRunner.StartCoroutine(LoadScene(name, onLoaded));

    private IEnumerator LoadScene(string nextSceneName, Action onLoaded = null)
    {
      if (nextSceneName == SceneManager.GetActiveScene().name)
      {
        onLoaded?.Invoke();
        yield break;
      }
      
      AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(nextSceneName);
      //hudLoading.StartLoad(waitNextScene, () => { onLoaded?.Invoke(); });

      while (!waitNextScene.isDone)
        yield return null;
      
      SceneManager.SetActiveScene(SceneManager.GetSceneByName(nextSceneName));
      onLoaded?.Invoke();
    }
  }
}